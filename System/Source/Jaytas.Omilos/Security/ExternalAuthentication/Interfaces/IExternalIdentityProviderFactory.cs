using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Security.ExternalAuthentication.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface IExternalIdentityProviderFactory
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="externalIdentityProvider"></param>
		/// <returns></returns>
		IExternalIdentityProvider GetExternalIdentityProvider(Common.Enumerations.ExternalIdentityProviders externalIdentityProvider);
	}
}
