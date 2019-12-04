using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Data.EntityFramework.Interfaces
{
	/// <summary>
	/// Interface for database context.
	/// </summary>
	/// <seealso cref="T:IDisposable"/>
	public interface IDbContext : IDisposable
	{
		/// <summary>
		/// Creates a Database instance for <see langword="this"/> context that allows for
		/// creation/deletion/existence checks for the underlying database.
		/// </summary>
		/// <value>The database.</value>
		DatabaseFacade Database { get; }

		/// <summary>
		/// Provides access to features of the context that deal with change tracking of entities.
		/// </summary>
		ChangeTracker ChangeTracker { get; }

		/// <summary>
		/// Gets a <see cref="T:Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry" /> object for the
		/// given entity providing access to information about the entity and the ability to perform
		/// actions on the entity.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="entity">The entity.</param>
		/// <returns>
		/// An entry for the entity.
		/// </returns>
		EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

		/// <summary>
		/// Saves the changes.
		/// </summary>
		/// <returns>An int.</returns>
		int SaveChanges();

		/// <summary>
		/// Saves the changes asynchronous.
		/// </summary>
		/// <returns>A Task&lt;int&gt;</returns>
		Task<int> SaveChangesAsync();

		/// <summary>
		/// Saves the changes asynchronous.
		/// </summary>
		/// <param name="cancellationToken">The cancellation token.</param>
		/// <returns>A Task&lt;int&gt;</returns>
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
