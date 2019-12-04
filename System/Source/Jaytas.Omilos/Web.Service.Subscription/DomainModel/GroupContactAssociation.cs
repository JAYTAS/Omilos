using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Subscription.DomainModel
{
	/// <summary>
	/// 
	/// </summary>
	public class GroupContactAssociation
	{
		/// <summary>
		/// 
		/// </summary>
		public Guid GroupId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid ContactId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool HasOptedOut { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool HasWelcomeMessageSent { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual Group Group { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual Contact Contact { get; set; }
	}
}
