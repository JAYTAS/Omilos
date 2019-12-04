using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using System;

namespace Jaytas.Omilos.Web.HostConfigurations
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class KestrelHostBuilderConfiguration
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TStartup"></typeparam>
		/// <param name="args"></param>
		/// <returns></returns>
		public static IWebHostBuilder CreateWebHostBuilder<TStartup>(string[] args) where TStartup : class
		{
			return WebHost.CreateDefaultBuilder(args).CaptureStartupErrors(true).UseSetting("detailedErrors", "true").UseStartup<TStartup>();
		}
	}
}
