using Jaytas.Omilos.Common.Models;
using Jaytas.Omilos.Web.Service.Models.Subscription.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Subscription.Business.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface ISubscriptionProvider : Providers.ICrudByFieldBaseProvider<DomainModel.Subscription, long, Guid>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="identifierFilters"></param>
		/// <returns></returns>
		Task<IEnumerable<DomainModel.Subscription>> GetSubscriptionsAndGroupSummaryById(List<IdentifierFilter> identifierFilters);

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		Task<PagedResultSet<DomainModel.Subscription>> MySubscriptions(Models.Common.PageDetails pageDetails);
	}
}
