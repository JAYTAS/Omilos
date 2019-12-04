using AutoMapper;
using Jaytas.Omilos.Common;
using Jaytas.Omilos.Common.Models;
using Jaytas.Omilos.Messaging.ServiceBus.Topic.Interfaces;
using Jaytas.Omilos.ServiceClient.Subscription.Interfaces;
using Jaytas.Omilos.Web.Providers;
using Jaytas.Omilos.Web.Service.Campaign.Business.Interfaces;
using Jaytas.Omilos.Web.Service.Campaign.Data.Repositories.Interfaces;
using Jaytas.Omilos.Web.Service.Campaign.DomainModel;
using Jaytas.Omilos.Web.Service.Models.Campaign.Input;
using Jaytas.Omilos.Web.Service.Models.Common;
using Jaytas.Omilos.Web.Service.Models.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Campaign.Business
{
	/// <summary>
	/// 
	/// </summary>
	public class CampaignProvider : CrudByFieldBaseProvider<DomainModel.Campaign, ICampaignRepository, long, Guid>, ICampaignProvider
	{
		readonly IMapper _mapper;
		readonly ISubscriptionServiceClient _subscriptionServiceClient;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="campaignRepository"></param>
		/// <param name="mapper"></param>
		/// <param name="subscriptionServiceClient"></param>
		/// <param name="messageSenderFactory"></param>
		/// <param name="messageBusMessageFactory"></param>
		public CampaignProvider(ICampaignRepository campaignRepository, IMapper mapper, ISubscriptionServiceClient subscriptionServiceClient,
								IMessageSenderFactory messageSenderFactory, IMessageBusMessageFactory messageBusMessageFactory) 
							   : base(campaignRepository)
		{
			_mapper = mapper;
			_subscriptionServiceClient = subscriptionServiceClient;
			MessageBusMessageFactory = messageBusMessageFactory;
			MessageSenderFactory = messageSenderFactory;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="domains"></param>
		/// <returns></returns>
		public async override Task AssertEntityToCreateIsValidAsync(IEnumerable<DomainModel.Campaign> domains)
		{
			//throw new NotImplementedException();
			await Task.CompletedTask;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="identifiers"></param>
		/// <returns></returns>
		public async override Task AssertEntityToDeleteIsValidAsync(IEnumerable<Guid> identifiers)
		{
			//throw new NotImplementedException();
			await Task.CompletedTask;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="domains"></param>
		/// <returns></returns>
		public async override Task AssertEntityToUpdateIsValidAsync(IEnumerable<DomainModel.Campaign> domains)
		{
			//throw new NotImplementedException();
			await Task.CompletedTask;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="subscriptionId"></param>
		/// <param name="pageDetails"></param>
		/// <returns></returns>
		public async Task<PagedResultSet<Models.Campaign.CampaignSummary>> GetMyCampaigns(Guid? subscriptionId, PageDetails pageDetails)
		{
			Expression<Func<DomainModel.Campaign, bool>> expression = campaign => true;

			if(subscriptionId.HasValue && subscriptionId.Value != Guid.Empty)
			{
				expression = campaign => campaign.SubscriptionId == subscriptionId;
			}

			var campaigns = await Repository.GetAsync(expression);

			if (!campaigns.Any())
			{
				return PagedResultSet<Models.Campaign.CampaignSummary>.Construct(new List<Models.Campaign.CampaignSummary>(), null, null);
			}

			var campaignsSummary = _mapper.Map<IEnumerable<Models.Campaign.CampaignSummary>>(campaigns);
			var IdentifierFilter = campaignsSummary.Select(campaignSummary => new Models.Subscription.Input.IdentifierFilter {
																						SubscriptionId = campaignSummary.SubscriptionId,
																						GroupId = campaignSummary.GroupId
																}).ToList();
			var subscriptionsAndGroupSummary = await _subscriptionServiceClient.GetSubscriptionsAndGroupSummaryByIdAsync(IdentifierFilter);

			campaignsSummary.ToList().ForEach(summary =>
			{
				var subscriptionSummary = subscriptionsAndGroupSummary.First(subscriptionAndGroupSummary => subscriptionAndGroupSummary.Id == summary.SubscriptionId);
				summary.Subscription = _mapper.Map<SubscriptionWithGroupSummary, Subscription>(subscriptionSummary);
				summary.GroupSummary = summary.GroupId != null ? subscriptionSummary.GroupSummary.First(group => group.Id == summary.GroupId) : null;
			});

			var skip = pageDetails.PageSize.HasValue ? pageDetails.PageSize.Value * (pageDetails.PageNo.Value - 1) : pageDetails.PageSize.GetValueOrDefault();
			return PagedResultSet<Models.Campaign.CampaignSummary>.Construct(campaignsSummary, skip, pageDetails.PageSize.GetValueOrDefault());
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="campaignId"></param>
		/// <returns></returns>
		public async Task PublishCampaign(Guid campaignId)
		{
			var campaign = await Repository.GetAsync(campaignId);
			campaign.Status = Omilos.Common.Enumerations.CampaignStatus.Active;

			await Repository.UpdateAsync(campaign);

			await RaiseDomainEventAsync(campaign.ExposedId, Omilos.Common.Enumerations.Events.Published, new Dictionary<string, object>
			{
				{ Constants.ServiceBus.MessageProperties.CampaignManagement.AdditionalProperties.CampaignStartDate, campaign.Schedule.StartDate.ToShortDateString() },
				{ Constants.ServiceBus.MessageProperties.CampaignManagement.AdditionalProperties.CampaignStartTime, campaign.Schedule.StartTime },
				{ Constants.ServiceBus.MessageProperties.CampaignManagement.AdditionalProperties.CampaignTimeZone, campaign.Schedule.TimeZone }
			});
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="campaignId"></param>
		/// <param name="groupId"></param>
		/// <returns></returns>
		public async Task AssignGroup(Guid campaignId, Guid groupId)
		{
			var campaign = await Repository.GetAsync(campaignId);
			var previousGroupId = campaign.GroupId;

			campaign.GroupId = groupId;

			await Repository.UpdateAsync(campaign);

			await _subscriptionServiceClient.MarkGroupAsAssigned(campaign.SubscriptionId, groupId, previousGroupId);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="campaignId"></param>
		/// <param name="campaignToBeUpdated"></param>
		/// <returns></returns>
		public async Task Patch(Guid campaignId, CampaignSelectiveMapping campaignToBeUpdated)
		{
			var campaign = await Repository.GetAsync(campaignId);

			var mappedCampaign = _mapper.Map<DomainModel.Campaign>(campaign);

			await Repository.UpdateAsync(campaign);

			await RaiseDomainEventAsync(campaign.ExposedId, Omilos.Common.Enumerations.Events.Published, new Dictionary<string, object>
			{
				{ Constants.ServiceBus.MessageProperties.CampaignManagement.AdditionalProperties.CampaignStartDate, campaign.Schedule.StartDate.ToShortDateString() },
				{ Constants.ServiceBus.MessageProperties.CampaignManagement.AdditionalProperties.CampaignStartTime, campaign.Schedule.StartTime },
				{ Constants.ServiceBus.MessageProperties.CampaignManagement.AdditionalProperties.CampaignTimeZone, campaign.Schedule.TimeZone }
			});
		}
	}
}