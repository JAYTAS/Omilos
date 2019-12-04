using Jaytas.Omilos.Common.Domain.Interfaces;
using Jaytas.Omilos.Common.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Providers
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <typeparam name="TBaseEntityType"></typeparam>
	public interface ICrudBaseProvider<TEntity, TBaseEntityType> : IBaseProvider
			where TEntity : class,
				  IBaseEntity<TBaseEntityType> where TBaseEntityType : struct
	{
		/// <summary>
		/// Creates the specified <paramref name="domain"/>.
		/// </summary>
		/// <param name="domain">The <paramref name="domain"/>.</param>
		/// <returns>A DomainId.</returns>
		Task<TBaseEntityType> CreateAsync(TEntity domain);

		/// <summary>
		/// Creates the specified <paramref name="domains"/>.
		/// </summary>
		/// <param name="domains">The <paramref name="domains"/>.</param>
		Task CreateAsync(IEnumerable<TEntity> domains);

		/// <summary>
		/// Deletes the specified identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		Task DeleteAsync(TBaseEntityType id);

		/// <summary>
		/// Deletes the specified identifier.
		/// </summary>
		/// <param name="entity">The entity to be deleted.</param>
		Task DeleteAsync(TEntity c);

		/// <summary>
		/// Updates the specified <paramref name="domain"/>.
		/// </summary>
		/// <param name="domain">The <paramref name="domain"/>.</param>
		Task UpdateAsync(TEntity domain);

		/// <summary>
		/// Updates the specified <paramref name="domains"/>.
		/// </summary>
		/// <param name="domains">The <paramref name="domains"/>.</param>
		Task UpdateAsync(IEnumerable<TEntity> domains);

		/// <summary>
		/// Gets the specified identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>A TModel.</returns>
		Task<TEntity> GetAsync(TBaseEntityType id);
	}
}
