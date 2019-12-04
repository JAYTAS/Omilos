using Jaytas.Omilos.Common;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jaytas.Omilos.ServiceClient.Subscription.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface ISubscriptionClient
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="identifierFilters"></param>
		/// <returns></returns>
		[Post("/api/subscription/GetSubscriptionsAndGroupSummaryById")] //Constants.Route.Subscription.GetSubscriptionsAndGroupSummaryById
		Task<IEnumerable<Web.Service.Models.Subscription.SubscriptionWithGroupSummary>> GetSubscriptionsAndGroupSummaryById([Body] List<Web.Service.Models.Subscription.Input.IdentifierFilter> identifierFilters);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="subscriptionId"></param>
		/// <param name="id"></param>
		/// <param name="previuslyAssignedGroup"></param>
		/// <returns></returns>
		[Patch("/api/subscription/{subscriptionId}/group/{id}/MarkAsAssigned")]//Constants.Route.Group.MarkAsAssigned
		Task MarkGroupAsAssigned([AliasAs("subscriptionId")] Guid subscriptionId, [AliasAs("id")] Guid id, [AliasAs("previuslyAssignedGroup")] Guid? previuslyAssignedGroup);
	}
}