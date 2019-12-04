using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Jayatas.Omilos.Functions.Common.Common;
using Jayatas.Omilos.Functions.Common.Database;
using System.Collections.Generic;
using System.Linq;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.Threading.Tasks;
using Refit;
using Jayatas.Omilos.Functions.Common.ServiceClients.Bitly;
using Newtonsoft.Json;

namespace Jayatas.Omilos.Functions.Notification.SendSmsNotification
{
	/// <summary>
	/// 
	/// </summary>
	public static class SendSmsNotification
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="myTimer"></param>
		/// <param name="log"></param>
		[FunctionName(nameof(SendSmsNotification))]
		public static void Run([TimerTrigger(Constants.NameResolverKeys.SmsNotificationInterval)]TimerInfo myTimer, TraceWriter log)
		{
			var utcTimeSpan = DateTime.UtcNow.AddMinutes(30).TimeOfDay;
			var minute = (int)(Math.Round(utcTimeSpan.Minutes / 5.0) * 5);

			log.Info("Timestamp : " + utcTimeSpan);
			log.Info("Minute : " + minute);

			var parameters = new Dictionary<string, object>
			{
				{ Constants.StoreProcedures.GetSmsNotification.Parameters.Status, 0 },
				{ Constants.StoreProcedures.GetSmsNotification.Parameters.NotificationDate, DateTime.UtcNow },
				{ Constants.StoreProcedures.GetSmsNotification.Parameters.NotificationTime, new TimeSpan(utcTimeSpan.Hours,  minute, 0) }
			};

			var pendingNotifications = MySqlUtilities.ExecuteQuery<NotificationModel>(ConfigurationManager.OmilosConnection,
																					  Constants.StoreProcedures.GetSmsNotification.Name,
																					  parameters).ToList();

			log.Info("Number of Notificaction to be sent :" + pendingNotifications.Count());

			if(!pendingNotifications.Any())
			{
				return;
			}

			SendNotifications(pendingNotifications, log);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pendingNotifications"></param>
		/// <param name="log"></param>
		public static void SendNotifications(List<NotificationModel> pendingNotifications, TraceWriter log)
		{
			TwilioClient.Init(ConfigurationManager.TwilioAccountId, ConfigurationManager.TwilioAccountSecret);

			var messageIds = new List<string>();

			foreach (var pendingNotification in pendingNotifications)
			{
				var responseUri = string.Format(ConfigurationManager.NotificationResponseRoute, pendingNotification.NotificationId, 2);
				var shortenRequestModel = new ShortenRequestModel
				{
					LongUrl = responseUri,
					GroupId = ConfigurationManager.BitlyGroupId
				};
				log.Info($"Response Uri : {responseUri}");
				var shortenResponse = RestClient.PostAsync<ShortenResponseModel>(Constants.Common.BitlyHostName, 
																				 "/v4/shorten",
																				 ConfigurationManager.BitlyKey, 
																				 JsonConvert.SerializeObject(shortenRequestModel)).GetAwaiter().GetResult();

				log.Info($"Shorten Uri : {shortenResponse?.Link}");
				var messageBody = $"{pendingNotification.Message} \n\n Please click : {(shortenResponse != null ? shortenResponse.Link : responseUri)} to optout.";

				var messageId = MessageResource.CreateAsync(body: messageBody,
															from: new Twilio.Types.PhoneNumber(ConfigurationManager.TwilioPhoneNumber),
															to: new Twilio.Types.PhoneNumber(pendingNotification.PhoneNumber)).GetAwaiter().GetResult();
			}
		}
	}
}