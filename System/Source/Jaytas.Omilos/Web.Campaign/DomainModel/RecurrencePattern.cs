using Jaytas.Omilos.Common.Domain;
using Jaytas.Omilos.Common.Enumerations;
using System;

namespace Jaytas.Omilos.Web.Service.Campaign.DomainModel
{
	/// <summary>
	/// 
	/// </summary>
	public partial class RecurrencePattern : LongBaseEntity
	{
		/// <summary>
		/// 
		/// </summary>
		public RecurringType RecurringType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int? SeparationCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int? MaxNumberOfOccurrences { get; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public Days? DaysOfWeek { get; set; }
		 
		/// <summary>
		/// 
		/// </summary>
		public Weeks? WeekOfMonth { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int? DayOfMonth { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Months MonthOfYear { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual Schedule Schedule { get; set; }
	}
}
