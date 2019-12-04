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
using Jaytas.Omilos.Web.Service.Campaign.Business.Interfaces;
using Jaytas.Omilos.Web.Service.Models.Campaign.Input;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Service.Campaign.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	[Route(Constants.Route.CampaignInstanceException.RootPath)]
	public class CampaignInstanceExceptionController : CrudByFieldBaseApiController<Jaytas.Omilos.Web.Service.Campaign.DomainModel.CampaignInstanceException,
																				   CampaignInstanceException,
																				   Command<CampaignInstanceException, Guid>,
																				   Guid, long>
	{
		readonly ICampaignInstanceExceptionProvider _campaignInstaneExceptionProvider;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="campaignInstaneExceptionProvider"></param>
		/// <param name="mapper"></param>
		public CampaignInstanceExceptionController(ICampaignInstanceExceptionProvider campaignInstaneExceptionProvider, IMapper mapper) : base(mapper)
		{
			_campaignInstaneExceptionProvider = campaignInstaneExceptionProvider;
		}


		/// <summary>
		/// Gets campaign instance exception.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[HttpHead]
		[Route(Constants.Route.Crud.Get, Name = Constants.Route.CampaignInstanceException.Name.GetById)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		[ProducesResponseType(typeof(Jaytas.Omilos.Web.Service.Models.Campaign.CampaignInstanceException), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> Get(Guid subscriptionId, Guid campaignId, Guid instanceId, Guid id)
		{
			return await GetOrStatusCodeAsync(id).ConfigureAwait(true);
		}

		/// <summary>
		/// Creates campaign instance exception.
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		[Route(Constants.Route.Crud.Create)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> Post(Guid subscriptionId, Guid campaignId, Guid instanceId, [FromBody] CampaignInstanceException campaignInstanceException)
		{
			var commandProperties = new Dictionary<string, dynamic>
			{
				{ nameof(Jaytas.Omilos.Web.Service.Campaign.DomainModel.CampaignInstanceException.InstanceId), instanceId }
			};

			return await PostOrStatusCodeAsync(campaignInstanceException, commandProperties, Constants.Route.CampaignInstanceException.Name.GetById).ConfigureAwait(true);
		}

		/// <summary>
		/// Updates campaign instance exception.
		/// </summary>
		/// <returns></returns>
		[HttpPut]
		[Route(Constants.Route.Crud.Update)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> Update(Guid subscriptionId, Guid campaignId, Guid instanceId, Guid id, [FromBody] CampaignInstanceException campaignInstanceException)
		{
			return await PutOrStatusCodeAsync(campaignInstanceException, id).ConfigureAwait(true);
		}

		/// <summary>
		/// Deletes campaign instance exception.
		/// </summary>
		/// <returns></returns>
		[HttpDelete]
		[Route(Constants.Route.Crud.Delete)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public async Task<IActionResult> Delete(Guid subscriptionId, Guid campaignId, Guid instanceId, Guid id)
		{
			return await DeleteOrStatusCodeAsync(id).ConfigureAwait(true);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		protected async override Task<Guid> CreateAsync(Jaytas.Omilos.Web.Service.Campaign.DomainModel.CampaignInstanceException model)
		{
			return await _campaignInstaneExceptionProvider.CreateAsync(model);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="model"></param>
		/// <param name="resourceId"></param>
		/// <returns></returns>
		protected override Command<CampaignInstanceException, Guid> CreateCommand(CampaignInstanceException model, Guid resourceId)
		{
			return new Command<CampaignInstanceException, Guid>(model, resourceId);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="model"></param>
		/// <param name="resourceId"></param>
		/// <param name="commandProperties"></param>
		/// <returns></returns>
		protected override Command<CampaignInstanceException, Guid> CreateCommand(CampaignInstanceException model, Guid resourceId, Dictionary<string, dynamic> commandProperties)
		{
			return new Command<CampaignInstanceException, Guid>(model, resourceId, commandProperties);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		protected async override Task DeleteAsync(Command<CampaignInstanceException, Guid> command)
		{
			await _campaignInstaneExceptionProvider.DeleteAsync(command.ResourceId);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		protected async override Task<IEnumerable<Jaytas.Omilos.Web.Service.Campaign.DomainModel.CampaignInstanceException>> GetAllAsync(Command<CampaignInstanceException, Guid> command)
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		protected async override Task<Jaytas.Omilos.Web.Service.Campaign.DomainModel.CampaignInstanceException> GetByIdAsync(Command<CampaignInstanceException, Guid> command)
		{
			return await _campaignInstaneExceptionProvider.GetAsync(command.ResourceId);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="command"></param>
		/// <param name="model"></param>
		/// <returns></returns>
		protected async override Task UpdateAsync(Command<CampaignInstanceException, Guid> command, Jaytas.Omilos.Web.Service.Campaign.DomainModel.CampaignInstanceException model)
		{
			await _campaignInstaneExceptionProvider.UpdateAsync(model);
		}
	}
}