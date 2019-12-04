using Jaytas.Omilos.Common.Enumerations;
using Jaytas.Omilos.Messaging.ServiceBus.Topic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Messaging.ServiceBus.Topic
{
	public class MessageSenderFactory : IMessageSenderFactory
	{
		IEnumerable<IMessageSender> _messageSenders;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="messageSenders"></param>
		public MessageSenderFactory(IEnumerable<IMessageSender> messageSenders)
		{
			_messageSenders = messageSenders;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="appMessageType"></param>
		/// <returns></returns>
		public IMessageSender Resolve(AppMessageType appMessageType)
		{
			return _messageSenders.FirstOrDefault(sender => sender.CanSend(appMessageType));
		}
	}
}
