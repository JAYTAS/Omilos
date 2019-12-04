using Jaytas.Omilos.Web.Service.Account.DomainModel;
using Jaytas.Omilos.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Account.Data.Repositories.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface IUserRepository : ICrudBaseEntityRepository<User, long>
	{
	}
}
