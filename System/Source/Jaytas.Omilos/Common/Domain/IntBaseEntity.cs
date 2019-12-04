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
	public class IntEntity : IBaseEntity<Int32>
	{
		/// <summary>
		/// 
		/// </summary>
		public Int32 Id { get; set; }
	}
}
