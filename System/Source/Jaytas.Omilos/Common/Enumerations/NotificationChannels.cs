using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Common.Enumerations
{
	/// <summary>
	/// 
	/// </summary>
	[Flags]
	public enum NotificationChannels
	{
		/// <summary>
		/// 
		/// </summary>
		None = 0,

		/// <summary>
		/// 
		/// </summary>
		Email = 1,

		/// <summary>
		/// 
		/// </summary>
		SMS = 2,

		/// <summary>
		/// 
		/// </summary>
		Voice = 4,

		/// <summary>
		/// 
		/// </summary>
		All = ~0
	}
}
