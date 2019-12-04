using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Web.Service.Models.Subscription
{
	public class ContactWithGroupDetails : Contact
	{
		/// <summary>
		/// 
		/// </summary>
		public List<Group> Groups { get; set; }
	}
}
