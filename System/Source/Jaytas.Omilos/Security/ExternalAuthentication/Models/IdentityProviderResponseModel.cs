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
	public class IdentityProviderResponseModel
	{
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty(PropertyName = Constants.Secrets.IdentityProviderSettings.ResponseParameters.AccessToken)]
		public string AccessToken { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty(PropertyName = Constants.Secrets.IdentityProviderSettings.ResponseParameters.TokenType)]
		public string TokenType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty(PropertyName = Constants.Secrets.IdentityProviderSettings.ResponseParameters.ExpiresIn)]
		public string ExpiresIn { get; set; }
	}
}
