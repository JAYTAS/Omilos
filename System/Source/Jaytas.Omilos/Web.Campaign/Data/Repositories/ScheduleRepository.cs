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
	public class ScheduleRepository : CrudByFieldBaseEntityRepository<ICampaignDbContext, Schedule, long, Guid>, IScheduleRepository
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="campaignDbContext"></param>
		public ScheduleRepository(ICampaignDbContext campaignDbContext) : base(campaignDbContext, campaignDbContext.Schedules)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async override Task<Schedule> GetAsync(Guid id)
		{
			return (await GetAsync(schedule => schedule.ExposedId == id)).FirstOrDefault();
		}
	}
}
