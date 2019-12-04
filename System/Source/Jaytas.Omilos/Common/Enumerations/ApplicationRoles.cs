using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Common.Enumerations
{
	/// <summary>
	/// 
	/// </summary>
	[Flags]
	public enum ApplicationRoles
	{
		/// <summary>
		/// 
		/// </summary>
		NONE = 0,

		/// <summary>
		/// 
		/// </summary>
		App_Owner = 1,

		/// <summary>
		/// 
		/// </summary>
		Subscription_Owner = 2,

		/// <summary>
		/// 
		/// </summary>
		Subscripton_Admin = 4,

		/// <summary>
		/// 
		/// </summary>
		Campaign_Manager = 8,

		/// <summary>
		/// For all Role
		/// </summary>
		All = ~0
	}
}
