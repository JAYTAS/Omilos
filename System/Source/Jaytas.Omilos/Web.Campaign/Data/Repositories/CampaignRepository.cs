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
	public class CampaignRepository : CrudByFieldBaseEntityRepository<ICampaignDbContext, DomainModel.Campaign, long, Guid>, ICampaignRepository
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="campaignDbContext"></param>
		public CampaignRepository(ICampaignDbContext campaignDbContext) : base(campaignDbContext, campaignDbContext.Campaigns)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async override Task<DomainModel.Campaign> GetAsync(Guid id)
		{
			return (await GetAsync(campaign => campaign.ExposedId == id)).FirstOrDefault();
		}
	}
}
