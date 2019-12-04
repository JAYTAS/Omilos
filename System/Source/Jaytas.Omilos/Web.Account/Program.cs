using Jaytas.Omilos.Web.HostConfigurations;
using Microsoft.AspNetCore.Hosting;

namespace Jaytas.Omilos.Web.Service.Account
{
	/// <summary>
	/// 
	/// </summary>
	public class Program : KestrelHostBuilderConfiguration
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="args"></param>
		public static void Main(string[] args)
		{
			CreateWebHostBuilder<Startup>(args).Build().Run();
		}
	}
}
