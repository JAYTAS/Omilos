using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Configuration.Interfaces
{
	public interface IAuthTokenProviderSettings
	{
		/// <summary>
		/// 
		/// </summary>
		string SingingSecret { get; set; }

		/// <summary>
		/// 
		/// </summary>
		int ExpiryTimeInMinutes { get; set; }
	}
}
