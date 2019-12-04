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
	public class LongBaseEntity : IBaseEntity<long>
	{
		/// <summary>
		/// 
		/// </summary>
		public long Id { get; set; }
	}
}