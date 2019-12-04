using Jaytas.Omilos.Common.Extensions;
using Jaytas.Omilos.Common.Models;
using Jaytas.Omilos.Web.Providers;
using Jaytas.Omilos.Web.Service.Subscription.Business.Interfaces;
using Jaytas.Omilos.Web.Service.Subscription.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Subscription.Business
{
	/// <summary>
	/// 
	/// </summary>
	public class GroupProvider : CrudByFieldBaseProvider<DomainModel.Group, IGroupRepository, long, Guid>, IGroupProvider
	{
		IContactRepository _contactRepository;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="groupRepository"></param>
		/// <param name="contactRepository"></param>
		public GroupProvider(IGroupRepository groupRepository, IContactRepository contactRepository) : base(groupRepository)
		{
			_contactRepository = contactRepository;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="domains"></param>
		/// <returns></returns>
		public async override Task AssertEntityToCreateIsValidAsync(IEnumerable<DomainModel.Group> domains)
		{
			//throw new NotImplementedException();
			await Task.CompletedTask;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="identifiers"></param>
		/// <returns></returns>
		public async override Task AssertEntityToDeleteIsValidAsync(IEnumerable<Guid> identifiers)
		{
			//throw new NotImplementedException();
			await Task.CompletedTask;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="domains"></param>
		/// <returns></returns>
		public async override Task AssertEntityToUpdateIsValidAsync(IEnumerable<DomainModel.Group> domains)
		{
			//throw new NotImplementedException();
			await Task.CompletedTask;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="groupId"></param>
		/// <returns></returns>
		public async Task<IEnumerable<DomainModel.GroupContactAssociation>> GetContacts(Guid groupId)
		{
			var group = await Repository.GetAsync(groupId);

			return group.GroupContactAssociations;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="subscriptionId"></param>
		/// <param name="groupId"></param>
		/// <param name="contacts"></param>
		/// <returns></returns>
		public async Task AddContactsToGroup(Guid subscriptionId, Guid groupId, IEnumerable<Guid> contacts)
		{
			var groupContactAssociations = from contact in contacts
										   select new DomainModel.GroupContactAssociation
										   {
											   GroupId = groupId,
											   ContactId = contact
										   };

			await Repository.AddContactsAsync(groupContactAssociations);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="subscriptionId"></param>
		/// <param name="groupId"></param>
		/// <param name="previuslyAssignedGroup"></param>
		/// <returns></returns>
		public async Task UpdateGroupAssignment(Guid subscriptionId, Guid groupId, Guid? previuslyAssignedGroup)
		{
			Expression<Func<DomainModel.Group, bool>> expression = x => x.SubscriptionId == subscriptionId && x.ExposedId == groupId;

			if(previuslyAssignedGroup.HasValue && previuslyAssignedGroup.Value != groupId)
			{
				expression.Or(x => x.SubscriptionId == subscriptionId && x.ExposedId == previuslyAssignedGroup);
			}

			var groups = await Repository.GetAsync(expression);

			if(!groups.Any())
			{
				return;
			}

			var groupToBeAssigned = groups.First(x => x.ExposedId == groupId);
			groupToBeAssigned.IsUsed = true;

			if (previuslyAssignedGroup.HasValue && previuslyAssignedGroup.Value != groupId)
			{
				var groupToBeUnAssigned = groups.First(x => x.ExposedId == previuslyAssignedGroup);
				groupToBeAssigned.IsUsed = false;
			}

			await Repository.UpdateAsync(groups);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pageDetails"></param>
		/// <param name="subscriptionId"></param>
		/// <returns></returns>
		public async Task<PagedResultSet<DomainModel.Group>> MyGroups(Models.Common.PageDetails pageDetails, Guid? subscriptionId)
		{
			Expression<Func<DomainModel.Group, bool>> expression = group => true;

			if (subscriptionId != null && subscriptionId != Guid.Empty)
			{
				expression = group => group.SubscriptionId == subscriptionId;
			}

			if (!string.IsNullOrWhiteSpace(pageDetails?.SearchText))
			{
				expression = expression.And(group => group.Name.Contains(pageDetails.SearchText));
			}

			var groups = await Repository.GetAsync(expression);

			var skip = pageDetails?.PageSize != null && pageDetails?.PageNo != null ?
					   pageDetails.PageSize.Value * (pageDetails.PageNo.Value - 1) :
					   pageDetails?.PageSize;
			return PagedResultSet<DomainModel.Group>.Construct(groups, skip, pageDetails?.PageSize);
		}
	}
}
