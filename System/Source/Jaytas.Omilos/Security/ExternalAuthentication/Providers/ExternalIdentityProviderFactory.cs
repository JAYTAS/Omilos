using Jaytas.Omilos.Common.Enumerations;
using Jaytas.Omilos.Security.ExternalAuthentication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaytas.Omilos.Security.ExternalAuthentication.Providers
{
	/// <summary>
	/// 
	/// </summary>
	public class ExternalIdentityProviderFactory : IExternalIdentityProviderFactory
	{
		readonly IEnumerable<IExternalIdentityProvider> _externalIdentityProviders;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="externalIdentityProviders"></param>
		public ExternalIdentityProviderFactory(IEnumerable<IExternalIdentityProvider> externalIdentityProviders)
		{
			_externalIdentityProviders = externalIdentityProviders;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="externalIdentityProvider"></param>
		/// <returns></returns>
		public IExternalIdentityProvider GetExternalIdentityProvider(ExternalIdentityProviders externalIdentityProvider)
		{
			if(null == _externalIdentityProviders)
			{
				return null;
			}

			return _externalIdentityProviders.FirstOrDefault(provider => provider.CanProcess(externalIdentityProvider));
		}
	}
}
