using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Configuration.Interfaces
{
	public interface IBaseConfiguration
	{
		/// <summary>
		/// 
		/// </summary>
		IIdentityProviderSettings FaceBookAuthenticationSettings { get; }

		/// <summary>
		/// 
		/// </summary>
		IIdentityProviderSettings GoogleAuthenticationSettings { get; }

		/// <summary>
		/// 
		/// </summary>
		IAuthTokenProviderSettings JwtBearerAuthTokenProviderSettings { get; }

		/// <summary>
		/// 
		/// </summary>
		IConnectionIdentifierSettings DatabaseConnectionIdentifier { get; }

		/// <summary>
		/// 
		/// </summary>
		IConnectionIdentifierSettings IntegrationConnectionIdentifier { get; }

		/// <summary>
		/// 
		/// </summary>
		IConnectionIdentifierSettings CacheConnectionIdentifier { get; }

		/// <summary>
		/// 
		/// </summary>
		IServiceEndpointSettings SubscriptionServiceEndpointSettings { get; }
	}
}