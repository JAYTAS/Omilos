using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Web.Service.Models.Subscription
{
	public class SubscriptionWithGroupSummary
	{
		/// <summary>
		/// 
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public List<GroupSummary> GroupSummary { get; set; }
	}
}
