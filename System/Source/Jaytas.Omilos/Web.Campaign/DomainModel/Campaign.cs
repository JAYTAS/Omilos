using Jaytas.Omilos.Common.Domain;
using Jaytas.Omilos.Common.Enumerations;
using System;
using System.Collections.Generic;

namespace Jaytas.Omilos.Web.Service.Campaign.DomainModel
{
	/// <summary>
	/// 
	/// </summary>
	public partial class Campaign : GuidFieldLongBaseEntity
	{
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
		
		/// <summary>
		/// 
		/// </summary>
		public virtual ICollection<CampaignInstance> CampaignInstances { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual ICollection<MessageTemplate> MessageTemplates { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual Schedule Schedule { get; set; }
	}
}
