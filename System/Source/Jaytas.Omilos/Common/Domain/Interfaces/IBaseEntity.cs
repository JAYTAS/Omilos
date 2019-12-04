using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Common.Domain.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface IBaseEntity<TBaseEntityType> where TBaseEntityType : struct
	{
		/// <summary> Gets or sets the identifier. </summary>
		///
		/// <value> The identifier. </value>
		TBaseEntityType Id { get; set; }
	}
}
