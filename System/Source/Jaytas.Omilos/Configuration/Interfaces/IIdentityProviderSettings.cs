using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Configuration.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface IIdentityProviderSettings
	{
		/// <summary>
		/// 
		/// </summary>
		string AccessTokenUri { get; set; }

		/// <summary>
		/// 
		/// </summary>
		string UserUri { get; set; }

		/// <summary>
		/// 
		/// </summary>
		string AppId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		string AppSecret { get; set; }

		/// <summary>
		/// 
		/// </summary>
		string RedirectUri { get; set; }

		/// <summary>
		/// 
		/// </summary>
		string GrantType { get; set; }
	}
}