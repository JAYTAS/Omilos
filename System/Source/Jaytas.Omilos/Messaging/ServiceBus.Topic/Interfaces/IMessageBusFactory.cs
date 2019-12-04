using Jaytas.Omilos.Messaging.Interfaces;
using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Messaging.ServiceBus.Topic.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface IMessageBusFactory : IBusFactory
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="topicName"></param>
		/// <param name="forceCreate"></param>
		/// <returns></returns>
		Task<ITopicClient> GetTopicAsync(string topicName, bool forceCreate);
	}
}
