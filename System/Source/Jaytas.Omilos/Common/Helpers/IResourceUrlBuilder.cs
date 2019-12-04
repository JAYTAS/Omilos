using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Common.Helpers
{
	/// <summary>
	/// An simple interface used to provide a focal point for code to build URLs with.  It is designed similarly
	/// to the Web api/MVC UrlHelper classes so that the implementation can be updated to leverage that class
	/// in the future if desired.  Extension methods are generally written against this interface to provide 
	/// resource-specific URLs with type-specific arguments that are backed by these generic methods.
	/// </summary>
	public interface IResourceUrlBuilder
	{
		/// <summary>
		/// Builds the URL off of the specified path and arguments.
		/// </summary>
		/// <param name="path">The formatted URL path.  This may contain '{variable}' strings that will be replaced 
		/// the value of that 'variable' if found in the arguments dictionary. This should not contain any query 
		/// string arguments, those should be passed in in the arguments array.</param>
		/// <param name="arguments">The raw/unescaped arguments that should be replaced in the path via their key 
		/// or appended to a new query string if not in the path.</param>
		/// <returns>A root-relative URL</returns>
		string Build(string path, IDictionary<string, dynamic> arguments);
	}
}
