using Jaytas.Omilos.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Web.Service.Models.Campaign
{
	/// <summary>
	/// 
	/// </summary>
	public class RecurrencePattern
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
		public Weeks WeekOfMonth { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int? DayOfMonth { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Months? MonthOfYear { get; set; }
	}
}
