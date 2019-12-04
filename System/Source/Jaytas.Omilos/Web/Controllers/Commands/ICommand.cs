using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Web.Controllers.Commands
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TModel"></typeparam>
	/// <typeparam name="TModelBaseType"></typeparam>
	public interface ICommand<TModel, TModelBaseType> where TModelBaseType : struct
	{
		/// <summary>
		/// 
		/// </summary>
		TModel Resource { get; set; }

		/// <summary>
		/// 
		/// </summary>
		TModelBaseType ResourceId { get; set; }

		/// <summary>
		/// Additional Properties to set
		/// </summary>
		Dictionary<String, dynamic> CommandProperties { get; set; }
	}
}
