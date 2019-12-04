using Jaytas.Omilos.Common.Domain.Interfaces;
using Jaytas.Omilos.Data.EntityFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Repositories
{
	/// <summary>
	/// Interface for repository.
	/// </summary>
	/// <typeparam name="TEntity">The type of the entity.</typeparam>
	/// <typeparam name="TBaseEntityType">The base entity type like (long, Guid..) </typeparam>
	/// <seealso cref="T:IDisposable"/>
	public interface ICrudBaseEntityRepository<TEntity, TBaseEntityType> : IBaseEntityRepository
			where TEntity : class,
				  IBaseEntity<TBaseEntityType> where TBaseEntityType : struct
	{
		/// <summary>
		/// Creates the specified <paramref name="entity"/> .
		/// </summary>
		/// <param name="entity">The <paramref name="entity"/> .</param>
		/// <returns>A DomainId.</returns>
		Task<TBaseEntityType> AddAsync(TEntity entity);

		/// <summary>
		/// Creates the specified <paramref name="entities"/> .
		/// </summary>
		/// <param name="entities">The <paramref name="entities"/> .</param>
		/// <returns>A DomainId.</returns>
		Task AddRangeAsync(IEnumerable<TEntity> entities);

		/// <summary>
		/// Updates the specified <paramref name="entity"/> .
		/// </summary>
		/// <param name="entity">The <paramref name="entity"/> .</param>
		Task UpdateAsync(TEntity entity);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		Task UpdateAsync(IEnumerable<TEntity> entity);

		/// <summary>
		/// Deletes the specified identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		Task DeleteAsync(TBaseEntityType id);

		/// <summary>
		/// Deletes the specified identifier.
		/// </summary>
		/// <param name="entity">The enity to be deleted.</param>
		Task DeleteAsync(TEntity entity);

		/// <summary>
		/// Deletes the specified identifier.
		/// </summary>
		/// <param name="expression">The identifiers.</param>
		Task DeleteAsync(Expression<Func<TEntity, bool>> expression);

		/// <summary>
		/// Gets the specified domain identifier.
		/// </summary>
		/// <param name="id">The domain identifier.</param>
		/// <returns>The by identifier.</returns>
		Task<TEntity> GetAsync(TBaseEntityType id);

		/// <summary>
		/// Gets many.
		/// </summary>
		/// <param name="expression">The <paramref name="expression"/> .</param>
		/// <returns>
		/// An enumerator that allows <see langword="foreach"/> to be used to process the entities in
		/// this collection.
		/// </returns>
		Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression);
	}
}
