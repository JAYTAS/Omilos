using Jaytas.Omilos.Common;
using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.ServiceClient.User.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class FacebookGraphRequestModel
	{
		/// <summary>
		/// 
		/// </summary>
		[AliasAs(Constants.Secrets.IdentityProviderSettings.RequestParameters.Fields)]
		public string Fields { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[AliasAs(Constants.Secrets.IdentityProviderSettings.ResponseParameters.AccessToken)]
		public string AccessToken { get; set; }
	}
}
