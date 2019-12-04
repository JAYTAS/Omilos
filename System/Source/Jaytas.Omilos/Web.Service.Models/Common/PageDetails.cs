using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Web.Service.Models.Common
{
	/// <summary>
	/// Page details
	/// </summary>
	public class PageDetails
	{
		/// <summary>
		/// Records per page
		/// </summary>
		public int? PageSize { get; set; }

		/// <summary>
		/// current page number
		/// </summary>
		public int? PageNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string SearchText { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Dictionary<string, string> AdditionalSearchCriteria { get; set; }
	}
}
