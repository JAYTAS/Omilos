using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jayatas.Omilos.Functions.Common.ServiceBus
{
	public class ServiceBusUtilities
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="topicName"></param>
		/// <param name="message"></param>
		public static void Send(string connectionString, string topicName, Message message)
		{
			var topicClient = GetTopic(connectionString, topicName, false);

			topicClient.SendAsync(message).GetAwaiter().GetResult();
			topicClient.CloseAsync().GetAwaiter().GetResult();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="connectionString"></param>
		/// <param name="topicName"></param>
		/// <param name="forceCreate"></param>
		/// <returns></returns>
		private static ITopicClient GetTopic(string connectionString, string topicName, bool forceCreate)
		{
			return new TopicClient(connectionString, topicName);
		}
	}
}