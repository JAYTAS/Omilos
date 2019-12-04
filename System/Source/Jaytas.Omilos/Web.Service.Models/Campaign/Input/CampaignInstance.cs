using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Web.Service.Models.Campaign.Input
{
	public class CampaignInstance
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
	}
}
