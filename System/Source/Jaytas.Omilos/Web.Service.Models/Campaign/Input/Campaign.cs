using Jaytas.Omilos.Common.Enumerations;
using System;

namespace Jaytas.Omilos.Web.Service.Models.Campaign.Input
{
	public class Campaign
	{
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
		public string CampaignManagerEmailId { get; set; }
	}
}
