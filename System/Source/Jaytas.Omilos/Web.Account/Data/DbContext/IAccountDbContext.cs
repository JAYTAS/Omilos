using Jaytas.Omilos.Data.EntityFramework.Interfaces;
using Jaytas.Omilos.Web.Service.Account.DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Account.Data.DbContext
{
	/// <summary>
	/// User API IDbContext.
	/// </summary>
	public interface IAccountDbContext : IDbContext
	{
		/// <summary>
		/// DbSet for Users entity.
		/// </summary>
		DbSet<User> Users { get; set; }

		/// <summary>
		/// DbSet for UserRole entity.
		/// </summary>
		DbSet<UserRole> UserRoles { get; set; }

		/// <summary>
		/// DbSet for the Roles table.
		/// </summary>
		DbSet<Role> Roles { get; set; }
	}
}
