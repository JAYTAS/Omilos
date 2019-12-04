using Jaytas.Omilos.Common;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Security.ExternalAuthentication.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class IdentityProviderRequestModel
	{
		/// <summary>
		/// 
		/// </summary>
		[AliasAs(Constants.Secrets.IdentityProviderSettings.RequestParameters.ClientId)]
		public string ClientId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AliasAs(Constants.Secrets.IdentityProviderSettings.RequestParameters.ClientSecret)]
		public string ClientSecret { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AliasAs(Constants.Secrets.IdentityProviderSettings.RequestParameters.Code)]
		public string Code { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AliasAs(Constants.Secrets.IdentityProviderSettings.RequestParameters.State)]
		public string State { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AliasAs(Constants.Secrets.IdentityProviderSettings.RequestParameters.RedirectUri)]
		public string RedirectUri { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AliasAs(Constants.Secrets.IdentityProviderSettings.RequestParameters.GrantType)]
		public string GrantType { get; set; }
	}
}