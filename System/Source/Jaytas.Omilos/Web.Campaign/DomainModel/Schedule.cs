using Jaytas.Omilos.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Campaign.DomainModel
{
	/// <summary>
	/// 
	/// </summary>
	public partial class Schedule : GuidFieldLongBaseEntity
	{
		/// <summary>
		/// 
		/// </summary>
		public Guid CampaignId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string TimeZone { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool IsRecurrence { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime StartDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime EndDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public TimeSpan StartTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public TimeSpan EndTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual RecurrencePattern RecurrencePattern { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual Campaign Campaign { get; set; }
	}
}
