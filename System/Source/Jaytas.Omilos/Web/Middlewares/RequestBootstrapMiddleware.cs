using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Middlewares
{
	public class RequestBootstrapMiddleware : IMiddleware
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="next"></param>
		public RequestBootstrapMiddleware()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			await next(context);
		}
	}
}
