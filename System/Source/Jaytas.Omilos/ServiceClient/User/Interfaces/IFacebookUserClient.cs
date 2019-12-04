using Jaytas.Omilos.Common;
using Jaytas.Omilos.Web.Service.Models.Account;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jaytas.Omilos.ServiceClient.User.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface IFacebookUserClient
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="facebookGraphRequest"></param>
		/// <returns></returns>
		[Get("/me")]//Constants.Secrets.IdentityProviderSettings.Facebook.UserUri)]
		Task <UserData> WhoAmI(Models.FacebookGraphRequestModel facebookGraphRequest);
	}
}