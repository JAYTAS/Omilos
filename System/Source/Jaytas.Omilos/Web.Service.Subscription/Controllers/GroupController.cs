using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Jaytas.Omilos.Common;
using Jaytas.Omilos.Common.Web;
using Jaytas.Omilos.Web.Controllers;
using Jaytas.Omilos.Web.Controllers.Commands;
using Jaytas.Omilos.Web.Service.Models.Subscription.Input;
using Jaytas.Omilos.Web.Service.Subscription.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Service.Subscription.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	[Route(Constants.Route.Group.RootPath)]
	public class GroupController : CrudByFieldBaseApiController<Jaytas.Omilos.Web.Service.Subscription.DomainModel.Group,
																Group, Command<Group, Guid>, Guid, long>
	{
		readonly IGroupProvider _groupProvider;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="groupProvider"></param>
		/// <param name="mapper"></param>
		public GroupController(IGroupProvider groupProvider, IMapper mapper) : base(mapper)
		{
			_groupProvider = groupProvider;
		}

		/// <summary>
		/// Gets group.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[HttpHead]
		[Route(Constants.Route.Crud.Get, Name = Constants.Route.Group.Name.GetById)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType(typeof(Jaytas.Omilos.Web.Service.Models.Subscription.Group), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> Get(Guid subscriptionId, Guid id)
		{
			return await GetOrStatusCodeAsync(id).ConfigureAwait(true);
		}

		/// <summary>
		/// Gets all the group for the logged in user.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[HttpHead]
		[Route(Constants.Route.Group.GetGroupsBySubscription)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType(typeof(IEnumerable<Jaytas.Omilos.Web.Service.Models.Subscription.Group>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> MyGroups(Guid subscriptionId, [FromQuery] Jaytas.Omilos.Web.Service.Models.Common.PageDetails pageDetails)
		{
			return await ExecutePagedResultWithExceptionHandlingAsync<Jaytas.Omilos.Web.Service.Subscription.DomainModel.Group, List<Jaytas.Omilos.Web.Service.Models.Subscription.Group>>
								(() => _groupProvider.MyGroups(pageDetails, subscriptionId)).ConfigureAwait(true);
		}

		/// <summary>
		/// Gets all the group for the logged in user.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[HttpHead]
		[Route(Constants.Route.Group.GetContacts, Name = Constants.Route.Group.Name.GetContactsById)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType(typeof(IEnumerable<Jaytas.Omilos.Web.Service.Models.Subscription.ContactWithAssociationStatus>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetContacts(Guid subscriptionId, Guid id)
		{
			return await ExecuteWithExceptionHandlingAsync<IEnumerable<Jaytas.Omilos.Web.Service.Subscription.DomainModel.GroupContactAssociation>, 
														   IEnumerable<Jaytas.Omilos.Web.Service.Models.Subscription.ContactWithAssociationStatus>>(
															() => _groupProvider.GetContacts(id)).ConfigureAwait(true);
		}

		/// <summary>
		/// Creates group.
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		[Route(Constants.Route.Crud.Create)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> Post(Guid subscriptionId, [FromBody] Group group)
		{
			var commandProperties = new Dictionary<string, dynamic>
			{
				{ nameof(Jaytas.Omilos.Web.Service.Subscription.DomainModel.Group.SubscriptionId), subscriptionId }
			};
			return await PostOrStatusCodeAsync(group, commandProperties, Constants.Route.Group.Name.GetById).ConfigureAwait(true);
		}

		/// <summary>
		/// Updates group.
		/// </summary>
		/// <returns></returns>
		[HttpPut]
		[Route(Constants.Route.Crud.Update)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> Update(Guid subscriptionId, Guid id, [FromBody] Group group)
		{
			return await PutOrStatusCodeAsync(group, id).ConfigureAwait(true);
		}

		/// <summary>
		/// Deletes group.
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
		/// Creates group.
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		[Route(Constants.Route.Group.AddContactsToGroup)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> AddContactsToGroup(Guid subscriptionId, Guid id, [FromBody] IEnumerable<Guid> contacts)
		{
			var routeValues = new Dictionary<string, object>
											   {
												   { nameof(subscriptionId), subscriptionId },
												   { nameof(id), id }
											   };
			return await PostOrStatusCodeAsync(() => _groupProvider.AddContactsToGroup(subscriptionId, id, contacts),
											   Constants.Route.Group.Name.GetContactsById,
											   routeValues
											  ).ConfigureAwait(true);
		}

		/// <summary>
		/// Creates group.
		/// </summary>
		/// <returns></returns>
		[HttpPatch]
		[Route(Constants.Route.Group.MarkAsAssigned)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> MarkAsAssigned(Guid subscriptionId, Guid id, [FromQuery] Guid? previuslyAssignedGroup)
		{
			return await PutOrStatusCodeAsync(() => _groupProvider.UpdateGroupAssignment(subscriptionId, id, previuslyAssignedGroup)).ConfigureAwait(true);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		protected async override Task<Guid> CreateAsync(Jaytas.Omilos.Web.Service.Subscription.DomainModel.Group model)
		{
			return await _groupProvider.CreateAsync(model);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="model"></param>
		/// <param name="resourceId"></param>
		/// <returns></returns>
		protected override Command<Group, Guid> CreateCommand(Group model, Guid resourceId)
		{
			return new Command<Group, Guid>(model, resourceId);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="model"></param>
		/// <param name="resourceId"></param>
		/// <param name="commandProperties"></param>
		/// <returns></returns>
		protected override Command<Group, Guid> CreateCommand(Group model, Guid resourceId, Dictionary<string, dynamic> commandProperties)
		{
			return new Command<Group, Guid>(model, resourceId, commandProperties);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		protected async override Task DeleteAsync(Command<Group, Guid> command)
		{
			await _groupProvider.DeleteAsync(command.ResourceId);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		protected async override Task<IEnumerable<Jaytas.Omilos.Web.Service.Subscription.DomainModel.Group>> GetAllAsync(Command<Group, Guid> command)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		protected async override Task<Jaytas.Omilos.Web.Service.Subscription.DomainModel.Group> GetByIdAsync(Command<Group, Guid> command)
		{
			return await _groupProvider.GetAsync(command.ResourceId);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="command"></param>
		/// <param name="model"></param>
		/// <returns></returns>
		protected async override Task UpdateAsync(Command<Group, Guid> command, Jaytas.Omilos.Web.Service.Subscription.DomainModel.Group model)
		{
			await _groupProvider.UpdateAsync(model);
		}
	}
}