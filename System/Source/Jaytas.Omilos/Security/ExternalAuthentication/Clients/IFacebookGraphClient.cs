using Jaytas.Omilos.Common;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Security.ExternalAuthentication.Clients
{
	/// <summary>
	/// 
	/// </summary>
	public interface IFacebookGraphClient
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[Get("/v3.1/oauth/access_token" )] //Constants.Secrets.IdentityProviderSettings.Facebook.AccessTokenUri
		Task <Models.IdentityProviderResponseModel> AcquireTokenAsync(Models.IdentityProviderRequestModel request);
	}
}
