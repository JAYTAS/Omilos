using Jaytas.Omilos.Web.Repositories;
using Jaytas.Omilos.Web.Service.Campaign.Data.DbContext;
using Jaytas.Omilos.Web.Service.Campaign.Data.Repositories.Interfaces;
using Jaytas.Omilos.Web.Service.Campaign.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Campaign.Data.Repositories
{
	/// <summary>
	/// 
	/// </summary>
	public class MessageTemplateRepository : CrudByFieldBaseEntityRepository<ICampaignDbContext, MessageTemplate, long, Guid>, IMessageTemplateRepository
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="campaignDbContext"></param>
		public MessageTemplateRepository(ICampaignDbContext campaignDbContext) : base(campaignDbContext, campaignDbContext.MessageTemplates)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async override Task<MessageTemplate> GetAsync(Guid id)
		{
			return (await GetAsync(messageTemplate => messageTemplate.ExposedId == id)).FirstOrDefault();
		}
	}
}
