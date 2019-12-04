using Jaytas.Omilos.Configuration.Interfaces;
using Jaytas.Omilos.Web.Service.Subscription.Business;
using Jaytas.Omilos.Web.Service.Subscription.Business.Interfaces;
using Jaytas.Omilos.Web.Service.Subscription.Data.DbContext;
using Jaytas.Omilos.Web.Service.Subscription.Data.Repositories;
using Jaytas.Omilos.Web.Service.Subscription.Data.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Subscription.App_Start
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
			IBaseConfiguration configurationProvider = null;
			services.AddDbContextPool<ISubscriptionDbContext, SubscriptionDbContext>((serviceProvider, options) =>
			{
				configurationProvider = serviceProvider.GetService<IBaseConfiguration>();
				options.UseLazyLoadingProxies();
				options.UseMySql(configurationProvider.DatabaseConnectionIdentifier.RootConnection,
								 builderOptions => builderOptions.ServerVersion(new Version(5, 7), Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql));
			});

			services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
			services.AddScoped<IGroupRepository, GroupRepository>();
			services.AddScoped<IContactRepository, ContactRepository>();

			services.AddScoped<ISubscriptionProvider, SubscriptionProvider>();
			services.AddScoped<IGroupProvider, GroupProvider>();
			services.AddScoped<IContactProvider, ContactProvider>();
		}
	}
}
