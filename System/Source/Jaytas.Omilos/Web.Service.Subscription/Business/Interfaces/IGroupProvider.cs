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
	public interface IGroupProvider : Providers.ICrudByFieldBaseProvider<DomainModel.Group, long, Guid>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="groupId"></param>
		/// <returns></returns>
		Task<IEnumerable<DomainModel.GroupContactAssociation>> GetContacts(Guid groupId);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="subscriptionId"></param>
		/// <param name="groupId"></param>
		/// <param name="contacts"></param>
		/// <returns></returns>
		Task AddContactsToGroup(Guid subscriptionId, Guid groupId, IEnumerable<Guid> contacts);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pageDetails"></param>
		/// <param name="subscriptionId"></param>
		/// <returns></returns>
		Task<PagedResultSet<DomainModel.Group>> MyGroups(Models.Common.PageDetails pageDetails, Guid? subscriptionId);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="subscriptionId"></param>
		/// <param name="groupId"></param>
		/// <param name="previuslyAssignedGroup"></param>
		/// <returns></returns>
		Task UpdateGroupAssignment(Guid subscriptionId, Guid groupId, Guid? previuslyAssignedGroup);
	}
}
