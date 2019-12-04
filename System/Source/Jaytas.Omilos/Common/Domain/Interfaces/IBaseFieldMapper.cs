using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Common.Domain.Interfaces
{
	/// <summary>
	/// Denotes the common properties necessary for entities that are auditable
	/// </summary>
	public interface IBaseFieldMapper
	{
		/// <summary>
		/// Gets or sets  the name of the field that corresponds to Id.
		/// </summary>
		string Id { get; }
	}
}
