using AutoMapper;
using Jaytas.Omilos.Common.Extensions;
using Jaytas.Omilos.Common.Models;
using Jaytas.Omilos.Web.Providers;
using Jaytas.Omilos.Web.Service.Models.Subscription;
using Jaytas.Omilos.Web.Service.Models.Subscription.Input;
using Jaytas.Omilos.Web.Service.Subscription.Business.Interfaces;
using Jaytas.Omilos.Web.Service.Subscription.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Subscription.Business
{
	/// <summary>
	/// 
	/// </summary>
	public class SubscriptionProvider : CrudByFieldBaseProvider<DomainModel.Subscription, ISubscriptionRepository, long, Guid>, ISubscriptionProvider
	{
		readonly IMapper _mapper;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="subscriptionRepository"></param>
		/// <param name="mapper"></param>
		public SubscriptionProvider(ISubscriptionRepository subscriptionRepository, IMapper mapper) : base(subscriptionRepository)
		{
			_mapper = mapper;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="domains"></param>
		/// <returns></returns>
		public async override Task AssertEntityToCreateIsValidAsync(IEnumerable<DomainModel.Subscription> domains)
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
		public async override Task AssertEntityToUpdateIsValidAsync(IEnumerable<DomainModel.Subscription> domains)
		{
			//throw new NotImplementedException();
			await Task.CompletedTask;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="identifierFilters"></param>
		/// <returns></returns>
		public async Task<IEnumerable<DomainModel.Subscription>> GetSubscriptionsAndGroupSummaryById(List<IdentifierFilter> identifierFilters)
		{
			System.Linq.Expressions.Expression<Func<DomainModel.Subscription, bool>> expression =  x => false;

			foreach (var identifierFilter in identifierFilters)
			{
				expression = expression.Or(subscription => subscription.ExposedId == identifierFilter.SubscriptionId &&
											  (!identifierFilter.GroupId.HasValue ||
											   subscription.Groups.Any(group => group.ExposedId == identifierFilter.GroupId.Value)));
			}

			return await Repository.GetAsync(expression);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public async Task<PagedResultSet<DomainModel.Subscription>> MySubscriptions(Models.Common.PageDetails pageDetails)
		{
			Expression<Func<DomainModel.Subscription, bool>> expression = subscription => true;

			if (!string.IsNullOrWhiteSpace(pageDetails?.SearchText))
			{
				expression = expression.And(subscription => subscription.Name.Contains(pageDetails.SearchText));
			}

			var subscriptions = await Repository.GetAsync(expression);

			var skip = pageDetails?.PageSize != null && pageDetails?.PageNo != null ?
					   pageDetails.PageSize.Value * (pageDetails.PageNo.Value - 1) :
					   pageDetails?.PageSize;
			return PagedResultSet<DomainModel.Subscription>.Construct(subscriptions, skip, pageDetails?.PageSize);
		}
	}
}
