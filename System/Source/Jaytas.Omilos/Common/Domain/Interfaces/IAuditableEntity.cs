using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Common.Domain.Interfaces
{
	/// <summary>
	/// Denotes the common properties necessary for entities that are auditable
	/// </summary>
	public interface IAuditableEntity
	{
		/// <summary>
		/// get and sets the DateTime this instance was created.
		/// </summary>
		DateTime CreatedDate { get; set; }

		/// <summary>
		/// get and sets the user reference that created this instance.
		/// </summary>
		/// <value>The create user URI.</value>
		string CreatedBy { get; set; }

		/// <summary>
		/// Gets or sets the modified date time.
		/// </summary>
		/// <value>The modified date time.</value>
		DateTime ModifiedDate { get; set; }

		/// <summary>
		/// Gets or sets URI of the modified user.
		/// </summary>
		/// <value>The modified user URI.</value>
		string ModifiedBy { get; set; }
	}
}
