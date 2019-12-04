using Jaytas.Omilos.Web.Repositories;
using Jaytas.Omilos.Web.Service.Subscription.Data.DbContext;
using Jaytas.Omilos.Web.Service.Subscription.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Subscription.Data.Repositories
{
	/// <summary>
	/// 
	/// </summary>
	public class SubscriptionRepository : CrudByFieldBaseEntityRepository<ISubscriptionDbContext, DomainModel.Subscription, long, Guid>, ISubscriptionRepository
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="subscriptionDbContext"></param>
		public SubscriptionRepository(ISubscriptionDbContext subscriptionDbContext) : base(subscriptionDbContext, subscriptionDbContext.Subscriptions)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async override Task<DomainModel.Subscription> GetAsync(Guid id)
		{
			return (await GetAsync(campaign => campaign.ExposedId == id)).FirstOrDefault();
		}
	}
}
