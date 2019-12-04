using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Common.Domain.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface IFieldEntity<TFieldEntityType> where TFieldEntityType : struct
	{
		/// <summary> Gets or sets the identifier. </summary>
		///
		/// <value> The Exposed identifier. </value>
		TFieldEntityType ExposedId { get; set; }

		/// <summary>
		/// Generates exposed field
		/// </summary>
		void GenerateExposedField();
	}
}