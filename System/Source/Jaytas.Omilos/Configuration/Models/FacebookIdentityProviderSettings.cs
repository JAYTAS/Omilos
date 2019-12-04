using Jaytas.Omilos.Common;
using Jaytas.Omilos.Configuration.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Configuration.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class FacebookIdentityProviderSettings : IIdentityProviderSettings
	{
		/// <summary>
		/// 
		/// </summary>
		public string AccessTokenUri { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string UserUri { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string AppId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string AppSecret { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string RedirectUri { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string GrantType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public FacebookIdentityProviderSettings()
		{
			AccessTokenUri = Constants.Secrets.IdentityProviderSettings.Facebook.AccessTokenUri;
			UserUri = Constants.Secrets.IdentityProviderSettings.Facebook.UserUri;
		}
	}
}
