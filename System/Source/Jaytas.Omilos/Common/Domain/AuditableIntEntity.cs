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
	public class AuditableIntEntity : IntEntity, IAuditableEntity
	{
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreatedDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string CreatedBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime ModifiedDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string ModifiedBy { get; set; }
	}
}
