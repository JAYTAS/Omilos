using Jaytas.Omilos.Common.Enumerations;
using Jaytas.Omilos.Messaging.ServiceBus.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Messaging.ServiceBus.Topic.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface IMessageBusMessageFactory
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="appMessageType"></param>
		/// <param name="campaignIdentifier"></param>
		/// <param name="campaignEvent"></param>
		/// <param name="additionalProperties"></param>
		/// <returns></returns>
		IMessageBusMessage CreateMessage(AppMessageType appMessageType, Guid campaignIdentifier, Events campaignEvent, Dictionary<string, object> additionalProperties);
	}
}
