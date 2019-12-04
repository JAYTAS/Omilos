using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Jaytas.Omilos.Common.Helpers
{
	/// <summary>
	/// An simple class used to provide a focal point for code to build URLs with.  It is designed similarly
	/// to the Web api/MVC UrlHelper classes so that the implementation can be updated to leverage that class
	/// in the future if desired.
	/// </summary>
	/// <seealso cref="T:Deloitte.Radia.Common.Helpers.IResourceUrlBuilder"/>
	public class ResourceUrlBuilder : IResourceUrlBuilder
	{
		/// <summary>
		/// Builds the URL off of the specified path and arguments.
		/// </summary>
		/// <param name="path">The formatted URL path.  This may contain '{variable}' strings that will be replaced 
		/// the value of that 'variable' if it is found in the arguments dictionary. This should not contain any query 
		/// string arguments, those should be passed in in the arguments array.</param>
		/// <param name="arguments">The raw/unescaped arguments that should be replaced in the path via their key 
		/// or appended to a new query string if not in the path.</param>
		/// <returns>A root-relative URL</returns>
		public string Build(string path, IDictionary<string, dynamic> arguments)
		{
			if (!path.StartsWith(Constants.Characters.BackSlash, StringComparison.Ordinal) && !path.StartsWith(Constants.Characters.Dot, StringComparison.Ordinal))
			{
				path = string.Concat(Constants.Characters.BackSlash, path);
			}
			if (arguments == null)
			{
				return path;
			}

			// replace {variables} in route with actual arguments
			// or append to query string if not in route
			path = Regex.Replace(path, Constants.Patterns.RetreiveRouteConstraints, string.Empty,
								 RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
			var query = new StringBuilder();
			foreach (var argument in arguments)
			{
				var key = string.Concat(Constants.Characters.CurlyOpenBrace, argument.Key, Constants.Characters.CurlyClosedBrace);

				var keyIsInPath = path.Contains(key);

				var escapedString = GetEscapedString(argument.Key, argument.Value, keyIsInPath);

				if (keyIsInPath)
				{
					path = path.Replace(key, escapedString);
				}
				else if (!string.IsNullOrWhiteSpace(escapedString))
				{
					// do not add arguments with null values to the query string 
					query.Append(escapedString);
				}
			}

			if (query.Length <= 0)
			{
				return path;
			}

			// if query string exists; remove trailing '&' and update route string
			query.Length--;
			path = string.Concat(path, Constants.Characters.QuestionMark, query.ToString());

			return path;
		}

		/// <summary>
		/// Gets the Escaped String 
		/// </summary>
		/// <param name="value">value</param>
		/// <param name="key">key</param>
		/// <param name="keyIsInPath">keyIsInPath</param>
		/// <returns>escaped string</returns>
		public static string GetEscapedString(string key, dynamic value, bool keyIsInPath)
		{
			if (keyIsInPath)
			{
				return Uri.EscapeDataString(value + String.Empty);
			}

			if (null == value)
			{
				return string.Empty;
			}

			if (!(value is System.Collections.ICollection))
			{
				return Uri.EscapeDataString(key) + Constants.Characters.EqualTo + Uri.EscapeDataString(value + String.Empty) + Constants.Characters.Ampersand;
			}

			StringBuilder escapeString = new StringBuilder();

			foreach (var val in value)
			{
				escapeString.Append(Uri.EscapeDataString(key)).Append(Constants.Characters.EqualTo).Append(Uri.EscapeDataString(val + String.Empty)).Append(Constants.Characters.Ampersand);
			}

			return escapeString.ToString();
		}
	}
}
