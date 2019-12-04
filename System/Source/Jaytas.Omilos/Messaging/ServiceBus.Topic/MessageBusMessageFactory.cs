using Jaytas.Omilos.Common.Enumerations;
using Jaytas.Omilos.Messaging.ServiceBus.Interfaces;
using Jaytas.Omilos.Messaging.ServiceBus.Topic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Messaging.ServiceBus.Topic
{
	public class MessageBusMessageFactory : IMessageBusMessageFactory
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="appMessageType"></param>
		/// <param name="campaignIdentifier"></param>
		/// <param name="campaignEvent"></param>
		/// <param name="additionalProperties"></param>
		/// <returns></returns>
		public IMessageBusMessage CreateMessage(AppMessageType appMessageType, Guid campaignIdentifier, Events campaignEvent, Dictionary<string, object> additionalProperties)
		{
			if(AppMessageType.CampaignManagement != appMessageType)
			{
				throw new InvalidOperationException();
			}

			return new Messages.CampaignMessage(campaignIdentifier, campaignEvent, additionalProperties);
		}
	}
}