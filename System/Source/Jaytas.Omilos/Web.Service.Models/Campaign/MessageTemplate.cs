using Jaytas.Omilos.Common.Enumerations;
using Jaytas.Omilos.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Web.Service.Models.Campaign
{
	/// <summary>
	/// 
	/// </summary>
	public class MessageTemplate
	{
		/// <summary>
		/// 
		/// </summary>
		public Guid Id { get; set; }

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
	}
}
