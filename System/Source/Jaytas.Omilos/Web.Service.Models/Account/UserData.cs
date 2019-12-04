using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Web.Service.Models.Account
{
	/// <summary>
	/// 
	/// </summary>
	public class UserData
	{
		/// <summary>
		/// 
		/// </summary>
		public Omilos.Common.Enumerations.ExternalIdentityProviders ExternalIdentityProvider { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Sub { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Email { get; set; }
	}
}
