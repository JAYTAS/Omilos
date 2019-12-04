using Microsoft.EntityFrameworkCore;
using Jaytas.Omilos.Data.EntityFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Jaytas.Omilos.Data.EntityFramework.BaseImplementations
{
	public abstract class MicroServiceDbContext<T> : DbContext, IDbContext where T : DbContext
	{
		/// <summary>
		/// Stores all log messages until they are logged (at dispose time)
		/// </summary>
		private readonly StringBuilder _logBuffer;
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="options"></param>
		public MicroServiceDbContext(DbContextOptions<T> options) : base(options)
		{
		}
		
		/// <summary>
		/// Saves all changes made in <see langword="this"/> context to the underlying database.
		/// </summary>
		/// <returns>
		/// The number of state entries written to the underlying database. This can include state
		/// entries for entities and/or relationships. Relationship state entries are created for
		/// many-to- many relationships and relationships where there is no foreign key property
		/// included in the entity class (often referred to as independent associations).
		/// </returns>
		/// <seealso cref="M:System.Data.Entity.DbContext.SaveChanges()"/>
		public override int SaveChanges()
		{
			BeforeSave();
			return base.SaveChanges();
		}

		/// <summary>
		/// Asynchronously saves all changes made in <see langword="this"/> context to the underlying database.
		/// </summary>
		/// <remarks>
		/// Multiple active operations on the same context instance are not supported. Use 'await' to
		/// ensure that any asynchronous operations have completed before calling another method on
		/// <see langword="this"/> context.
		/// </remarks>
		/// <returns>
		/// A task that represents the asynchronous save operation. The task result contains the
		/// number of state entries written to the underlying database. This can include state
		/// entries for entities and/or relationships. Relationship state entries are created for
		/// many-to-many relationships and relationships where there is no foreign key property
		/// included in the entity class (often referred to as independent associations).
		/// </returns>
		/// <seealso cref="M:Microsoft.EntityFrameworkCore.DbContext.SaveChangesAsync()"/>
		public async Task<int> SaveChangesAsync()
		{
			BeforeSave();
			return await base.SaveChangesAsync(CancellationToken.None).ConfigureAwait(true);
		}

		/// <summary>
		/// Asynchronously saves all changes made in <see langword="this"/> context to the underlying database.
		/// </summary>
		/// <remarks>
		/// Multiple active operations on the same context instance are not supported. Use 'await' to
		/// ensure that any asynchronous operations have completed before calling another method on
		/// <see langword="this"/> context.
		/// </remarks>
		/// <param name="cancellationToken">
		/// A <see cref="CancellationToken"/> to observe while waiting for the task to complete.
		/// </param>
		/// <returns>
		/// A task that represents the asynchronous save operation. The task result contains the
		/// number of state entries written to the underlying database. This can include state
		/// entries for entities and/or relationships. Relationship state entries are created for
		/// many-to-many relationships and relationships where there is no foreign key property
		/// included in the entity class (often referred to as independent associations).
		/// </returns>
		/// <seealso cref="M:Microsoft.EntityFrameworkCore..DbContext.SaveChangesAsync(CancellationToken)"/>
		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
		{
			BeforeSave();
			return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(true);
		}

		/// <summary>
		/// Logs all database messages to the LoggingProvider.
		/// </summary>
		/// <param name="message">The <paramref name="message"/>.</param>
		private void Log(string message)
		{
			_logBuffer.Append(message);
		}

		/// <summary>
		/// Intended to be called before the Context SaveChanges to perform any checks or operations that
		/// are common enough across all entities to be done at this low level (such as updating the Audit columns)
		/// </summary>
		/// <exception cref="System.ArgumentOutOfRangeException"></exception>
		private void BeforeSave()
		{
			//var userId = _identityProvider?.UserId.ToString();
			//var changedEntities = ChangeTracker.Entries();

			//foreach (var changedEntity in changedEntities)
			//{
			//	var entity = changedEntity.Entity as IAuditableEntity;
			//	if (entity == null) continue;

			//	switch (changedEntity.State)
			//	{
			//		case EntityState.Added:
			//			entity.CreatedBy = userId;
			//			entity.CreatedDate = DateTime.UtcNow;
			//			entity.ModifiedBy = userId;
			//			entity.ModifiedDate = entity.CreatedDate;
			//			break;

			//		case EntityState.Modified:
			//			entity.ModifiedBy = userId;
			//			entity.ModifiedDate = DateTime.UtcNow;
			//			break;
			//		case EntityState.Detached:
			//			break;
			//		case EntityState.Unchanged:
			//			break;
			//		case EntityState.Deleted:
			//			break;
			//		default:
			//			throw new ArgumentOutOfRangeException();
			//	}
			//}
		}
	}
}
