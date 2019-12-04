using AutoMapper;
using Jaytas.Omilos.Web.Controllers.Commands;
using Jaytas.Omilos.Web.Mapping.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaytas.Omilos.Web.Service.Campaign.App_Start
{
	/// <summary>
	/// 
	/// </summary>
	public class AutoMapperConfigurations
	{
		/// <summary>
		/// Registers both middle tier and web tier maps since our portal is currently running as a two-
		/// tier system (web &amp; data).
		/// </summary>
		/// <returns>Mapper</returns>
		public static IMapper RegisterMaps()
		{
			var mapperConfiguration = new MapperConfiguration(config =>
			{
				config.AddProfile(new GlobalMappingProfile());
				config.AddProfile(new WebProfile());
			});

			return mapperConfiguration.CreateMapper();
		}

		/// <summary>
		/// Registers the mappings used by the web tier to go to and from business models.
		/// </summary>
		///
		/// <seealso cref="T:AutoMapper.Profile"/>
		internal class WebProfile : Profile
		{
			public WebProfile()
			{
				CreateMap<DomainModel.Campaign, Models.Campaign.Campaign>().ForMember(api => api.Id, domain => domain.MapFrom(dom => dom.ExposedId));
				CreateMap<DomainModel.Campaign, Models.Campaign.CampaignSummary>()
										.ForMember(api => api.Id, domain => domain.MapFrom(dom => dom.ExposedId))
										.ForMember(api => api.ScheduleSummary, domain => domain.MapFrom(dom => dom.Schedule));

				CreateMap<Models.Campaign.Input.Campaign, DomainModel.Campaign>();
				CreateMap<Command<Models.Campaign.Input.Campaign, Guid>, DomainModel.Campaign>()
										.ForMember(dom => dom.SubscriptionId, command => command.MapFrom(api => api.CommandProperties.GetValueOrDefault(nameof(DomainModel.Campaign.SubscriptionId), default(Guid))));

				CreateMap<DomainModel.CampaignInstance, Models.Campaign.CampaignInstance>().ForMember(api => api.Id, domain => domain.MapFrom(dom => dom.ExposedId));

				CreateMap<Models.Campaign.Input.CampaignInstance, DomainModel.CampaignInstance>();
				CreateMap<Command<Models.Campaign.Input.CampaignInstance, Guid>, DomainModel.CampaignInstance>()
										.ForMember(dom => dom.CampaignId, command => command.MapFrom(api => api.CommandProperties.GetValueOrDefault(nameof(DomainModel.CampaignInstance.CampaignId), default(Guid))));

				CreateMap<DomainModel.CampaignInstanceException, Models.Campaign.CampaignInstanceException>().ForMember(api => api.Id, domain => domain.MapFrom(dom => dom.ExposedId));

				CreateMap<Models.Campaign.Input.CampaignInstanceException, DomainModel.CampaignInstanceException>();
				CreateMap<Command<Models.Campaign.Input.CampaignInstanceException, Guid>, DomainModel.CampaignInstanceException>()
										.ForMember(dom => dom.InstanceId, command => command.MapFrom(api => api.CommandProperties.GetValueOrDefault(nameof(DomainModel.CampaignInstanceException.InstanceId), default(Guid))));

				CreateMap<DomainModel.MessageTemplate, Models.Campaign.MessageTemplate>().ForMember(api => api.Id, domain => domain.MapFrom(dom => dom.ExposedId));

				CreateMap<Models.Campaign.Input.MessageTemplate, DomainModel.MessageTemplate>();
				CreateMap<Command<Models.Campaign.Input.MessageTemplate, Guid>, DomainModel.MessageTemplate>()
										.ForMember(dom => dom.CampaignId, command => command.MapFrom(api => api.CommandProperties.GetValueOrDefault(nameof(DomainModel.MessageTemplate.CampaignId), default(Guid))));

				CreateMap<DomainModel.Schedule, Models.Campaign.Schedule>().ForMember(api => api.Id, domain => domain.MapFrom(dom => dom.ExposedId));
				CreateMap<DomainModel.Schedule, Models.Campaign.ScheduleSummary>().ForMember(api => api.Id, domain => domain.MapFrom(dom => dom.ExposedId));

				CreateMap<Models.Campaign.Input.Schedule, DomainModel.Schedule>();
				CreateMap<Models.Campaign.RecurrencePattern, DomainModel.RecurrencePattern>();
				CreateMap<Command<Models.Campaign.Input.Schedule, Guid>, DomainModel.Schedule>()
										.ForMember(dom => dom.CampaignId, command => command.MapFrom(api => api.CommandProperties.GetValueOrDefault(nameof(DomainModel.Schedule.CampaignId), default(Guid))));

				CreateMap<Models.Subscription.SubscriptionWithGroupSummary, Models.Subscription.Subscription>();
				CreateMap<Models.Subscription.SubscriptionWithGroupSummary, Models.Subscription.GroupSummary>()
													.ForMember(api => api.Id, domain => domain.MapFrom(dom => dom.GroupSummary.First().Id))
													.ForMember(api => api.Name, domain => domain.MapFrom(dom => dom.GroupSummary.First().Name))
													.ForMember(api => api.NumberOfContacts, domain => domain.MapFrom(dom => dom.GroupSummary.First().NumberOfContacts));
			}

			/// <summary>
			/// Gets the name of the profile.
			/// </summary>
			/// <value>
			/// The name of the profile.
			/// </value>
			public override string ProfileName => typeof(WebProfile).FullName;
		}
	}
}
