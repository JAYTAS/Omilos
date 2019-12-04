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
	public class Subscription : GuidFieldLongBaseEntity
	{
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual ICollection<Contact> Contacts { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual ICollection<Group> Groups { get; set; }
	}
}