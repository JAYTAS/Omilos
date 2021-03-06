﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace Jaytas.Omilos.Web.Extensions
{
	public static class ServiceExtensions
	{
		/// <summary>
		/// Adds Automapper configuration to services
		/// </summary>
		/// <param name="services">service collection</param>
		/// <param name="mapProfiles">func which returns mapper configuration</param>
		/// <returns></returns>
		public static IServiceCollection AddMaps(this IServiceCollection services, Func<IMapper> mapProfiles)
		{
			services.AddSingleton<IMapper>(mapProfiles());
			return services;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="services"></param>
		/// <param name="authenticationSchems"></param>
		/// <param name="configureJwtBearerOptions"></param>
		/// <returns></returns>
		public static IServiceCollection AddOmilosAuthentication(this IServiceCollection services, string authenticationScheme, Action<JwtBearerOptions> configureJwtBearerOptions)
		{
			services.AddAuthentication(authenticationScheme).AddJwtBearer(configureJwtBearerOptions);
			return services;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="services"></param>
		/// <param name="registerTypes"></param>
		/// <returns></returns>
		public static IServiceCollection AddOmilosTypes(this IServiceCollection services, Action<IServiceCollection> registerTypes)
		{
			registerTypes(services);
			return services;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="services"></param>
		/// <param name="corsOptionProvider"></param>
		/// <returns></returns>
		public static IServiceCollection AddOmilosCors(this IServiceCollection services, Action<CorsOptions> corsOptionProvider)
		{
			services.AddCors(corsOptionProvider);
			return services;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="services"></param>
		/// <param name="setupOptions"></param>
		/// <returns></returns>
		public static IServiceCollection AddSwagger(this IServiceCollection services, Action<SwaggerGenOptions> setupOptions)
		{
			services.AddSwaggerGen(setupOptions);
			return services;
		}
	}
}
