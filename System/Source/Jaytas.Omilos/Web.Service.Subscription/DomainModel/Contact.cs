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
	public class Contact : GuidFieldLongBaseEntity
	{
		/// <summary>
		/// 
		/// </summary>
		public Guid SubscriptionId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string PhoneNumber { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string TimeZone { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string CustomColumn1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string CustomColumn2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string CustomColumn3 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string CustomColumn4 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string CustomColumn5 { get; set; }

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
