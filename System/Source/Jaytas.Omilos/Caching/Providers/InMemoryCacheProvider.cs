using Jaytas.Omilos.Caching.Interfaces;
using Jaytas.Omilos.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Caching.Providers
{
	/// <summary>
	/// An ICacheProvider implementation that uses InMemoryCache for caching.
	/// </summary>
	/// 
	public class InMemoryCacheProvider : ICacheProvider
	{
		private readonly ICachePolicyProvider _cachePolicyProvider;
		private static MemoryCache _cache;
		private object _lockObject;
		private Dictionary<string, object> _lockObjectsForKeys;
		private object _lockObjectForCacheKeys;
		private HashSet<String> _cacheKeys;

		/// <summary>
		/// Initializes a new instance of the <see cref="InMemoryCacheProvider" /> class.
		/// </summary>
		/// <param name="cachePolicyProvider">The cache policy provider.</param>
		public InMemoryCacheProvider(ICachePolicyProvider cachePolicyProvider)
		{
			_cachePolicyProvider = cachePolicyProvider;
			_cache = MemoryCache.Default;
			_cacheKeys = new HashSet<string>();
			_lockObject = new object();
			_lockObjectsForKeys = new Dictionary<string, object>();
			_lockObjectForCacheKeys = new object();
		}

		/// <summary>
		/// Gets the specified cache key from cache if present, otherwise calls the seedFunction to get
		/// the data to cache and adds it using the cachePolicy specified.
		/// </summary>
		/// <typeparam name="T">The type of the data in cache.</typeparam>
		/// <param name="cacheKey">The cache key.</param>
		/// <param name="cachePolicy">The cache policy.</param>
		/// <param name="seedFunction">The synchronous seed function.</param>
		/// <returns></returns>
		public T GetOrSet<T>(string cacheKey, string cachePolicy, Func<T> seedFunction)
		{
			var data = _cacheKeys.Any(x => x == cacheKey) ? (T)_cache.Get(cacheKey) : default(T);
			if (data != null)
			{
				return data;
			}

			lock (GetLockObjectForaKey(cacheKey))
			{
				data = _cacheKeys.Any(x => x == cacheKey) ? (T)_cache.Get(cacheKey) : default(T);
				if (data != null)
				{
					return data;
				}

				var policy = _cachePolicyProvider.GetPolicy(cachePolicy);
				data = seedFunction();

				if (data == null)
				{
					return data;
				}

				_cache.Set(cacheKey, data, policy);
				AddOrRemoveCacheKey(cacheKey, true);
			}

			return data;
		}

		/// <summary>
		/// Gets the specified cache key from cache if present, otherwise calls the seedFunction to get
		/// the data to cache and adds it using the cachePolicy specified.
		/// </summary>
		/// <typeparam name="T">The type of the data in cache.</typeparam>
		/// <param name="cacheKey">The cache key.</param>
		/// <param name="cachePolicy">The cache policy.</param>
		/// <param name="seedFunction">The asynchronous seed function.</param>
		/// <returns></returns>
		public async Task<T> GetOrSetAsync<T>(string cacheKey, string cachePolicy, Func<Task<T>> seedFunction)
		{
			try
			{
				var data = _cacheKeys.Any(x => x == cacheKey) ? (T)_cache.Get(cacheKey) : default(T);
				if (data != null)
				{
					return data;
				}

				var policy = _cachePolicyProvider.GetPolicy(cachePolicy);
				var seedData = await seedFunction().ConfigureAwait(true);

				if (seedData == null)
				{
					return default(T);
				}

				lock (GetLockObjectForaKey(cacheKey))
				{
					data = _cacheKeys.Any(x => x == cacheKey) ? (T)_cache.Get(cacheKey) : default(T);
					if (data != null)
					{
						return data;
					}

					data = seedData;
					_cache.Set(cacheKey, data, policy);
					AddOrRemoveCacheKey(cacheKey, true);
				}
				return data;
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Sets the data for a specific key
		/// </summary>
		/// <param name="cacheKey">Cache Key</param>
		/// <param name="cachePolicy">Cache policy for this key</param>
		/// <param name="data">data to be cached</param>
		/// <returns></returns>
		public void Set(string cacheKey, string cachePolicy, object data)
		{
			var policy = _cachePolicyProvider.GetPolicy(cachePolicy);
			_cache.Set(cacheKey, data.ToJson(), policy);
			AddOrRemoveCacheKey(cacheKey, true);
		}

		/// <summary>
		/// Invalidate cache object for collection of keys.
		/// </summary>
		/// <param name="key"></param>
		public Task InvalidateCacheAsync(string key)
		{
			if (_cacheKeys.All(x => x == key))
			{
				AddOrRemoveCacheKey(key, false);
			}
			return Task.CompletedTask;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="keys"></param>
		public void InvalidateCacheAsync(string[] keys)
		{
			foreach (var key in keys)
			{
				this.InvalidateCacheAsync(key);
			}
		}

		/// <summary>
		/// Invalidate all the cache objects by partial key.
		/// </summary>
		/// <param name="partialKey"></param>
		public void InvalidateCacheByPartialKey(string partialKey)
		{
			var keys = _cacheKeys.Where(x => x.Contains(partialKey)).ToList();

			if (!keys.Any())
			{
				return;
			}

			AddOrRemoveCacheKeys(keys, false);
		}

		/// <summary>
		/// Invalidate all the cache objects for collection of partial keys.
		/// </summary>
		/// <param name="partialKeys"></param>
		public void InvalidateCacheByPartialKey(string[] partialKeys)
		{
			foreach (var partialKey in partialKeys)
			{
				this.InvalidateCacheByPartialKey(partialKey);
			}
		}

		/// <summary>
		/// Gets the value for a key
		/// </summary>
		/// <typeparam name="T">The type of the data in cache.</typeparam>
		/// <param name="cacheKey">Cache Key</param>
		/// <returns></returns>
		public T Get<T>(string cacheKey)
		{
			return ((String)_cache.Get(cacheKey)).ToObject<T>();
		}

		/// <summary>
		/// Gets the value for a key
		/// </summary>
		/// <typeparam name="T">The type of the data in cache.</typeparam>
		/// <param name="cacheKey">Cache Key</param>
		/// <returns></returns>
		public async Task<T> GetAsync<T>(string cacheKey)
		{
			return await Task.FromResult((T)_cache.Get(cacheKey));
		}

		/// <summary>
		/// Get all keys by partial name
		/// </summary>
		/// <param name="partialKey"></param>
		/// <returns></returns>
		public IEnumerable<string> GetKeysByPartialName(string partialKey)
		{
			return _cacheKeys.Where(x => x.Contains(partialKey));
		}

		/// <summary>
		/// Gets the lock object for a key
		/// </summary>
		/// <param name="key">Cache Key</param>
		private object GetLockObjectForaKey(string key)
		{
			object @object;
			if (_lockObjectsForKeys.TryGetValue(key, out @object))
			{
				return @object;
			}

			lock (_lockObject)
			{
				if (_lockObjectsForKeys.TryGetValue(key, out @object))
				{
					return @object;
				}

				@object = new object();
				_lockObjectsForKeys.Add(key, @object);

				return @object;
			}
		}

		/// <summary>
		/// utility method to add or remove keys.
		/// </summary>
		/// <param name="key">cahce key</param>
		/// <param name="isAddOperation">flag to decide add operation</param>
		private void AddOrRemoveCacheKey(string key, bool isAddOperation)
		{
			lock (_lockObjectForCacheKeys)
			{
				if (isAddOperation)
				{
					_cacheKeys.Add(key);
					return;
				}
				_cacheKeys.Remove(key);
			}
		}

		/// <summary>
		/// utility method to add or remove keys.
		/// </summary>
		/// <param name="keys">cahce keys</param>
		/// <param name="isAddOperation">flag to decide add operation</param>
		private void AddOrRemoveCacheKeys(IEnumerable<string> keys, bool isAddOperation)
		{
			lock (_lockObjectForCacheKeys)
			{
				if (isAddOperation)
				{
					keys.Select(x => { _cacheKeys.Add(x); return x; }).ToList();
					return;
				}
				_cacheKeys.RemoveWhere(cacheKey => keys.Any(key => key == cacheKey));
			}
		}
	}
}
