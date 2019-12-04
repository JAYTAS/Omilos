using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Jaytas.Omilos.Common.Models
{
	/// <summary>
	/// Represents a link to one resource.  The Rel (relationship) attribute defines what resource it links to
	/// and the Href is the URI to that resource.
	/// <example>A class should reference its own 'id' (which publicly is a URI) via a Rel=self link.</example>
	/// </summary>
	[DebuggerDisplay("{Rel}:{Href}")]
	public class Link
	{
		/// <summary>
		/// Gets or sets the URI.
		/// </summary>
		/// <value>The href.</value>
		public string Href { get; set; }
		/// <summary>
		/// Add details to the entity
		/// </summary>
		public Dictionary<string, dynamic> Details { get; set; }

		/// <summary>
		/// Gets or sets the relationship key.
		/// </summary>
		/// <value>The relative.</value>
		public string Rel { get; set; }
	}
}
