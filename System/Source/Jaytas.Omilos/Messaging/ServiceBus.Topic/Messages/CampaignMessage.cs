using Jaytas.Omilos.Common;
using Jaytas.Omilos.Common.Enumerations;
using Jaytas.Omilos.Messaging.ServiceBus.Interfaces;
using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaytas.Omilos.Messaging.ServiceBus.Topic.Messages
{
	public class CampaignMessage : IMessageBusMessage
	{
		/// <summary>
		/// 
		/// </summary>
		public string Id { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public AppMessageType AppMessageType => AppMessageType.CampaignManagement;

		/// <summary>
		/// 
		/// </summary>
		public MessageType Type => MessageType.Queues_And_Topics;

		/// <summary>
		/// 
		/// </summary>
		public Guid CampaignIdentifier { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public Events CampaignEvent { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		IDictionary<string, object> AdditionalProperties { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="campaignIdentifier"></param>
		/// <param name="campaignEvent"></param>
		public CampaignMessage(Guid campaignIdentifier, Events campaignEvent) : this(campaignIdentifier, campaignEvent, new Dictionary<string, object>())
		{			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="campaignIdentifier"></param>
		/// <param name="campaignEvent"></param>
		/// <param name="additionalProperties"></param>
		public CampaignMessage(Guid campaignIdentifier, Events campaignEvent, IDictionary<string, object> additionalProperties)
		{
			Id = Guid.NewGuid().ToString();
			CampaignIdentifier = campaignIdentifier;
			CampaignEvent = campaignEvent;
			AdditionalProperties = additionalProperties ?? new Dictionary<string, object>();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public Message ToMessage()
		{
			var message = new Message()
			{
				MessageId = Id.ToString()
			};

			message.UserProperties.Add(Constants.ServiceBus.MessageProperties.Type, AppMessageType.ToString());
			message.UserProperties.Add(Constants.ServiceBus.MessageProperties.CampaignManagement.CampaignIdentifier, CampaignIdentifier.ToString());
			message.UserProperties.Add(Constants.ServiceBus.MessageProperties.CampaignManagement.EventType, CampaignEvent.ToString());

			if(AdditionalProperties != null	&& AdditionalProperties.Any())
			{
				AdditionalProperties.ToList().ForEach(x =>
				{
					message.UserProperties.Add(x.Key, x.Value);
				});
			}

			return message;
		}
	}
}