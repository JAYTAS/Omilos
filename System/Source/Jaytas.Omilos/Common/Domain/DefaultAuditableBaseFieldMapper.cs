using Jaytas.Omilos.Common.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Common.Domain
{
	/// <summary>
	/// 
	/// </summary>
	/// 
	public class DefaultAuditableBaseFieldMapper : IAuditableBaseFieldMapper
	{
		/// <summary>
		/// 
		/// </summary>
		public string CreatedDate => Constants.DefaultFieldMappings.CreatedDate;

		/// <summary>
		/// 
		/// </summary>
		public string CreatedBy => Constants.DefaultFieldMappings.CreatedBy;

		/// <summary>
		/// 
		/// </summary>
		public string ModifiedDate => Constants.DefaultFieldMappings.ModifiedDate;

		/// <summary>
		/// 
		/// </summary>
		public string ModifiedBy => Constants.DefaultFieldMappings.ModifiedBy;

		/// <summary>
		/// 
		/// </summary>
		public string Id => Constants.DefaultFieldMappings.PrimaryKey;
	}
}
