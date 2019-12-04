using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jayatas.Omilos.Functions.Common.Common
{
	/// <summary>
	/// 
	/// </summary>
	public struct Utilities
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static T GetOrDefaultEnvironmentValue<T>(string key, T defaultValue)
		{
			try
			{
				var parsedValue = (T)Convert.ChangeType(Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process), typeof(T));
				return parsedValue == null ? defaultValue : parsedValue;
			}
			catch (Exception)
			{
			}

			return defaultValue;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="maxDegreeOfConcurrency"></param>
		/// <param name="collection"></param>
		/// <param name="taskFactory"></param>
		/// <returns></returns>
		public static async Task RunWithMaxDegreeOfConcurrency<T>(int maxDegreeOfConcurrency, IEnumerable<T> collection, Func<T, Task> taskFactory)
		{
			var activeTasks = new List<Task>(maxDegreeOfConcurrency);
			foreach (var task in collection.Select(taskFactory))
			{
				activeTasks.Add(task);
				if (activeTasks.Count == maxDegreeOfConcurrency)
				{
					await Task.WhenAny(activeTasks.ToArray());
					activeTasks.RemoveAll(t => t.IsCompleted);
				}
			}
			await Task.WhenAll(activeTasks.ToArray());
		}
	}
}