using Jaytas.Omilos.Common.Disposable;
using Jaytas.Omilos.Data.EntityFramework.Interfaces;

namespace Jaytas.Omilos.Data.EntityFramework.BaseImplementations
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class BaseEntityRepository<T> : Disposable, IBaseEntityRepository where T : IDbContext
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EntityFrameworkRepository{T}" /> class.
		/// </summary>
		/// <param name="dbContext">The database context.</param>
		protected BaseEntityRepository(T dbContext)
		{
			DbContext = dbContext;
		}

		/// <summary>
		/// Gets the EF database context.
		/// </summary>
		/// <value>
		/// The database context.
		/// </value>
		protected T DbContext { get; }

		/// <summary>
		/// Begins the transaction.
		/// </summary>
		/// <returns></returns>
		public ITransaction BeginTransaction()
		{
			var existing = DbContext.Database.CurrentTransaction;
			if (existing == null)
			{
				return new EnitityTransaction(DbContext.Database.BeginTransaction());
			}
			// EF only supports one transaction, so if one already exists, create this faux-transaction object that will essentially do nothing
			return new EnitityTransaction(null);
		}

		/// <summary>
		/// Dispose implementation.
		/// </summary>
		protected override void DisposeImplementation()
		{
			using (DbContext) { }

			base.DisposeImplementation();
		}
	}
}
