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
	public class CampaignInstanceRepository : CrudByFieldBaseEntityRepository<ICampaignDbContext, CampaignInstance, long, Guid>, ICampaignInstanceRepository
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="campaignDbContext"></param>
		public CampaignInstanceRepository(ICampaignDbContext campaignDbContext) : base(campaignDbContext, campaignDbContext.CampaignInstances)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async override Task<CampaignInstance> GetAsync(Guid id)
		{
			return (await GetAsync(campaignInstance => campaignInstance.ExposedId == id)).FirstOrDefault();
		}
	}
}
