using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Middlewares
{
	public class AuthorizationMiddleware : IMiddleware
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="next"></param>
		public AuthorizationMiddleware()
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
