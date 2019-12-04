using Jaytas.Omilos.Data.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Campaign.Data.DbContext
{
	/// <summary>
	/// 
	/// </summary>
	public interface ICampaignDbContext : IDbContext
	{
		/// <summary>
		/// 
		/// </summary>
		DbSet<DomainModel.Campaign> Campaigns { get; set; }

		/// <summary>
		/// 
		/// </summary>
		DbSet<DomainModel.CampaignInstance> CampaignInstances { get; set; }

		/// <summary>
		/// 
		/// </summary>
		DbSet<DomainModel.CampaignInstanceException> CampaignInstanceExceptions { get; set; }

		/// <summary>
		/// 
		/// </summary>
		DbSet<DomainModel.MessageTemplate> MessageTemplates { get; set; }

		/// <summary>
		/// 
		/// </summary>
		DbSet<DomainModel.Schedule> Schedules { get; set; }

		/// <summary>
		/// 
		/// </summary>
		DbSet<DomainModel.RecurrencePattern> RecurrencePatterns { get; set; }
	}
}
