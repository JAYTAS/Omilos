using Jaytas.Omilos.Caching.Interfaces;
using Jaytas.Omilos.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace Jaytas.Omilos.Caching.Providers
{
	/// <summary>
	/// A simple cache provider that uses hard coded policies.
	/// </summary>
	/// <seealso cref="Interfaces.ICachePolicyProvider" />
	public class StaticCachePolicyProvider : ICachePolicyProvider
	{
		private class CacheConfig
		{
			public CacheItemPriority Priority { get; set; }
			public TimeSpan? AbsoluteExpirationWindow { get; set; }
			public TimeSpan? SlidingExpirationWindow { get; set; }

			public CacheConfig(CacheItemPriority priority, TimeSpan? absoluteExpirationWindow, TimeSpan? slidingExpirationWindow)
			{
				Priority = priority;
				AbsoluteExpirationWindow = absoluteExpirationWindow;
				SlidingExpirationWindow = slidingExpirationWindow;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private readonly Dictionary<string, CacheConfig> _configs;

		/// <summary>
		/// 
		/// </summary>
		public StaticCachePolicyProvider()
		{
			_configs = new Dictionary<string, CacheConfig>()
			{
				{ Constants.Caching.Policies.Default, new CacheConfig(CacheItemPriority.Default, new TimeSpan(0, 30, 0), null) },
				{ Constants.Caching.Policies.UserRolesDependencies, new CacheConfig(CacheItemPriority.NotRemovable,  new TimeSpan(7, 0, 0, 0), null) }
			};
		}

		/// <summary>
		/// Creates a new cache item policy based on the configuration specified by name.
		/// </summary>
		/// <param name="name">The cache policy name.</param>
		/// <returns>A new policy with correct dates relative to now</returns>
		/// <exception cref="System.InvalidOperationException">If a policy with that name is not configured</exception>
		public CacheItemPolicy GetPolicy(string name)
		{
			CacheConfig config;
			if (_configs.TryGetValue(name, out config))
			{
				return BuildPolicy(config);
			}

			throw new InvalidOperationException($"Unknown cache policy '{name}'");
		}

		private static CacheItemPolicy BuildPolicy(CacheConfig config)
		{
			var policy = new CacheItemPolicy
			{
				Priority = config.Priority
			};
			if (config.AbsoluteExpirationWindow.HasValue)
			{
				policy.AbsoluteExpiration = DateTimeOffset.UtcNow.Add(config.AbsoluteExpirationWindow.Value);
			}
			if (config.SlidingExpirationWindow.HasValue)
			{
				policy.SlidingExpiration = config.SlidingExpirationWindow.Value;
			}

			return policy;
		}

	}
}
