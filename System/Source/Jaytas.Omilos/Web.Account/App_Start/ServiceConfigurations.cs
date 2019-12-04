using Jaytas.Omilos.Common;
using Jaytas.Omilos.Common.DelegationHandlers;
using Jaytas.Omilos.Configuration.Interfaces;
using Jaytas.Omilos.Security.ExternalAuthentication.Clients;
using Jaytas.Omilos.Security.ExternalAuthentication.Interfaces;
using Jaytas.Omilos.Security.ExternalAuthentication.Providers;
using Jaytas.Omilos.ServiceClient.User.Implementations;
using Jaytas.Omilos.ServiceClient.User.Interfaces;
using Jaytas.Omilos.Web.Service.Account.Business;
using Jaytas.Omilos.Web.Service.Account.Business.Interfaces;
using Jaytas.Omilos.Web.Service.Account.Data.DbContext;
using Jaytas.Omilos.Web.Service.Account.Data.Repositories;
using Jaytas.Omilos.Web.Service.Account.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

namespace Jaytas.Omilos.Web.Service.Account.App_Start
{
	/// <summary>
	/// 
	/// </summary>
	public class ServiceConfigurations
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="services"></param>
		public static void RegisterTypes(IServiceCollection services)
		{
			services.AddRefitClient<IFacebookGraphClient>()
						.ConfigureHttpClient(c => c.BaseAddress = new Uri(Constants.Secrets.IdentityProviderSettings.Facebook.GraphBaseUri))
						.AddHttpMessageHandler<HttpBootstrapHandler>();
			services.AddRefitClient<IGoogleGraphClient>()
						.ConfigureHttpClient(c => c.BaseAddress = new Uri(Constants.Secrets.IdentityProviderSettings.Google.GraphBaseUri))
						.AddHttpMessageHandler<HttpBootstrapHandler>();

			IBaseConfiguration configurationProvider = null;
			services.AddSingleton<IExternalIdentityProvider, FacebookIdentityProvider>(serviceProvider =>
			{
				configurationProvider = serviceProvider.GetService<IBaseConfiguration>();
				IFacebookGraphClient facebookGraphClient = serviceProvider.GetService<IFacebookGraphClient>();
				return new FacebookIdentityProvider(configurationProvider.FaceBookAuthenticationSettings, facebookGraphClient);
			});

			services.AddSingleton<IExternalIdentityProvider, GoogleIdentityProvider>(serviceProvider =>
			{
				configurationProvider = serviceProvider.GetService<IBaseConfiguration>();
				IGoogleGraphClient googleGraphClient = serviceProvider.GetService<IGoogleGraphClient>();
				return new GoogleIdentityProvider(configurationProvider.GoogleAuthenticationSettings, googleGraphClient);
			});

			services.AddSingleton<IExternalIdentityProviderFactory, ExternalIdentityProviderFactory>();

			services.AddRefitClient<IFacebookUserClient>()
						.ConfigureHttpClient(c => c.BaseAddress = new Uri(Constants.Secrets.IdentityProviderSettings.Facebook.GraphBaseUri))
						.AddHttpMessageHandler<HttpBootstrapHandler>();
			services.AddRefitClient<IGoogleUserClient>()
						.ConfigureHttpClient(c => c.BaseAddress = new Uri(Constants.Secrets.IdentityProviderSettings.Google.GraphBaseUri))
						.AddHttpMessageHandler<HttpBootstrapHandler>();

			services.AddSingleton<IFacebookUserServiceClient, FacebookUserServiceClient>();
			services.AddSingleton<IGoogleUserServiceClient, GoogleUserServiceClient>();

			services.AddDbContextPool<IAccountDbContext, AccountDbContext>((serviceProvider, options) =>
			{
				configurationProvider = serviceProvider.GetService<IBaseConfiguration>();
				options.UseLazyLoadingProxies();
				options.UseMySql(configurationProvider.DatabaseConnectionIdentifier.RootConnection,
								 builderOptions => builderOptions.ServerVersion(new Version(5, 7), Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql));
			});

			services.AddScoped<IRoleRepository, RoleRepository>();
			services.AddScoped<IUserRepository, UserRepository>();

			services.AddScoped<IRoleProvider, RoleProvider>();
			services.AddScoped<IAccountProvider, AccountProvider>();
		}
	}
}