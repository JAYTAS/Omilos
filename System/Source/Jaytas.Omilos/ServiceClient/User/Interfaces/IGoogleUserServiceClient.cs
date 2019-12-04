using Jaytas.Omilos.ServiceClient.BaseInterfaces;
using Jaytas.Omilos.Web.Service.Models.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jaytas.Omilos.ServiceClient.User.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface IGoogleUserServiceClient : IServiceClient
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		Task<UserData> WhoAmIByCodeAsync(string code);
	}
}