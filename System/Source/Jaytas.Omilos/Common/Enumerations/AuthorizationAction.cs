using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Common.Enumerations
{
	/// <summary>
	/// Defines an authorization action related to a set of permissions,
	/// e.g. permissions granted or denied
	/// </summary>
	public enum AuthorizationAction
	{
		/// <summary>
		/// Grants the associated permission
		/// </summary>
		Grant = 1,

		/// <summary>
		/// Denies the associated permission
		/// </summary>
		Deny = 2,
	}
}
