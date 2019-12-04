using Jaytas.Omilos.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Subscription.Business.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface IContactProvider : Providers.ICrudByFieldBaseProvider<DomainModel.Contact, long, Guid>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pageDetails"></param>
		/// <param name="subscriptionId"></param>
		/// <returns></returns>
		Task<PagedResultSet<DomainModel.Contact>> MyContacts(Models.Common.PageDetails pageDetails, Guid? subscriptionId);
	}
}
