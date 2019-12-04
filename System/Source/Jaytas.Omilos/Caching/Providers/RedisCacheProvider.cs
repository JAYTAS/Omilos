using Jaytas.Omilos.Caching.Interfaces;
using Jaytas.Omilos.Common;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Caching.Providers
{
	/// <summary>
	/// An ICacheProvider implementation that uses Azure Redis Cache service for caching.
	/// </summary>
	public class RedisCacheProvider : ICacheProvider
	{
		private ICachePolicyProvider _cachePolicyProvider;
		private static string _connectionString;
		private IConnectionMultiplexer _connection;
		private IDatabase _cacheDb;
		private readonly static Dictionary<string, object> _lockObjects = new Dictionary<string, object>();
		private readonly static object _lockObject = new object();

		/// <summary>
		/// Initializes a new instance of the <see cref="RedisCacheProvider" /> class.
		/// </summary>
		/// <param name="cachePolicyProvider">The cache policy provider.</param>
		/// <param name="connectionString">The redis connectionstring.</param>
		public RedisCacheProvider(ICachePolicyProvider cachePolicyProvider, string connectionString)
		{
			_cachePolicyProvider = cachePolicyProvider;
			_connectionString = connectionString;
			Connect();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="connectionMultiplexer"></param>
		/// <param name="database"></param>
		public RedisCacheProvider(IConnectionMultiplexer connectionMultiplexer, IDatabase database, ICachePolicyProvider cachePolicyProvider)
		{
			_connection = connectionMultiplexer;
			_cacheDb = database;
			_cachePolicyProvider = cachePolicyProvider;
		}
		/// <summary>
		/// 
		/// </summary>
		private void Connect()
		{
			_connection = ConnectionMultiplexer.Connect(_connectionString);
			_cacheDb = _connection.GetDatabase();
		}

		/// <summary>
		/// 
		/// </summary>
		private IDatabase Cache
		{
			get
			{
				if (!_connection.IsConnected)
				{
					Connect();
				}
				return _cacheDb;
			}
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
		public T GetOrSet<T>(string cacheKey, string cachePolicy, Func<T> seedFunction)
		{
			var cacheData = Cache.StringGet(cacheKey);
			if (!cacheData.IsNull)
			{
				if (cacheData is ValueType)
				{
					return (T)Convert.ChangeType(cacheData, typeof(T));
				}
				else
				{
					return JsonConvert.DeserializeObject<T>(cacheData);
				}
			}

			lock (GetLockObjectForaKey(cacheKey))
			{
				cacheData = Cache.StringGet(cacheKey);
				if (!cacheData.IsNull)
				{
					if (cacheData is ValueType)
					{
						return (T)Convert.ChangeType(cacheData, typeof(T));
					}
					else
					{
						return JsonConvert.DeserializeObject<T>(cacheData);
					}
				}

				var policy = _cachePolicyProvider.GetPolicy(cachePolicy);
				var seedData = seedFunction();

				if (seedData == null)
				{
					return default(T);
				}
				var serializedData = JsonConvert.SerializeObject(seedData);

				var expiryDate = policy.AbsoluteExpiration;
				if (policy.SlidingExpiration > TimeSpan.Zero)
				{
					expiryDate = DateTime.UtcNow.Add(policy.SlidingExpiration);
				}

				Cache.StringSet(cacheKey, serializedData, expiryDate.Subtract(DateTime.UtcNow));
				return seedData;
			}
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
			var cacheData = Cache.StringGet(cacheKey);
			if (!cacheData.IsNullOrEmpty)
			{
				return JsonConvert.DeserializeObject<T>(cacheData);
			}

			var policy = _cachePolicyProvider.GetPolicy(cachePolicy);
			var seedData = await seedFunction().ConfigureAwait(true);
			lock (GetLockObjectForaKey(cacheKey))
			{
				cacheData = Cache.StringGet(cacheKey);
				if (!cacheData.IsNullOrEmpty)
				{
					return JsonConvert.DeserializeObject<T>(cacheData);
				}

				if (seedData == null ||
					(seedData.GetType().IsGenericType &&
					 seedData is System.Collections.ICollection &&
					 ((System.Collections.ICollection)seedData).Count == 0))
				{
					return default(T);
				}

				var serializedData = JsonConvert.SerializeObject(seedData);

				var expiryDate = policy.AbsoluteExpiration;
				if (policy.SlidingExpiration > TimeSpan.Zero)
				{
					expiryDate = DateTime.UtcNow.Add(policy.SlidingExpiration);
				}

				Cache.StringSet(cacheKey, serializedData, expiryDate.Subtract(DateTime.UtcNow));

				return seedData;
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
			var expiryDate = policy.AbsoluteExpiration;
			if (policy.SlidingExpiration > TimeSpan.Zero)
			{
				expiryDate = DateTime.UtcNow.Add(policy.SlidingExpiration);
			}

			Cache.StringSet(cacheKey, JsonConvert.SerializeObject(data), expiryDate.Subtract(DateTime.UtcNow));
		}

		/// <summary>
		/// Invalidate all the cache object for a key.
		/// </summary>
		/// <param name="key"></param>
		public async Task InvalidateCacheAsync(string key)
		{
			await Cache.KeyDeleteAsync(key);
		}

		/// <summary>
		/// Invalidate cache object for collection of keys.
		/// </summary>
		/// <param name="keys"></param>
		public void InvalidateCacheAsync(string[] keys)
		{
			foreach (var key in keys)
			{
				Cache.KeyDeleteAsync(key);
			}
		}

		/// <summary>
		/// Invalidate all the cache objects by partial key.
		/// </summary>
		/// <param name="partialKey"></param>
		public void InvalidateCacheByPartialKey(string partialKey)
		{
			var cacheServer = _connection.GetServer(_connection.GetEndPoints().FirstOrDefault());
			var keys = cacheServer.Keys(database: Cache.Database, pattern: Constants.Characters.Asterisk + partialKey + Constants.Characters.Asterisk);
			// show all keys in database 0 that include "user id" in their name.
			Cache.KeyDeleteAsync(keys.ToArray());
		}

		/// <summary>
		/// Invalidate all the cache objects by collection of partial keys.
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
			var cacheData = Cache.StringGet(cacheKey);


			if (!cacheData.IsNull)
			{
				if (cacheData is ValueType)
				{
					return (T)Convert.ChangeType(cacheData, typeof(T));
				}
				else
				{
					return JsonConvert.DeserializeObject<T>(cacheData);
				}
			}

			return default(T);
		}

		/// <summary>
		/// Gets the value for a key
		/// </summary>
		/// <typeparam name="T">The type of the data in cache.</typeparam>
		/// <param name="cacheKey">Cache Key</param>
		/// <returns></returns>
		public async Task<T> GetAsync<T>(string cacheKey)
		{
			var cacheData = await Cache.StringGetAsync(cacheKey);
			if (!cacheData.IsNullOrEmpty)
			{
				if (cacheData is ValueType)
				{
					return (T)Convert.ChangeType(cacheData, typeof(T));
				}
				else
				{
					return JsonConvert.DeserializeObject<T>(cacheData);
				}
			}

			return default(T);
		}

		/// <summary>
		/// Get all keys by partial name
		/// </summary>
		/// <param name="partialKey"></param>
		/// <returns></returns>
		public IEnumerable<string> GetKeysByPartialName(string partialKey)
		{
			var cacheServer = _connection.GetServer(_connection.GetEndPoints().FirstOrDefault());
			return cacheServer.Keys(database: Cache.Database, pattern: Constants.Characters.Asterisk + partialKey + Constants.Characters.Asterisk).Select(key => key.ToString());
		}

		/// <summary>
		/// Gets the lock object for a key
		/// </summary>
		/// <param name="key">Cache Key</param>
		private object GetLockObjectForaKey(string key)
		{
			object @object;
			if (_lockObjects.TryGetValue(key, out @object))
			{
				return @object;
			}

			lock (_lockObject)
			{
				if (_lockObjects.TryGetValue(key, out @object))
				{
					return @object;
				}

				@object = new object();
				_lockObjects.Add(key, @object);

				return @object;
			}
		}
	}
}
