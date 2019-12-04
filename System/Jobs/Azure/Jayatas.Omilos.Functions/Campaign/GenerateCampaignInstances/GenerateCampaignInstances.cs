using Jayatas.Omilos.Functions.Common.Common;
using Jayatas.Omilos.Functions.Common.Database;
using Jayatas.Omilos.Functions.Common.ServiceBus;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;

namespace Jayatas.Omilos.Functions.Campaign.GenerateCampaignInstances
{
	/// <summary>
	/// 
	/// </summary>
    public static class GenerateCampaignInstances
    {
        [FunctionName(nameof(GenerateCampaignInstances))]
        public static void Run([ServiceBusTrigger(Constants.NameResolverKeys.CampaignManagementTopic, Constants.Subscriptions.Published, Connection = Constants.ConfigKeys.OmilosIntegrationConnection)]Message message, TraceWriter log)
        {
			var campaignIdentifier = message.UserProperties[Constants.MessageProperties.CampaignManagement.CampaignIdentifier];
			var parameters = new Dictionary<string, object>
			{
				{
					Constants.StoreProcedures.GenerateCampaignInstances.Parameters.CampaignId,
					message.UserProperties[Constants.MessageProperties.CampaignManagement.CampaignIdentifier]
				}
			};

			MySqlUtilities.ExecuteNonQuery(ConfigurationManager.OmilosConnection,
			  							   Constants.StoreProcedures.GenerateCampaignInstances.Name,
										   parameters);

			var campaignStartDateTimeText = $"{message.UserProperties[Constants.MessageProperties.CampaignManagement.AdditionalProperties.CampaignStartDate]} {message.UserProperties[Constants.MessageProperties.CampaignManagement.AdditionalProperties.CampaignStartTime]}";

			log.Info($"campaignStartDateTime in Text : {campaignStartDateTimeText}");

			DateTime campaignStartDateTime;
			DateTime.TryParse(campaignStartDateTimeText, out campaignStartDateTime);

			log.Info($"campaignStartDateTime After conversion : {campaignStartDateTime}");

			var campaignStartDateTimeInUtc = TimeZoneInfo.ConvertTimeToUtc(campaignStartDateTime, Constants.TimeZoneInformation.TimeZoneMapping[message.UserProperties[Constants.MessageProperties.CampaignManagement.AdditionalProperties.CampaignTimeZone].ToString()]);

			log.Info($"campaignStartDateTime in UTC : {campaignStartDateTimeInUtc}");

			if (DateTime.UtcNow.Date >= campaignStartDateTime.Date)
			{
				TriggerTodaysNotification(Guid.Parse(campaignIdentifier.ToString()));
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="campaignIdentifier"></param>
		private static void TriggerTodaysNotification(Guid campaignIdentifier)
		{
			var message = new Message
			{
				MessageId = Guid.NewGuid().ToString(),
			};

			message.UserProperties.Add(Constants.MessageProperties.Type, Enumerations.AppMessageType.CampaignManagement.ToString());
			message.UserProperties.Add(Constants.MessageProperties.CampaignManagement.CampaignIdentifier, campaignIdentifier);
			message.UserProperties.Add(Constants.MessageProperties.CampaignManagement.EventType, Enumerations.Events.LoadTodaysNotifications.ToString());

			ServiceBusUtilities.Send(ConfigurationManager.OmilosIntegrationConnection, ConfigurationManager.CampaignManagementTopicName, message);
		}
    } 
}
