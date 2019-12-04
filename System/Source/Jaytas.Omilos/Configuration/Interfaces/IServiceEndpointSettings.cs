using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Configuration.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface IServiceEndpointSettings
	{
		/// <summary>
		/// 
		/// </summary>
		string PrivateEndpoint { get; set; }

		/// <summary>
		/// 
		/// </summary>
		string PublicEndpoint { get; set; }
	}
}
