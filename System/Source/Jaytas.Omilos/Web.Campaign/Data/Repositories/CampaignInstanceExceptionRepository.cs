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
	public class CampaignInstanceExceptionRepository : CrudByFieldBaseEntityRepository<ICampaignDbContext, CampaignInstanceException, long, Guid>, ICampaignInstanceExceptionRepository
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="campaignDbContext"></param>
		public CampaignInstanceExceptionRepository(ICampaignDbContext campaignDbContext) : base(campaignDbContext, campaignDbContext.CampaignInstanceExceptions)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async override Task<CampaignInstanceException> GetAsync(Guid id)
		{
			return (await GetAsync(campaignInstanceException => campaignInstanceException.ExposedId == id)).FirstOrDefault();
		}
	}
}
