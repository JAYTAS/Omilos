using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Web.Service.Models.Campaign.Input
{
	/// <summary>
	/// 
	/// </summary>
	public class Schedule
	{
		/// <summary>
		/// 
		/// </summary>
		public bool IsRecurrence { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string TimeZone { get; set; }

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
		public RecurrencePattern RecurrencePattern { get; set; }
	}
}
