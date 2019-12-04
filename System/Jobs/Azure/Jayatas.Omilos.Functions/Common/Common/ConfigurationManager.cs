using System;
using System.Collections.Generic;
using System.Text;

namespace Jayatas.Omilos.Functions.Common.Common
{
	public class ConfigurationManager
	{
		private static string _environmentName;

		public static string EnvironmentName
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_environmentName))
				{
					_environmentName = Utilities.GetOrDefaultEnvironmentValue<string>(Constants.ConfigKeys.EnvironmentName, null);
				}
				return _environmentName;
			}
		}

		private static string _omilosConnection;

		public static string OmilosConnection
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_omilosConnection))
				{
					_omilosConnection = Utilities.GetOrDefaultEnvironmentValue<string>(Constants.ConfigKeys.OmilosDbConnection, null);
				}
				return _omilosConnection;
			}
		}

		private static string _twilioAccountId;

		public static string TwilioAccountId
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_twilioAccountId))
				{
					_twilioAccountId = Utilities.GetOrDefaultEnvironmentValue<string>(Constants.ConfigKeys.TwilioAccountId, null);
				}
				return _twilioAccountId;
			}
		}

		private static string _twilioAccountSecret;

		public static string TwilioAccountSecret
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_twilioAccountSecret))
				{
					_twilioAccountSecret = Utilities.GetOrDefaultEnvironmentValue<string>(Constants.ConfigKeys.TwilioAccountSecret, null);
				}
				return _twilioAccountSecret;
			}
		}

		private static string _twilioPhoneNumber;

		public static string TwilioPhoneNumber
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_twilioPhoneNumber))
				{
					_twilioPhoneNumber = Utilities.GetOrDefaultEnvironmentValue<string>(Constants.ConfigKeys.TwilioPhoneNumber, null);
				}
				return _twilioPhoneNumber;
			}
		}

		private static string _omilosIntegrationConnection;

		public static string OmilosIntegrationConnection
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_omilosIntegrationConnection))
				{
					_omilosIntegrationConnection = Utilities.GetOrDefaultEnvironmentValue<string>(Constants.ConfigKeys.OmilosIntegrationConnection, null);
				}
				return _omilosIntegrationConnection;
			}
		}

		private static string _campaignManagementTopicName;

		public static string CampaignManagementTopicName
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_campaignManagementTopicName))
				{
					_campaignManagementTopicName = Utilities.GetOrDefaultEnvironmentValue<string>(Constants.ConfigKeys.OmilosCampaignManagement, null);
				}
				return _campaignManagementTopicName;
			}
		}

		private static string _notificationResponseRoute;

		public static string NotificationResponseRoute
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_notificationResponseRoute))
				{
					_notificationResponseRoute = Utilities.GetOrDefaultEnvironmentValue<string>(Constants.ConfigKeys.OmilosNotificationResponseRoute, null);
				}
				return _notificationResponseRoute;
			}
		}

		private static string _bitlyKey;

		public static string BitlyKey
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_bitlyKey))
				{
					_bitlyKey = Utilities.GetOrDefaultEnvironmentValue<string>(Constants.ConfigKeys.BitlyKey, null);
				}
				return _bitlyKey;
			}
		}

		private static string _bitlyGroupId;

		public static string BitlyGroupId
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_bitlyGroupId))
				{
					_bitlyGroupId = Utilities.GetOrDefaultEnvironmentValue<string>(Constants.ConfigKeys.BitlyGroupId, null);
				}
				return _bitlyGroupId;
			}
		}
	}
}