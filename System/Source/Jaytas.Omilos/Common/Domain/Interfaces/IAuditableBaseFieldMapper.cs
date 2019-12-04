using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Common.Domain.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface IAuditableBaseFieldMapper : IBaseFieldMapper
	{
		/// <summary>
		/// get and sets the name of the field that corresponds to createddate.
		/// </summary>
		string CreatedDate { get; }

		/// <summary>
		/// get and sets  the name of the field that corresponds to CreatedBy.
		/// </summary>
		string CreatedBy { get; }

		/// <summary>
		/// Gets or sets  the name of the field that corresponds to ModifiedDate.
		/// </summary>
		string ModifiedDate { get; }

		/// <summary>
		/// Gets or sets  the name of the field that corresponds to ModifiedBy.
		/// </summary>
		string ModifiedBy { get; }
	}
}
