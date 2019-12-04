using Jaytas.Omilos.Common;
using Jaytas.Omilos.Data.EntityFramework.BaseImplementations;
using Jaytas.Omilos.Web.Service.Campaign.Data.Map;
using Jaytas.Omilos.Web.Service.Campaign.DomainModel;
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
	public class CampaignDbContext : MicroServiceDbContext<CampaignDbContext>, ICampaignDbContext
	{
		/// <summary>
		/// Constructor which sets the db Initializer.
		/// </summary>
		/// <param name="options">Db context options.</param>
		public CampaignDbContext(DbContextOptions<CampaignDbContext> options) : base(options)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		public DbSet<DomainModel.Campaign> Campaigns { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DbSet<CampaignInstance> CampaignInstances { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DbSet<CampaignInstanceException> CampaignInstanceExceptions { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DbSet<MessageTemplate> MessageTemplates { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DbSet<Schedule> Schedules { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DbSet<RecurrencePattern> RecurrencePatterns { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="modelBuilder"></param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new CampaignFluentMap(Constants.Schemas.Campaign.Tables.Campaign, Constants.Schemas.Campaign.Name, true));
			modelBuilder.ApplyConfiguration(new CampaignInstanceFluentMap(Constants.Schemas.Campaign.Tables.CampaignInstance, Constants.Schemas.Campaign.Name, true));
			modelBuilder.ApplyConfiguration(new CampaignInstanceExceptionFluentMap(Constants.Schemas.Campaign.Tables.CampaignInstanceException, Constants.Schemas.Campaign.Name, true));
			modelBuilder.ApplyConfiguration(new MessageTemplateFluentMap(Constants.Schemas.Campaign.Tables.MessageTemplate, Constants.Schemas.Campaign.Name, true));
			modelBuilder.ApplyConfiguration(new ScheduleFluentMap(Constants.Schemas.Campaign.Tables.Schedule, Constants.Schemas.Campaign.Name, true));
			modelBuilder.ApplyConfiguration(new RecurrencePatternFluentMap(Constants.Schemas.Campaign.Tables.ScheduleRecurrencePattern, Constants.Schemas.Campaign.Name, true));
		}
	}
}