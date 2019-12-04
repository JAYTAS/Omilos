using Jaytas.Omilos.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Campaign.Data.Repositories.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface IScheduleRepository : ICrudByFieldBaseEntityRepository<DomainModel.Schedule, long, Guid>
	{
	}
}
