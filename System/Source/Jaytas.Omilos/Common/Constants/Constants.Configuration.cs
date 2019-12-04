using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Common
{
	public partial struct Constants
	{
		public struct Secrets
		{
			public struct AuthTokenProviderSettings
			{
				public struct JwtBearer
				{
					public const string SettingsSectionName = "Secrets:JwtBearerToken";

					public const int ExpiryTimeInMinutes = 60;
				}
			}
			
			public struct IdentityProviderSettings
			{
				public struct Facebook
				{
					public const string SettingsSectionName = "Secrets:Identity:Facebook";

					public const string GraphBaseUri = "https://graph.facebook.com";

					public const string AccessTokenUri = "/v3.1/oauth/access_token";

					public const string UserUri = "/me";
				}

				public struct Google
				{
					public const string SettingsSectionName = "Secrets:Identity:Google";

					public const string GraphBaseUri = "https://www.googleapis.com";

					public const string AccessTokenUri = "/oauth2/v4/token";

					public const string UserUri = "/oauth2/v3/userinfo";

					public const string GrantType = "authorization_code";
				}

				public struct RequestParameters
				{
					public const string ClientId = "client_id";

					public const string ClientSecret = "client_secret";

					public const string Code = "code";

					public const string State = "state";

					public const string RedirectUri = "redirect_uri";

					public const string GrantType = "grant_type";

					public const string Fields = "fields";
				}

				public struct ResponseParameters
				{
					public const string AccessToken = "access_token";

					public const string TokenType = "token_type";

					public const string ExpiresIn = "expires_in";
				}

				public struct Scope
				{
					public const string Email = "email";
				}
			}

			public struct ConnectionIdentifierSettings
			{
				public struct Database
				{
					public const string Default = "Secrets:Database:Default";
				}

				public struct Integration
				{
					public const string Default = "Secrets:Integration:Default";
				}

				public struct Cache
				{
					public const string Default = "Secrets:Cache:Default";
				}
			}
		}

		public struct ServiceEndpointSettings
		{
			public const string SubscriptionService = "ServiceEndpoints:Subscription";
		}

		/// <summary>
		/// 
		/// </summary>
		public struct Claims
		{
			public const string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

			public const string Upn = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn";

			public const string FirstName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";

			public const string Surname = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";

			public const string AppId = "appid";
		}
	}
}