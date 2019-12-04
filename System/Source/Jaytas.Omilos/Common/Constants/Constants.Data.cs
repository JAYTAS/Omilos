using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Common
{
	public partial struct Constants
	{
		/// <summary>
		/// 
		/// </summary>
		public struct Schemas
		{
			/// <summary>
			/// 
			/// </summary>
			public struct Dbo
			{
				public const string Name = "dbo";
			}

			/// <summary>
			/// Schema for Account Management
			/// </summary>
			public struct Account
			{
				/// <summary>
				/// 
				/// </summary>
				public const string Name = "account";

				/// <summary>
				/// 
				/// </summary>
				public struct Tables
				{
					public const string Role = "role";

					public const string User = "user";

					public const string UserLoginDetail = "user_logindetail";

					public const string UserRole = "user_role";
				}
			}

			/// <summary>
			/// Schema for Campaign Management
			/// </summary>
			public struct Campaign
			{
				/// <summary>
				/// 
				/// </summary>
				public const string Name = "campaign";

				/// <summary>
				/// 
				/// </summary>
				public struct Tables
				{
					public const string Campaign = "campaign";

					public const string CampaignInstance = "campaign_instance";

					public const string CampaignInstanceException = "campaign_instance_exception";

					public const string MessageTemplate = "message_template";

					public const string Schedule = "schedule";

					public const string ScheduleRecurrencePattern = "schedule_recurrencepattern";
				}
			}

			/// <summary>
			/// Schema for Subscription Management
			/// </summary>
			public struct Subscription
			{
				/// <summary>
				/// 
				/// </summary>
				public const string Name = "subscription";

				/// <summary>
				/// 
				/// </summary>
				public struct Tables
				{
					public const string Subscription = "subscription";

					public const string Group = "group";

					public const string Contact = "contact";

					public const string GroupContactAssociation = "group_contact_association";
				}
			}

		}


		/// <summary>
		/// 
		/// </summary>
		public struct DefaultFieldMappings
		{
			/// <summary>
			/// 
			/// </summary>
			public const string CreatedDate = "CreatedDate";

			/// <summary>
			/// 
			/// </summary>
			public const string CreatedBy = "CreatedBy";

			/// <summary>
			/// 
			/// </summary>
			public const string ModifiedDate = "ModifiedDate";

			/// <summary>
			/// 
			/// </summary>
			public const string ModifiedBy = "ModifiedBy";

			/// <summary>
			/// 
			/// </summary>
			public const string PrimaryKey = "Id";
		}

		public struct CustomFeildMappings
		{
			/// <summary>
			/// 
			/// </summary>
			public const string UserId = "UserId";

			/// <summary>
			/// 
			/// </summary>
			public const string ScheduleId = "ScheduleId";

			/// <summary>
			/// 
			/// </summary>
			public const string GraphId = "GraphId";

			/// <summary>
			/// 
			/// </summary>
			public const string CampaignId = "CampaignId";

			/// <summary>
			/// 
			/// </summary>
			public const string MessageId = "MessageId";

			/// <summary>
			/// 
			/// </summary>
			public const string InstanceId = "InstanceId";

			/// <summary>
			/// 
			/// </summary>
			public const string CampaignInstanceExceptionId = "CampaignInstanceExceptionId";

			/// <summary>
			/// 
			/// </summary>
			public const string SubscriptionId = "SubscriptionId";

			/// <summary>
			/// 
			/// </summary>
			public const string GroupId = "GroupId";

			/// <summary>
			/// 
			/// </summary>
			public const string ContactId = "ContactId";
		}
	}
}
