
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Jayatas.Omilos.Functions.Common.Common;
using System.Collections.Generic;
using Jayatas.Omilos.Functions.Common.Database;

namespace Jayatas.Omilos.Functions.Notification.RecordUserResponse
{
    public static class RecordUserResponse
    {
        [FunctionName(nameof(RecordUserResponse))]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, Constants.HttpVerb.Get, Route = Constants.Route.NotificationResponse)]HttpRequest req, string notificationId, int notificationChannel, TraceWriter log)
		{
			log.Info("C# HTTP trigger function processed a request.");
			log.Info($"NotificationId : {notificationId}. NotificationChannel : {notificationChannel}");

			var parameters = new Dictionary<string, object>
			{
				{ Constants.StoreProcedures.RecordUserResponse.Parameters.NotificationId, notificationId },
				{ Constants.StoreProcedures.RecordUserResponse.Parameters.NotificationChannel, notificationChannel },
				{ Constants.StoreProcedures.RecordUserResponse.Parameters.Response, "OptOut" }
			};

			MySqlUtilities.ExecuteNonQuery(ConfigurationManager.OmilosConnection,
			  							   Constants.StoreProcedures.RecordUserResponse.Name,
										   parameters);

			return new OkResult();
		}
    }
}