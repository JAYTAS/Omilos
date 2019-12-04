using Jaytas.Omilos.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Web.Service.Models.Subscription
{
	public class Group
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
		public bool IsUsed { get; set; }
	}
}
