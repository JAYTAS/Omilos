using AutoMapper;
using Jaytas.Omilos.Web.Controllers.Commands;
using Jaytas.Omilos.Web.Mapping.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Subscription.App_Start
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
				CreateMap<DomainModel.Subscription, Models.Subscription.Subscription>().ForMember(api => api.Id, domain => domain.MapFrom(dom => dom.ExposedId));
				CreateMap<DomainModel.Subscription, Models.Subscription.SubscriptionWithGroupSummary>()
														.ForMember(api => api.Id, domain => domain.MapFrom(dom => dom.ExposedId))
														.ForMember(api => api.GroupSummary, dom => dom.ResolveUsing(Map));

				CreateMap<Models.Subscription.Input.Subscription, DomainModel.Subscription>();

				CreateMap<DomainModel.Group, Models.Subscription.Group>().ForMember(api => api.Id, domain => domain.MapFrom(dom => dom.ExposedId));

				CreateMap<Models.Subscription.Input.Group, DomainModel.Group>();
				CreateMap<Command<Models.Subscription.Input.Group, Guid>, DomainModel.Group>()
										.ForMember(dom => dom.SubscriptionId, command => command.MapFrom(api => api.CommandProperties.GetValueOrDefault(nameof(DomainModel.Group.SubscriptionId), default(Guid))));

				CreateMap<DomainModel.GroupContactAssociation, Models.Subscription.ContactWithAssociationStatus>()
							.ForMember(api => api.Id, dom => dom.MapFrom(model => model.Contact.ExposedId))
							.ForMember(api => api.FirstName, dom => dom.MapFrom(model => model.Contact.FirstName))
							.ForMember(api => api.LastName, dom => dom.MapFrom(model => model.Contact.LastName))
							.ForMember(api => api.Email, dom => dom.MapFrom(model => model.Contact.Email))
							.ForMember(api => api.PhoneNumber, dom => dom.MapFrom(model => model.Contact.PhoneNumber))
							.ForMember(api => api.CustomColumn1, dom => dom.MapFrom(model => model.Contact.CustomColumn1))
							.ForMember(api => api.CustomColumn2, dom => dom.MapFrom(model => model.Contact.CustomColumn2))
							.ForMember(api => api.CustomColumn3, dom => dom.MapFrom(model => model.Contact.CustomColumn3))
							.ForMember(api => api.CustomColumn4, dom => dom.MapFrom(model => model.Contact.CustomColumn4))
							.ForMember(api => api.CustomColumn5, dom => dom.MapFrom(model => model.Contact.CustomColumn5));

				CreateMap<DomainModel.Contact, Models.Subscription.Contact>().ForMember(api => api.Id, domain => domain.MapFrom(dom => dom.ExposedId));

				CreateMap<DomainModel.Contact, Models.Subscription.ContactWithGroupDetails>()
						   .ForMember(api => api.Id, domain => domain.MapFrom(dom => dom.ExposedId))
						   .ForMember(api => api.Groups, domain => domain.ResolveUsing(Map));

				CreateMap<Models.Subscription.Input.Contact, DomainModel.Contact>();
				CreateMap<Command<Models.Subscription.Input.Contact, Guid>, DomainModel.Contact>()
										.ForMember(dom => dom.SubscriptionId, command => command.MapFrom(api => api.CommandProperties.GetValueOrDefault(nameof(DomainModel.Contact.SubscriptionId), default(Guid))));
			}

			/// <summary>
			/// Gets the name of the profile.
			/// </summary>
			/// <value>
			/// The name of the profile.
			/// </value>
			public override string ProfileName => typeof(WebProfile).FullName;
			
			/// <summary>
			/// 
			/// </summary>
			/// <param name="subscription"></param>
			/// <returns></returns>
			private List<Models.Subscription.GroupSummary> Map(DomainModel.Subscription subscription)
			{
				if(subscription.Groups == null || subscription.Groups.Count() < 1)
				{
					return null;
				}

				return subscription.Groups.Select(group => new Models.Subscription.GroupSummary
				{
					Id = group.ExposedId,
					Name = group.Name,
					NumberOfContacts = group.GroupContactAssociations.Count()
				}).ToList();
			}

			/// <summary>
			/// 
			/// </summary>
			/// <param name="contact"></param>
			/// <returns></returns>
			private List<Models.Subscription.Group> Map(DomainModel.Contact contact)
			{
				if(!contact.GroupContactAssociations.Any())
				{
					return new List<Models.Subscription.Group>();
				}

				return contact.GroupContactAssociations.Where(asso => !asso.HasOptedOut).Select(asso => new Models.Subscription.Group
				{
					Id = asso.GroupId,
					Name = asso.Group.Name
				}).ToList();
			}
		}
	}
}
