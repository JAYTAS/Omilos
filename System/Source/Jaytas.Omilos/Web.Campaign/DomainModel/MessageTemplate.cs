using Jaytas.Omilos.Common.Domain;
using Jaytas.Omilos.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Campaign.DomainModel
{
	/// <summary>
	/// 
	/// </summary>
	public partial class MessageTemplate : GuidFieldLongBaseEntity
	{
		/// <summary>
		/// 
		/// </summary>
		public Guid CampaignId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public NotificationChannels NotificationChannel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string WelcomeMessage { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string RemainderMessage { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string OverDueMessage { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual Campaign Campaign { get; set; }
	}
}
