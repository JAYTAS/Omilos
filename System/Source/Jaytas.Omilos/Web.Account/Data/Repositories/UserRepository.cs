using Jaytas.Omilos.Web.Service.Account.Data.DbContext;
using Jaytas.Omilos.Web.Service.Account.Data.Repositories.Interfaces;
using Jaytas.Omilos.Web.Service.Account.DomainModel;
using Jaytas.Omilos.Web.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Account.Data.Repositories
{
	/// <summary>
	/// 
	/// </summary>
	public class UserRepository : CrudBaseEntityRepository<IAccountDbContext, User, long>, IUserRepository
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="userDbContext"></param>
		public UserRepository(IAccountDbContext userDbContext) : base(userDbContext, userDbContext.Users)
		{
		}
	}
}
