using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Web.Common
{
	/// <summary>
	/// Interface for have links.
	/// </summary>
	public interface IHaveLinks
	{
		/// <summary>
		/// Get of links related to an resource.
		/// </summary>
		List<Link> Links { get; }
	}
}
