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
	class GoogleIdentityProviderSettings : IIdentityProviderSettings
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
		public GoogleIdentityProviderSettings()
		{
			AccessTokenUri = Constants.Secrets.IdentityProviderSettings.Google.AccessTokenUri;
			UserUri = Constants.Secrets.IdentityProviderSettings.Google.UserUri;
			GrantType = Constants.Secrets.IdentityProviderSettings.Google.GrantType;
		}
	}
}