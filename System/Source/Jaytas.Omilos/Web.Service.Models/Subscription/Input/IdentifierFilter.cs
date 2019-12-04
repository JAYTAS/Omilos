using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Web.Service.Models.Subscription.Input
{
	/// <summary>
	/// 
	/// </summary>
	public class IdentifierFilter
	{
		/// <summary>
		/// 
		/// </summary>
		public Guid SubscriptionId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? GroupId { get; set; }
	}
}
