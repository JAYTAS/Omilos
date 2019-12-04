using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Configuration.Interfaces
{
	public interface IConnectionIdentifierSettings
	{
		/// <summary>
		/// 
		/// </summary>
		string RootConnection { get; set; }

		/// <summary>
		/// 
		/// </summary>
		string ReadOnlyConnection { get; set; }

		/// <summary>
		/// 
		/// </summary>
		string TemplateConnection { get; set; }
	}
}
