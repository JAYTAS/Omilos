using Jaytas.Omilos.Common;
using Microsoft.Extensions.Configuration;

namespace Jaytas.Omilos.Configuration.Interfaces
{
	public abstract class BaseConfiguration : IBaseConfiguration
	{
		protected IConfiguration _configuration;

		protected BaseConfiguration(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		/// <summary>
		/// 
		/// </summary>
		public IIdentityProviderSettings FaceBookAuthenticationSettings { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public IIdentityProviderSettings GoogleAuthenticationSettings { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public IConnectionIdentifierSettings DatabaseConnectionIdentifier { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public IConnectionIdentifierSettings IntegrationConnectionIdentifier { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public IConnectionIdentifierSettings CacheConnectionIdentifier { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public IAuthTokenProviderSettings JwtBearerAuthTokenProviderSettings { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public IServiceEndpointSettings SubscriptionServiceEndpointSettings { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		protected void ParseConfiguration()
		{
			LoadJwtBearerAuthSettings();
			LoadAuthenticationSettings();
			LoadConnectionIdentifierSettings();
			LoadServiceEndpointSettings();
		}

		private void LoadJwtBearerAuthSettings()
		{
			JwtBearerAuthTokenProviderSettings = _configuration.GetSection(Constants.Secrets.AuthTokenProviderSettings.JwtBearer.SettingsSectionName).Get<Models.JwtBearerAuthTokenProviderSettings>();
		}

		/// <summary>
		/// 
		/// </summary>
		private void LoadAuthenticationSettings()
		{
			FaceBookAuthenticationSettings = _configuration.GetSection(Constants.Secrets.IdentityProviderSettings.Facebook.SettingsSectionName).Get<Models.FacebookIdentityProviderSettings>();
			GoogleAuthenticationSettings = _configuration.GetSection(Constants.Secrets.IdentityProviderSettings.Google.SettingsSectionName).Get<Models.GoogleIdentityProviderSettings>();
		}

		/// <summary>
		/// 
		/// </summary>
		private void LoadConnectionIdentifierSettings()
		{
			DatabaseConnectionIdentifier = _configuration.GetSection(Constants.Secrets.ConnectionIdentifierSettings.Database.Default).Get<Models.ConnectionIdentifierSettings>();
			IntegrationConnectionIdentifier = _configuration.GetSection(Constants.Secrets.ConnectionIdentifierSettings.Integration.Default).Get<Models.ConnectionIdentifierSettings>();
			CacheConnectionIdentifier = _configuration.GetSection(Constants.Secrets.ConnectionIdentifierSettings.Cache.Default).Get<Models.ConnectionIdentifierSettings>();
		}

		/// <summary>
		/// 
		/// </summary>
		private void LoadServiceEndpointSettings()
		{
			SubscriptionServiceEndpointSettings = _configuration.GetSection(Constants.ServiceEndpointSettings.SubscriptionService).Get<Models.ServiceEndpointSettings>();
		}
	}
}