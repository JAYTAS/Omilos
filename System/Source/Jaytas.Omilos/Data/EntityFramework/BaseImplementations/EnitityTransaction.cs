using Jaytas.Omilos.Data.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Data.EntityFramework.BaseImplementations
{
	/// <summary>
	/// 
	/// </summary>
	public class EnitityTransaction : ITransaction
	{
		/// <summary>
		/// 
		/// </summary>
		private readonly IDbContextTransaction _transaction;

		/// <summary>
		/// Initializes a new instance of the <see cref="EnitityTransaction" /> class.
		/// </summary>
		/// <param name="transaction">The transaction.</param>
		public EnitityTransaction(IDbContextTransaction transaction)
		{
			_transaction = transaction;
		}

		/// <summary>
		/// Commits the underlying store transaction
		/// </summary>
		public void Commit()
		{
			_transaction?.Commit();
		}

		/// <summary>
		/// Rolls back the underlying store transaction
		/// </summary>
		public void Rollback()
		{
			_transaction?.Rollback();
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			_transaction?.Dispose();
		}
	}
}
