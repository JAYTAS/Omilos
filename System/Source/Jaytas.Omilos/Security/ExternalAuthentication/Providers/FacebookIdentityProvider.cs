using Jaytas.Omilos.Common.Enumerations;
using Jaytas.Omilos.Configuration.Interfaces;
using Jaytas.Omilos.Security.ExternalAuthentication.Clients;
using Jaytas.Omilos.Security.ExternalAuthentication.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Security.ExternalAuthentication.Providers
{
	/// <summary>
	/// 
	/// </summary>
	public class FacebookIdentityProvider : IExternalIdentityProvider
	{
		readonly IIdentityProviderSettings _identityProviderSettings;
		readonly IFacebookGraphClient _facebookGraphClient;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="identityProviderSettings"></param>
		/// <param name="facebookGraphClient"></param>
		public FacebookIdentityProvider(IIdentityProviderSettings identityProviderSettings, IFacebookGraphClient facebookGraphClient)
		{
			_identityProviderSettings = identityProviderSettings;
			_facebookGraphClient = facebookGraphClient;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="externalIdentityProvider"></param>
		/// <returns></returns>
		public bool CanProcess(ExternalIdentityProviders externalIdentityProvider)
		{
			return ExternalIdentityProviders.Facebook == externalIdentityProvider;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		public async Task<string> AcquireTokenByCodeAsync(string code)
		{
			var request = new Models.IdentityProviderRequestModel
			{
				ClientId = _identityProviderSettings.AppId,
				ClientSecret = _identityProviderSettings.AppSecret,
				Code = code,
				RedirectUri = _identityProviderSettings.RedirectUri
			};
			var response = await _facebookGraphClient.AcquireTokenAsync(request);
			return response.AccessToken;
		}
	}
}