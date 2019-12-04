using Jaytas.Omilos.Common.Domain.Interfaces;
using Jaytas.Omilos.Common.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Providers
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	/// <typeparam name="TBaseEntityType"></typeparam>
	/// <typeparam name="TFieldEntityType"></typeparam>
	public interface ICrudByFieldBaseProvider<TEntity, TBaseEntityType, TFieldEntityType> : IBaseProvider
						where TEntity : class, IFieldEntity<TFieldEntityType>, IBaseEntity<TBaseEntityType>
						where TFieldEntityType : struct
						where TBaseEntityType : struct
	{
		/// <summary>
		/// Creates the specified <paramref name="domain"/>.
		/// </summary>
		/// <param name="domain">The <paramref name="domain"/>.</param>
		/// <returns>A DomainId.</returns>
		Task<TFieldEntityType> CreateAsync(TEntity domain);

		/// <summary>
		/// Creates the specified <paramref name="domains"/>.
		/// </summary>
		/// <param name="domains">The <paramref name="domains"/>.</param>
		Task CreateAsync(IEnumerable<TEntity> domains);

		/// <summary>
		/// Deletes the specified identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		Task DeleteAsync(TFieldEntityType id);

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
		Task<TEntity> GetAsync(TFieldEntityType id);
	}
}
