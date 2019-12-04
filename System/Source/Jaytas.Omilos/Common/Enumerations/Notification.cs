using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Common.Enumerations
{
	public struct Notification
	{
		/// <summary>
		/// 
		/// </summary>
		public enum Source
		{
			None,

			Campaign,

			CampaignInstance
		}

		/// <summary>
		/// 
		/// </summary>
		public enum MessageType
		{
			None,

			Welcome,

			Remainder,

			OverDue
		}

		public enum Status
		{
			Pending,

			Initiated,

			Sent
		}
	}
}
