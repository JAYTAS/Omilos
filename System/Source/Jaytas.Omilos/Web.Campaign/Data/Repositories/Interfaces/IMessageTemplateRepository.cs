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
	public interface IMessageTemplateRepository : ICrudByFieldBaseEntityRepository<DomainModel.MessageTemplate, long, Guid>
	{
	}
}
