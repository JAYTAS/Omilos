using Jaytas.Omilos.Web.Repositories;
using Jaytas.Omilos.Web.Service.Subscription.Data.DbContext;
using Jaytas.Omilos.Web.Service.Subscription.Data.Repositories.Interfaces;
using Jaytas.Omilos.Web.Service.Subscription.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Subscription.Data.Repositories
{
	/// <summary>
	/// 
	/// </summary>
	public class GroupRepository : CrudByFieldBaseEntityRepository<ISubscriptionDbContext, DomainModel.Group, long, Guid>, IGroupRepository
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="subscriptionDbContext"></param>
		public GroupRepository(ISubscriptionDbContext subscriptionDbContext) : base(subscriptionDbContext, subscriptionDbContext.Groups)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="groupContactAssociations"></param>
		/// <returns></returns>
		public async Task AddContactsAsync(IEnumerable<GroupContactAssociation> groupContactAssociations)
		{
			await DbContext.GroupContactAssociations.AddRangeAsync(groupContactAssociations);
			await DbContext.SaveChangesAsync();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async override Task<Group> GetAsync(Guid id)
		{
			return (await GetAsync(group => group.ExposedId == id)).FirstOrDefault();
		}
	}
}