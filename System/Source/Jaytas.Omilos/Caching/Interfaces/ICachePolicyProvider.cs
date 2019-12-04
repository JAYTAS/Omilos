using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text;

namespace Jaytas.Omilos.Caching.Interfaces
{
	/// <summary>
	/// Provides access to CachePolicies by configuration names.
	/// </summary>
	public interface ICachePolicyProvider
	{
		/// <summary>
		/// Gets the cache item policy based on the configured name
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		CacheItemPolicy GetPolicy(string name);
	}
}
