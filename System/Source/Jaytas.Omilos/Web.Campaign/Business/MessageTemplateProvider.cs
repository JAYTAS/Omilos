using Jaytas.Omilos.Web.Providers;
using Jaytas.Omilos.Web.Service.Campaign.Business.Interfaces;
using Jaytas.Omilos.Web.Service.Campaign.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Campaign.Business
{
	/// <summary>
	/// 
	/// </summary>
	public class MessageTemplateProvider : CrudByFieldBaseProvider<DomainModel.MessageTemplate, IMessageTemplateRepository, long, Guid>, IMessageTemplateProvider
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="messageTemplateRepository"></param>
		public MessageTemplateProvider(IMessageTemplateRepository messageTemplateRepository) : base(messageTemplateRepository)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="domains"></param>
		/// <returns></returns>
		public async override Task AssertEntityToCreateIsValidAsync(IEnumerable<DomainModel.MessageTemplate> domains)
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
		public async override Task AssertEntityToUpdateIsValidAsync(IEnumerable<DomainModel.MessageTemplate> domains)
		{
			//throw new NotImplementedException();
			await Task.CompletedTask;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="campaignId"></param>
		/// <returns></returns>
		public async Task<IEnumerable<DomainModel.MessageTemplate>> GetCampaignMessages(Guid campaignId)
		{
			return await Repository.GetAsync(messageTemplate => messageTemplate.CampaignId == campaignId);
		}
	}
}
