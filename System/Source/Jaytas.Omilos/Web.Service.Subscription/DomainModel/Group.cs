using Jaytas.Omilos.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Subscription.DomainModel
{
	/// <summary>
	/// 
	/// </summary>
	public class Group : GuidFieldLongBaseEntity
	{
		/// <summary>
		/// 
		/// </summary>
		public Guid SubscriptionId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool IsUsed { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual Subscription Subscription { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual ICollection<GroupContactAssociation> GroupContactAssociations { get; set; }
	}
}
