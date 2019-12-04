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
	public class GoogleIdentityProvider : IExternalIdentityProvider
	{
		readonly IIdentityProviderSettings _identityProviderSettings;
		readonly IGoogleGraphClient _googleGraphClient;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="identityProviderSettings"></param>
		public GoogleIdentityProvider(IIdentityProviderSettings identityProviderSettings, IGoogleGraphClient googleGraphClient)
		{
			_identityProviderSettings = identityProviderSettings;
			_googleGraphClient = googleGraphClient;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="externalIdentityProvider"></param>
		/// <returns></returns>
		public bool CanProcess(ExternalIdentityProviders externalIdentityProvider)
		{
			return ExternalIdentityProviders.Google == externalIdentityProvider;
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
				RedirectUri = _identityProviderSettings.RedirectUri,
				GrantType = _identityProviderSettings.GrantType
			};

			var response = await _googleGraphClient.AcquireTokenAsync(request);
			return response.AccessToken;
		}
	}
}
