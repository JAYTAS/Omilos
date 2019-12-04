using Jaytas.Omilos.Common.Domain;
using System;

namespace Jaytas.Omilos.Web.Service.Campaign.DomainModel
{
	/// <summary>
	/// 
	/// </summary>
	public partial class CampaignInstance : GuidFieldLongBaseEntity
	{
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
		public Guid CampaignId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual CampaignInstanceException CampaignInstanceException { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual Campaign Campaign { get; set; }
	}
}
