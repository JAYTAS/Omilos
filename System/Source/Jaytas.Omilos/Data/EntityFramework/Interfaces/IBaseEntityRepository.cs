using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Jaytas.Omilos.Data.EntityFramework.Interfaces
{
	/// <summary>
	/// Common methods for Entity Framework based repository clases.
	/// </summary>
	public interface IBaseEntityRepository : IDisposable
	{
		/// <summary>
		/// Begins the transaction.
		/// </summary>
		/// <returns></returns>
		ITransaction BeginTransaction();
	}
}
