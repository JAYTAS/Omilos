using Jaytas.Omilos.Common.Enumerations;
using Jaytas.Omilos.Messaging.Interfaces;
using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Messaging.ServiceBus.Interfaces
{
	public interface IMessageBusMessage : IMessage
	{
		/// <summary>
		/// 
		/// </summary>
		AppMessageType AppMessageType { get; }

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		Message ToMessage();
	}
}