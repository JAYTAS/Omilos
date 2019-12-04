using Jaytas.Omilos.Common;
using Jaytas.Omilos.Configuration.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Configuration.Models
{
	public class JwtBearerAuthTokenProviderSettings : IAuthTokenProviderSettings
	{
		/// <summary>
		/// 
		/// </summary>
		public string SingingSecret { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int ExpiryTimeInMinutes { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public JwtBearerAuthTokenProviderSettings()
		{
			ExpiryTimeInMinutes = Constants.Secrets.AuthTokenProviderSettings.JwtBearer.ExpiryTimeInMinutes;
		}
	}
}
