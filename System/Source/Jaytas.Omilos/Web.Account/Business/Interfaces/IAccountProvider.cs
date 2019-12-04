using Jaytas.Omilos.Common.Providers;
using Jaytas.Omilos.Web.Service.Models.Account;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Account.Business.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface IAccountProvider : IBaseProvider
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="signinRequest"></param>
		/// <returns></returns>
		Task<string> AcquireFacebookAccessToken(ExternalSigninRequest signinRequest);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="signinRequest"></param>
		/// <returns></returns>
		Task<string> AcquireGoogleAccessToken(ExternalSigninRequest signinRequest);
	}
}
