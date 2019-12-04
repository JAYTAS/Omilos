using System;
using System.Collections.Generic;
using Jayatas.Omilos.Functions.Common.Common;
using Jayatas.Omilos.Functions.Common.Database;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace Jayatas.Omilos.Functions.Campaign.LoadSmsNotificationForCampaignRemainder
{
    public static class LoadSmsNotificationForCampaignRemainder
    {
        [FunctionName(nameof(LoadSmsNotificationForCampaignRemainder))]
        public static void Run([TimerTrigger(Constants.NameResolverKeys.LoadCampaignSmsRemainderInterval)]TimerInfo myTimer, TraceWriter log)
        {
			var parameters = new Dictionary<string, object>
			{
				{   Constants.StoreProcedures.LoadSmsNotificationForCampaignRemainder.Parameters.CampaignId, null }
			};

			MySqlUtilities.ExecuteNonQuery(ConfigurationManager.OmilosConnection,
										   Constants.StoreProcedures.LoadSmsNotificationForCampaignRemainder.Name,
										   parameters);
		}
    }
}