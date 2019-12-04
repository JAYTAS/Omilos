using Jaytas.Omilos.Data.EntityFramework.Interfaces;
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
	public interface ISubscriptionDbContext : IDbContext
	{
		/// <summary>
		/// 
		/// </summary>
		DbSet<DomainModel.Subscription> Subscriptions { get; set; }

		/// <summary>
		/// 
		/// </summary>
		DbSet<DomainModel.Group> Groups { get; set; }

		/// <summary>
		/// 
		/// </summary>
		DbSet<DomainModel.Contact> Contacts { get; set; }

		/// <summary>
		/// 
		/// </summary>
		DbSet<DomainModel.GroupContactAssociation> GroupContactAssociations { get; set; }
	}
}
