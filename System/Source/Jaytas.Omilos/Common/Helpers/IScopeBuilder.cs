using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Common.Helpers
{
	/// <summary>
	/// 
	/// </summary>
	public interface IScopeBuilder
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="scopeTemplate"></param>
		/// <param name="arguments"></param>
		/// <returns></returns>
		string Build(string scopeTemplate, IDictionary<string, dynamic> arguments);
	}
}
