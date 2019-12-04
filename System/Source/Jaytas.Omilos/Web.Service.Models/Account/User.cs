using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Web.Service.Models.Account
{
	/// <summary>
	/// 
	/// </summary>
	public class User
	{
		/// <summary>
		/// 
		/// </summary>
		public Guid UserId { get; set; }

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
	}
}
