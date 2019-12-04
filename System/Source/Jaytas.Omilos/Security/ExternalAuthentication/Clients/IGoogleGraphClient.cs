using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Security.ExternalAuthentication.Clients
{
	public interface IGoogleGraphClient
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[Post("/oauth2/v4/token")] //Constants.Secrets.IdentityProviderSettings.Google.AccessTokenUri
		Task<Models.IdentityProviderResponseModel> AcquireTokenAsync([Body(BodySerializationMethod.UrlEncoded)] Models.IdentityProviderRequestModel request);
	}
}
