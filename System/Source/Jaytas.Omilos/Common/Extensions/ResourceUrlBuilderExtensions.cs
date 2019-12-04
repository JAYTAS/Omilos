using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaytas.Omilos.Common.Extensions
{
	/// <summary>
	/// Provides an explicit, strongly typed, interface to generating URLs to specific REST endpoints.
	/// </summary>
	public static class ResourceUrlBuilderExtensions
	{
		/// <summary>
		/// 
		/// </summary>
		private static Dictionary<string, IEnumerable<string>> ResourceWithSubtypes = new Dictionary<string, IEnumerable<string>>(StringComparer.InvariantCultureIgnoreCase)
		{
			
		};
		
		/// <summary>
		/// Parses the resource type from a microservice URI.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <returns></returns>
		public static string ParseResourceTypeFromUri(this string uri)
		{
			// the second folder should be the entity type
			var folders = uri.Split(Constants.Characters.BackSlash[0]);

			// the first folder should be 'api'
			if (folders.Length <= 1)
			{
				// unknow URI format
				return null;
			}

			var resourceType = folders[2].ToLower();
			IEnumerable<string> _subTypes;

			if (folders.Length < 3 || !ResourceWithSubtypes.TryGetValue(resourceType, out _subTypes))
			{
				return resourceType;
			}

			return _subTypes.Where(subtype => folders.Any(folder => string.Compare(folder, subtype, true) == 0)).LastOrDefault() ?? resourceType;
		}

		/// <summary>
		/// Parses the resource type from a microservice URI.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <returns></returns>
		public static string ParseEntityIdFromResourceUri(string uri)
		{
			var folders = uri.TrimStart(Constants.Characters.BackSlash[0]).Split(Constants.Characters.BackSlash[0]);

			// the first folder should be 'api''
			if (folders.Length <= 2)
			{
				// unknow URI format
				return null;
			}

			return folders[3].ToLower();
		}
	}
}
