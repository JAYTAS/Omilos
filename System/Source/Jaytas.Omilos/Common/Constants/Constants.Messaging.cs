using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Common
{
	/// <summary>
	/// 
	/// </summary>
	public partial struct Constants
	{
		/// <summary>
		/// 
		/// </summary>
		public struct ServiceBus
		{
			/// <summary>
			/// 
			/// </summary>
			public struct Topics
			{
				public const string CampaignManagement = "CampaignManagement";
			}

			/// <summary>
			/// 
			/// </summary>
			public struct Subscriptions
			{

			}

			/// <summary>
			/// 
			/// </summary>
			public struct SubscriptionFilters
			{

			}

			/// <summary>
			/// 
			/// </summary>
			public struct MessageProperties
			{
				/// <summary>
				/// RequestId
				/// </summary>
				public const string RequestId = "RequestId";

				/// <summary>
				/// MessageType
				/// </summary>
				public const string Type = "Type";

				/// <summary>
				/// 
				/// </summary>
				public struct CampaignManagement
				{
					public const string EventType = "EventType";

					public const string CampaignIdentifier = "CampaignIdentifier";

					public struct AdditionalProperties
					{
						public const string CampaignStartDate = "CampaignStartDate";

						public const string CampaignStartTime = "CampaignStartTime";

						public const string CampaignTimeZone = "CampaignTimeZone";
					}
				}
			}
		}
	}
}