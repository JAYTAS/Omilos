using Jaytas.Omilos.Common.Enumerations;
using Jaytas.Omilos.Messaging.Interfaces;
using Jaytas.Omilos.Messaging.ServiceBus.Interfaces;
using Jaytas.Omilos.Messaging.ServiceBus.Topic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Messaging.ServiceBus.Topic
{
	/// <summary>
	/// 
	/// </summary>
	public class MessageSender : IMessageSender
	{
		IMessageBusFactory _messageBusFactory;
		AppMessageType _appMessageType;
		string _topicName;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="messageBusFactory"></param>
		/// <param name="appMessageType"></param>
		/// <param name="topicName"></param>
		public MessageSender(IMessageBusFactory messageBusFactory, AppMessageType appMessageType, string topicName)
		{
			_messageBusFactory = messageBusFactory;
			_appMessageType = appMessageType;
			_topicName = topicName;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="appMessageType"></param>
		/// <returns></returns>
		public bool CanSend(AppMessageType appMessageType)
		{
			return _appMessageType == appMessageType;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public async Task SendAsync(IMessage message)
		{
			var asMessageBusMessage = message as IMessageBusMessage;

			if (asMessageBusMessage == null)
			{
				throw new FormatException("Invalid MessageBus Message format");
			}

			var messageBusMessage = asMessageBusMessage.ToMessage();
			var topicClient = await _messageBusFactory.GetTopicAsync(_topicName, true);
			try
			{
				await topicClient.SendAsync(messageBusMessage);
				await topicClient.CloseAsync();
			}
			catch (Exception exception)
			{
				//_loggingProvider.Log(System.Diagnostics.TraceEventType.Critical, $"Failed to send Message - {exception.GetInnermostException()}");
			}
		}
	}
}