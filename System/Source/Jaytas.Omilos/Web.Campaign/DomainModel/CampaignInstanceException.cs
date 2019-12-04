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
	public partial class CampaignInstanceException : GuidFieldLongBaseEntity
	{
		/// <summary>
		/// 
		/// </summary>
		public Guid InstanceId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool IsRescheduled { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool IsCancelled { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public TimeSpan? StartTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public TimeSpan? EndTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual CampaignInstance CampaignInstance { get; set; }
	}
}
