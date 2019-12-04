using Jaytas.Omilos.Common.Domain.Interfaces;
using Jaytas.Omilos.Common.Providers;
using Jaytas.Omilos.Web.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Providers
{
	/// <summary>
	/// Base provider class providing the default CRUD behaviors and extensibility for additional
	/// validation for the CRUD operations.
	/// </summary>
	/// <typeparam name="TEntity">The type of the t entity.</typeparam>
	/// <typeparam name="TRepository">The type of the t repository.</typeparam>
	/// <typeparam name="TBaseEntityType">the base enity type lile (Guid, int, long..)</typeparam>
	public abstract class CrudBaseProvider<TEntity, TRepository, TBaseEntityType> : ICrudBaseProvider<TEntity, TBaseEntityType>
							where TEntity : class, IBaseEntity<TBaseEntityType>
							where TRepository : class, ICrudBaseEntityRepository<TEntity, TBaseEntityType>
							where TBaseEntityType : struct
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="CrudBaseProvider{TEntity, TRepository,TBaseEntityType}" /> class.
		/// </summary>
		/// <param name="repository">The repository.</param>
		/// <param name="messageFactory">factory class to create message</param>
		/// <param name="messageSenderFactory">message sender</param>
		protected CrudBaseProvider(TRepository repository)
		{
			Repository = repository;
		}

		/// <summary>
		/// The repository.
		/// </summary>
		public TRepository Repository { get; }

		/// <summary>
		/// Asserts the entity to create is valid. If the entity is not valid, an appropriate
		/// Argument**Exception should be thrown containing a listing of all the validation errors.
		/// </summary>
		/// <param name="domains">The domain objects.</param>
		public abstract Task AssertEntityToCreateIsValidAsync(IEnumerable<TEntity> domains);

		/// <summary>
		/// Asserts the entity to create is valid. If the entity is not valid, an appropriate
		/// Argument**Exception should be thrown containing a listing of all the validation errors.
		/// </summary>
		/// <param name="domains">The domain objects.</param>
		public abstract Task AssertEntityToUpdateIsValidAsync(IEnumerable<TEntity> domains);

		/// <summary>
		/// Asserts the entity to create is valid. If the entity is not valid, an appropriate
		/// Argument**Exception should be thrown containing a listing of all the validation errors.
		/// </summary>
		/// <param name="identifiers">The identifiers</param>
		public abstract Task AssertEntityToDeleteIsValidAsync(IEnumerable<TBaseEntityType> identifiers);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="domain"></param>
		/// <returns></returns>
		public async virtual Task<TBaseEntityType> CreateAsync(TEntity domain)
		{
			await AssertEntityToCreateIsValidAsync(new List<TEntity> { domain }).ConfigureAwait(true);

			return await Repository.AddAsync(domain);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="domains"></param>
		/// <returns></returns>
		public async virtual Task CreateAsync(IEnumerable<TEntity> domains)
		{
			await AssertEntityToCreateIsValidAsync(domains).ConfigureAwait(true);

			await Repository.AddRangeAsync(domains);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async virtual Task DeleteAsync(TBaseEntityType id)
		{
			await AssertEntityToDeleteIsValidAsync(new List<TBaseEntityType> { id }).ConfigureAwait(true);

			await Repository.DeleteAsync(id);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public async virtual Task DeleteAsync(TEntity entity)
		{
			await AssertEntityToDeleteIsValidAsync(new List<TBaseEntityType> { entity.Id }).ConfigureAwait(true);

			await Repository.DeleteAsync(entity);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async virtual Task<TEntity> GetAsync(TBaseEntityType id)
		{
			return await Repository.GetAsync(id);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="domain"></param>
		/// <returns></returns>
		public async virtual Task UpdateAsync(TEntity domain)
		{
			await AssertEntityToUpdateIsValidAsync(new List<TEntity> { domain }).ConfigureAwait(true);
			await Repository.UpdateAsync(domain);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="domains"></param>
		/// <returns></returns>
		public async virtual Task UpdateAsync(IEnumerable<TEntity> domains)
		{
			await AssertEntityToUpdateIsValidAsync(domains).ConfigureAwait(true);
			await Repository.UpdateAsync(domains);
		}
	}
}
