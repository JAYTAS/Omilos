using Jaytas.Omilos.Common;
using Jaytas.Omilos.Messaging.ServiceBus.Topic.Interfaces;
using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Messaging.ServiceBus.Topic
{
	/// <summary>
	/// 
	/// </summary>
	public class MessageBusFactory : IMessageBusFactory
	{
		string _connectionString;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="connectionString"></param>
		public MessageBusFactory(string connectionString)
		{
			_connectionString = connectionString;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="topicName"></param>
		/// <param name="forceCreate"></param>
		/// <returns></returns>
		public async Task<ITopicClient> GetTopicAsync(string topicName, bool forceCreate)
		{
			return await Task.FromResult<ITopicClient>(new TopicClient(_connectionString, topicName));
		}
	}
}