using Jaytas.Omilos.Web.Middlewares;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Web.Extensions
{
	public static class AppExtensions
	{
		/// <summary>
		/// 
		/// </summary>
		public static IApplicationBuilder UseRequestBootstrap(this IApplicationBuilder builder)
		{
			builder.UseMiddleware<RequestBootstrapMiddleware>();
			return builder;
		}

		/// <summary>
		/// 
		/// </summary>
		public static IApplicationBuilder UseAuthorization(this IApplicationBuilder builder)
		{
			builder.UseMiddleware<AuthorizationMiddleware>();
			return builder;
		}
	}
}
