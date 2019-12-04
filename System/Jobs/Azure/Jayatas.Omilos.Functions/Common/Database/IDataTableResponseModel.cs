using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Jayatas.Omilos.Functions.Common.Database
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IDataTableResponseModel<T>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		IEnumerable<T> Fill(DataTable table);
	}
}
