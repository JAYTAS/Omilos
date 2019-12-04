using Jaytas.Omilos.Common;
using Jaytas.Omilos.Web.Service.Models.Account;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jaytas.Omilos.ServiceClient.User.Interfaces
{
	public interface IGoogleUserClient
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="facebookGraphRequest"></param>
		/// <returns></returns>
		[Get("/oauth2/v3/userinfo")]//Constants.Secrets.IdentityProviderSettings.Google.UserUri)]
		Task<UserData> WhoAmI([Header(Constants.SharedHttpHeaders.Authorization)] string accesstoken);
	}
}