using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Web.Service.Models.Subscription
{
	/// <summary>
	/// 
	/// </summary>
	public class ContactWithAssociationStatus : Contact
	{
		/// <summary>
		/// 
		/// </summary>
		public bool HasOptedOut { get; set; }
	}
}
