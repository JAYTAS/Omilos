using Jaytas.Omilos.Common;
using Jaytas.Omilos.Data.EntityFramework.BaseImplementations;
using Jaytas.Omilos.Web.Service.Account.Data.Map;
using Jaytas.Omilos.Web.Service.Account.DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Account.Data.DbContext
{
	/// <summary>
	/// DbContext for the User Management Web API
	/// </summary>
	public class AccountDbContext : MicroServiceDbContext<AccountDbContext>, IAccountDbContext
	{
		/// <summary>
		/// Constructor which sets the db Initializer.
		/// </summary>
		/// <param name="options">Represents the Identity of the logged in user.</param>
		public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
		{
		}

		/// <summary>
		/// DbSet for Roles entity.
		/// </summary>
		public virtual DbSet<Role> Roles { get; set; }

		/// <summary>
		/// DbSet for Users entity.
		/// </summary>
		public virtual DbSet<User> Users { get; set; }

		/// <summary>
		/// DbSet for UserRole entity.
		/// </summary>
		public virtual DbSet<UserRole> UserRoles { get; set; }

		/// <summary>
		/// DbSet for UserLoginDetail entity.
		/// </summary>
		public virtual DbSet<UserLoginDetail> UserLoginDetails { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="modelBuilder"></param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new RoleFluentMap(Constants.Schemas.Account.Tables.Role, Constants.Schemas.Account.Name, true));
			modelBuilder.ApplyConfiguration(new UserFluentMap(Constants.Schemas.Account.Tables.User, Constants.Schemas.Account.Name, true));
			modelBuilder.ApplyConfiguration(new UserLoginDetailFluentMap(Constants.Schemas.Account.Tables.UserLoginDetail, Constants.Schemas.Account.Name, false));
			modelBuilder.ApplyConfiguration(new UserRoleFluentMap(Constants.Schemas.Account.Tables.UserRole, Constants.Schemas.Account.Name, true));
		}
	}
}
