using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Jaytas.Omilos.Common;
using Jaytas.Omilos.Common.Web;
using Jaytas.Omilos.Web.Controllers;
using Jaytas.Omilos.Web.Controllers.Commands;
using Jaytas.Omilos.Web.Service.Models.Subscription;
using Jaytas.Omilos.Web.Service.Models.Subscription.Input;
using Jaytas.Omilos.Web.Service.Subscription.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Service.Subscription.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	[Route(Constants.Route.Subscription.RootPath)]
	public class SubscriptionController : CrudByFieldBaseApiController<Jaytas.Omilos.Web.Service.Subscription.DomainModel.Subscription,
																	   Jaytas.Omilos.Web.Service.Models.Subscription.Input.Subscription,
																       Command<Jaytas.Omilos.Web.Service.Models.Subscription.Input.Subscription, Guid>,
																       Guid, long>
	{
		readonly ISubscriptionProvider _subscriptionProvider;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="subscriptionProvider"></param>
		/// <param name="mapper"></param>
		public SubscriptionController(ISubscriptionProvider subscriptionProvider, IMapper mapper) : base(mapper)
		{
			_subscriptionProvider = subscriptionProvider;
		}

		/// <summary>
		/// Gets subscription.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[HttpHead]
		[Route(Constants.Route.Crud.Get, Name = Constants.Route.Subscription.Name.GetById)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType(typeof(Jaytas.Omilos.Web.Service.Models.Subscription.Subscription), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> Get(Guid id)
		{
			return await GetOrStatusCodeAsync(id).ConfigureAwait(true);
		}

		/// <summary>
		/// Gets all the subscription for the logged in user.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[HttpHead]
		[Route(Constants.Route.Subscription.MySubscriptions)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType(typeof(IEnumerable<Jaytas.Omilos.Web.Service.Models.Subscription.Subscription>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> MySubscriptions()
		{
			return await ExecutePagedResultWithExceptionHandlingAsync<Jaytas.Omilos.Web.Service.Subscription.DomainModel.Subscription, List<Jaytas.Omilos.Web.Service.Models.Subscription.Subscription>>
								(() => _subscriptionProvider.MySubscriptions(null)).ConfigureAwait(true);
		}

		/// <summary>
		/// Gets all the subscription for the logged in user.
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		[Route(Constants.Route.Subscription.GetSubscriptionsAndGroupSummaryById)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType(typeof(IEnumerable<SubscriptionWithGroupSummary>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetSubscriptionsAndGroupSummaryById([FromBody] List<IdentifierFilter> identifierFilters)
		{
			return await ExecuteWithExceptionHandlingAsync<IEnumerable<Jaytas.Omilos.Web.Service.Subscription.DomainModel.Subscription>, 
														   IEnumerable<SubscriptionWithGroupSummary>>
														   (() => _subscriptionProvider.GetSubscriptionsAndGroupSummaryById(identifierFilters)).ConfigureAwait(true);
		}

		/// <summary>
		/// Creates subscription.
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		[Route(Constants.Route.Crud.Create)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> Post([FromBody] Jaytas.Omilos.Web.Service.Models.Subscription.Input.Subscription subscription)
		{
			return await PostOrStatusCodeAsync(subscription, Constants.Route.Subscription.Name.GetById).ConfigureAwait(true);
		}

		/// <summary>
		/// Updates subscription.
		/// </summary>
		/// <returns></returns>
		[HttpPut]
		[Route(Constants.Route.Crud.Update)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> Update(Guid id, [FromBody] Jaytas.Omilos.Web.Service.Models.Subscription.Input.Subscription subscription)
		{
			return await PutOrStatusCodeAsync(subscription, id).ConfigureAwait(true);
		}

		/// <summary>
		/// Deletes subscription.
		/// </summary>
		/// <returns></returns>
		[HttpDelete]
		[Route(Constants.Route.Crud.Delete)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> Delete(Guid id)
		{
			return await DeleteOrStatusCodeAsync(id).ConfigureAwait(true);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		protected async override Task<Guid> CreateAsync(Jaytas.Omilos.Web.Service.Subscription.DomainModel.Subscription model)
		{
			return await _subscriptionProvider.CreateAsync(model);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="model"></param>
		/// <param name="resourceId"></param>
		/// <returns></returns>
		protected override Command<Jaytas.Omilos.Web.Service.Models.Subscription.Input.Subscription, Guid> CreateCommand(Jaytas.Omilos.Web.Service.Models.Subscription.Input.Subscription model, Guid resourceId)
		{
			return new Command<Jaytas.Omilos.Web.Service.Models.Subscription.Input.Subscription, Guid>(model, resourceId);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="model"></param>
		/// <param name="commandProperties"></param>
		/// <param name="resourceId"></param>
		/// <returns></returns>
		protected override Command<Jaytas.Omilos.Web.Service.Models.Subscription.Input.Subscription, Guid> CreateCommand(Jaytas.Omilos.Web.Service.Models.Subscription.Input.Subscription model,
																												   Guid resourceId, Dictionary<string, dynamic> commandProperties)
		{
			return new Command<Jaytas.Omilos.Web.Service.Models.Subscription.Input.Subscription, Guid>(model, resourceId, commandProperties);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		protected async override Task DeleteAsync(Command<Jaytas.Omilos.Web.Service.Models.Subscription.Input.Subscription, Guid> command)
		{
			await _subscriptionProvider.DeleteAsync(command.ResourceId);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		protected async override Task<IEnumerable<Jaytas.Omilos.Web.Service.Subscription.DomainModel.Subscription>> GetAllAsync(Command<Jaytas.Omilos.Web.Service.Models.Subscription.Input.Subscription, Guid> command)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		protected async override Task<Jaytas.Omilos.Web.Service.Subscription.DomainModel.Subscription> GetByIdAsync(Command<Jaytas.Omilos.Web.Service.Models.Subscription.Input.Subscription, Guid> command)
		{
			return await _subscriptionProvider.GetAsync(command.ResourceId);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="command"></param>
		/// <param name="model"></param>
		/// <returns></returns>
		protected async override Task UpdateAsync(Command<Jaytas.Omilos.Web.Service.Models.Subscription.Input.Subscription, Guid> command, Jaytas.Omilos.Web.Service.Subscription.DomainModel.Subscription model)
		{
			await _subscriptionProvider.UpdateAsync(model);
		}
	}
}