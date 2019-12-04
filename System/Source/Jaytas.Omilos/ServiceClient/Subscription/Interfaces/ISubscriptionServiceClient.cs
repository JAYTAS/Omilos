using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jaytas.Omilos.ServiceClient.Subscription.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface ISubscriptionServiceClient
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="identifierFilters"></param>
		/// <returns></returns>
		Task<IEnumerable<Web.Service.Models.Subscription.SubscriptionWithGroupSummary>> GetSubscriptionsAndGroupSummaryByIdAsync(List<Web.Service.Models.Subscription.Input.IdentifierFilter> identifierFilters);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="subscriptionId"></param>
		/// <param name="groupId"></param>
		/// <param name="previuslyAssignedGroup"></param>
		/// <returns></returns>
		Task MarkGroupAsAssigned(Guid subscriptionId, Guid groupId, Guid? previuslyAssignedGroup);
	}
}
