using Jaytas.Omilos.Common;
using Jaytas.Omilos.Security.ExternalAuthentication.Interfaces;
using Jaytas.Omilos.ServiceClient.User.Interfaces;
using Jaytas.Omilos.Web.Service.Models.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jaytas.Omilos.ServiceClient.User.Implementations
{
	public class GoogleUserServiceClient : IGoogleUserServiceClient
	{
		readonly IExternalIdentityProvider _externalIdentityProvider;
		readonly IGoogleUserClient _googleGraphClient;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="externalIdentityProviderFactory"></param>
		/// <param name="googleGraphClient"></param>
		public GoogleUserServiceClient(IExternalIdentityProviderFactory externalIdentityProviderFactory, IGoogleUserClient googleGraphClient)
		{
			_externalIdentityProvider = externalIdentityProviderFactory.GetExternalIdentityProvider(Common.Enumerations.ExternalIdentityProviders.Google);
			_googleGraphClient = googleGraphClient;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		public async Task<UserData> WhoAmIByCodeAsync(string code)
		{
			var accessToken = await _externalIdentityProvider.AcquireTokenByCodeAsync(code);
			
			var userData = await _googleGraphClient.WhoAmI($"{Constants.BearerOptions.Scheme} {accessToken}");
			userData.ExternalIdentityProvider = Common.Enumerations.ExternalIdentityProviders.Google;

			return userData;

			throw new Exception();
		}
	}
}
