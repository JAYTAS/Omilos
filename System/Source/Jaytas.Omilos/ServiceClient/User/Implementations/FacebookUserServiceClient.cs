using Jaytas.Omilos.Common;
using Jaytas.Omilos.Security.ExternalAuthentication.Interfaces;
using Jaytas.Omilos.ServiceClient.User.Interfaces;
using Jaytas.Omilos.Web.Service.Models.Account;
using System.Threading.Tasks;

namespace Jaytas.Omilos.ServiceClient.User.Implementations
{
	/// <summary>
	/// 
	/// </summary>
	public class FacebookUserServiceClient : IFacebookUserServiceClient
	{
		readonly IExternalIdentityProvider _externalIdentityProvider;
		readonly IFacebookUserClient _facebookGraphClient;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="externalIdentityProviderFactory"></param>
		public FacebookUserServiceClient(IExternalIdentityProviderFactory externalIdentityProviderFactory, IFacebookUserClient facebookGraphClient)
		{
			_externalIdentityProvider = externalIdentityProviderFactory.GetExternalIdentityProvider(Common.Enumerations.ExternalIdentityProviders.Facebook);
			_facebookGraphClient = facebookGraphClient;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		public async Task<UserData> WhoAmIByCodeAsync(string code)
		{
			var accessToken = await _externalIdentityProvider.AcquireTokenByCodeAsync(code);

			var graphRequest = new Models.FacebookGraphRequestModel
			{
				AccessToken = accessToken,
				Fields = Constants.Secrets.IdentityProviderSettings.Scope.Email
			};

			var userData = await _facebookGraphClient.WhoAmI(graphRequest);
			userData.ExternalIdentityProvider = Common.Enumerations.ExternalIdentityProviders.Facebook;

			return userData;
		}
	}
}