using Jaytas.Omilos.Common.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Common.Domain
{
	/// <summary>
	/// 
	/// </summary>
	public class CustomBaseFieldMapper : IBaseFieldMapper
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="idFieldName"></param>
		public CustomBaseFieldMapper(string idFieldName = Constants.DefaultFieldMappings.PrimaryKey)
		{
			Id = idFieldName;
		}

		/// <summary>
		/// 
		/// </summary>
		public string Id { get; private set; }
	}
}
