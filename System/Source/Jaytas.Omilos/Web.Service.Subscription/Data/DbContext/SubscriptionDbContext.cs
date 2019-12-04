using Jaytas.Omilos.Common;
using Jaytas.Omilos.Data.EntityFramework.BaseImplementations;
using Jaytas.Omilos.Web.Service.Subscription.Data.Map;
using Jaytas.Omilos.Web.Service.Subscription.DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Subscription.Data.DbContext
{
	/// <summary>
	/// 
	/// </summary>
	public class SubscriptionDbContext : MicroServiceDbContext<SubscriptionDbContext>, ISubscriptionDbContext
	{
		/// <summary>
		/// Constructor which sets the db Initializer.
		/// </summary>
		/// <param name="options">Db context options.</param>
		public SubscriptionDbContext(DbContextOptions<SubscriptionDbContext> options) : base(options)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		public DbSet<DomainModel.Subscription> Subscriptions { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DbSet<Group> Groups { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DbSet<Contact> Contacts { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DbSet<GroupContactAssociation> GroupContactAssociations { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="modelBuilder"></param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new SubscriptionFluentMap(Constants.Schemas.Subscription.Tables.Subscription, Constants.Schemas.Subscription.Name, true));
			modelBuilder.ApplyConfiguration(new GroupFluentMap(Constants.Schemas.Subscription.Tables.Group, Constants.Schemas.Subscription.Name, true));
			modelBuilder.ApplyConfiguration(new ContactFluentMap(Constants.Schemas.Subscription.Tables.Contact, Constants.Schemas.Subscription.Name, true));
			modelBuilder.ApplyConfiguration(new GroupContactAssociationFluentMap(Constants.Schemas.Subscription.Tables.GroupContactAssociation, Constants.Schemas.Subscription.Name));
		}
	}
}
