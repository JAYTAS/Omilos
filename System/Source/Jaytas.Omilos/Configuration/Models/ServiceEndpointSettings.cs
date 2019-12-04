using Jaytas.Omilos.Configuration.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Configuration.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class ServiceEndpointSettings : IServiceEndpointSettings
	{
		/// <summary>
		/// 
		/// </summary>
		public string PrivateEndpoint { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string PublicEndpoint { get; set; }
	}
}
