using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Data.EntityFramework.Interfaces
{
	/// <summary>
	/// Abstraction of underlying database transaction objects to aide in testing.
	/// </summary>
	public interface ITransaction : IDisposable
	{
		/// <summary>
		/// Commits this instance.
		/// </summary>
		void Commit();

		/// <summary>
		/// Rollbacks this instance.
		/// </summary>
		void Rollback();
	}
}