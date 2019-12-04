using Jaytas.Omilos.Configuration.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Configuration.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class ConnectionIdentifierSettings : IConnectionIdentifierSettings
	{
		/// <summary>
		/// 
		/// </summary>
		public string RootConnection { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string ReadOnlyConnection { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string TemplateConnection { get; set; }

	}
}
