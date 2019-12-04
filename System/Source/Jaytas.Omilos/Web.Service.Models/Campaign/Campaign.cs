using Jaytas.Omilos.Common.Enumerations;
using Jaytas.Omilos.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Web.Service.Models.Campaign
{
	public class Campaign
	{
		/// <summary>
		/// 
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid SubscriptionId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? GroupId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public NotificationChannels NotificationChannels { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool IsWelcomeMessageRequired { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool IsRemainderMessageRequired { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool IsOverDueMessageRequired { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public CampaignStatus Status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string CampaignManagerEmailId { get; set; }
	}
}
