using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Jaytas.Omilos.Common;
using Jaytas.Omilos.Common.Web;
using Jaytas.Omilos.Web.Controllers;
using Jaytas.Omilos.Web.Controllers.Commands;
using Jaytas.Omilos.Web.Service.Subscription.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Service.Subscription.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	[Route(Constants.Route.Contact.RootPath)]
	public class ContactController : CrudByFieldBaseApiController<Jaytas.Omilos.Web.Service.Subscription.DomainModel.Contact,
																  Jaytas.Omilos.Web.Service.Models.Subscription.Input.Contact,
																  Command<Jaytas.Omilos.Web.Service.Models.Subscription.Input.Contact, Guid>,
																  Guid, long>
	{
		readonly IContactProvider _contactProvider;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="contactProvider"></param>
		/// <param name="mapper"></param>
		public ContactController(IContactProvider contactProvider, IMapper mapper) : base(mapper)
		{
			_contactProvider = contactProvider;
		}

		/// <summary>
		/// Gets contact.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[HttpHead]
		[Route(Constants.Route.Crud.Get, Name = Constants.Route.Contact.Name.GetById)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType(typeof(Jaytas.Omilos.Web.Service.Models.Subscription.Group), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> Get(Guid subscriptionId, Guid id)
		{
			return await GetOrStatusCodeAsync(id).ConfigureAwait(true);
		}

		/// <summary>
		/// Gets contact.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[HttpHead]
		[Route(Constants.Route.Contact.ContactsBySubscription)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType(typeof(Jaytas.Omilos.Web.Service.Models.Subscription.Group), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> ContactsBySubscription(Guid subscriptionId, Guid id, [FromQuery] Jaytas.Omilos.Web.Service.Models.Common.PageDetails pageDetails)
		{
			return await ExecutePagedResultWithExceptionHandlingAsync<Jaytas.Omilos.Web.Service.Subscription.DomainModel.Contact, List<Jaytas.Omilos.Web.Service.Models.Subscription.Contact>>
								(() => _contactProvider.MyContacts(pageDetails, subscriptionId)).ConfigureAwait(true);
		}

		/// <summary>
		/// Gets all the contact for the logged in user.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[HttpHead]
		[Route(Constants.Route.Contact.MyContacts)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType(typeof(IEnumerable<Jaytas.Omilos.Web.Service.Models.Subscription.ContactWithGroupDetails>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> MyContacts([FromQuery] Jaytas.Omilos.Web.Service.Models.Common.PageDetails pageDetails)
		{
			return await ExecutePagedResultWithExceptionHandlingAsync<Jaytas.Omilos.Web.Service.Subscription.DomainModel.Contact, List<Jaytas.Omilos.Web.Service.Models.Subscription.ContactWithGroupDetails>>
								(() => _contactProvider.MyContacts(pageDetails, null)).ConfigureAwait(true);
		}

		/// <summary>
		/// Creates contact.
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		[Route(Constants.Route.Crud.Create)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> Post(Guid subscriptionId, [FromBody] Jaytas.Omilos.Web.Service.Models.Subscription.Input.Contact contact)
		{
			var commandProperties = new Dictionary<string, dynamic>
			{
				{ nameof(Jaytas.Omilos.Web.Service.Subscription.DomainModel.Contact.SubscriptionId), subscriptionId }
			};

			return await PostOrStatusCodeAsync(contact, commandProperties, Constants.Route.Contact.Name.GetById).ConfigureAwait(true);
		}

		/// <summary>
		/// Updates contact.
		/// </summary>
		/// <returns></returns>
		[HttpPut]
		[Route(Constants.Route.Crud.Update)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> Update(Guid subscriptionId, Guid id, [FromBody] Jaytas.Omilos.Web.Service.Models.Subscription.Input.Contact contact)
		{
			return await PutOrStatusCodeAsync(contact, id).ConfigureAwait(true);
		}

		/// <summary>
		/// Deletes contact.
		/// </summary>
		/// <returns></returns>
		[HttpDelete]
		[Route(Constants.Route.Crud.Delete)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> Delete(Guid subscriptionId, Guid id)
		{
			return await DeleteOrStatusCodeAsync(id).ConfigureAwait(true);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		protected async override Task<Guid> CreateAsync(Jaytas.Omilos.Web.Service.Subscription.DomainModel.Contact model)
		{
			return await _contactProvider.CreateAsync(model);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="model"></param>
		/// <param name="resourceId"></param>
		/// <param name="commandProperties"></param>
		/// <returns></returns>
		protected override Command<Jaytas.Omilos.Web.Service.Models.Subscription.Input.Contact, Guid> CreateCommand(Jaytas.Omilos.Web.Service.Models.Subscription.Input.Contact model, Guid resourceId, Dictionary<string, dynamic> commandProperties)
		{
			return new Command<Jaytas.Omilos.Web.Service.Models.Subscription.Input.Contact, Guid>(model, resourceId, commandProperties);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="model"></param>
		/// <param name="resourceId"></param>
		/// <returns></returns>
		protected override Command<Jaytas.Omilos.Web.Service.Models.Subscription.Input.Contact, Guid> CreateCommand(Jaytas.Omilos.Web.Service.Models.Subscription.Input.Contact model, Guid resourceId)
		{
			return new Command<Jaytas.Omilos.Web.Service.Models.Subscription.Input.Contact, Guid>(model, resourceId);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		protected async override Task DeleteAsync(Command<Jaytas.Omilos.Web.Service.Models.Subscription.Input.Contact, Guid> command)
		{
			await _contactProvider.DeleteAsync(command.ResourceId);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		protected async override Task<IEnumerable<Jaytas.Omilos.Web.Service.Subscription.DomainModel.Contact>> GetAllAsync(Command<Jaytas.Omilos.Web.Service.Models.Subscription.Input.Contact, Guid> command)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		protected async override Task<Jaytas.Omilos.Web.Service.Subscription.DomainModel.Contact> GetByIdAsync(Command<Jaytas.Omilos.Web.Service.Models.Subscription.Input.Contact, Guid> command)
		{
			return await _contactProvider.GetAsync(command.ResourceId);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="command"></param>
		/// <param name="model"></param>
		/// <returns></returns>
		protected async override Task UpdateAsync(Command<Jaytas.Omilos.Web.Service.Models.Subscription.Input.Contact, Guid> command, Jaytas.Omilos.Web.Service.Subscription.DomainModel.Contact model)
		{
			await _contactProvider.UpdateAsync(model);
		}
	}
}