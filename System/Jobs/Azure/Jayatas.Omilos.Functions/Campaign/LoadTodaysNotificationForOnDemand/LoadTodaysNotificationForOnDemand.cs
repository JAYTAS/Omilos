using Jayatas.Omilos.Functions.Common.Common;
using Jayatas.Omilos.Functions.Common.Database;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.Collections.Generic;

namespace Jayatas.Omilos.Functions.Campaign.LoadTodaysNotificationForOnDemand
{
    public static class LoadTodaysNotificationForOnDemand
	{
        [FunctionName(nameof(LoadTodaysNotificationForOnDemand))]
        public static void Run([ServiceBusTrigger(Constants.NameResolverKeys.CampaignManagementTopic, Constants.Subscriptions.LoadTodaysNotificationsOnDemand, Connection = Constants.ConfigKeys.OmilosIntegrationConnection)]Message message, TraceWriter log)
		{
			var campaignIdentifier = message.UserProperties[Constants.MessageProperties.CampaignManagement.CampaignIdentifier];
			var parameters = new Dictionary<string, object>
			{
				{
					Constants.StoreProcedures.GenerateCampaignInstances.Parameters.CampaignId, campaignIdentifier
				}
			};

			MySqlUtilities.ExecuteNonQuery(ConfigurationManager.OmilosConnection,
										   Constants.StoreProcedures.LoadSmsNotificationForCampaignRemainder.Name,
										   parameters);
		}
    }
}