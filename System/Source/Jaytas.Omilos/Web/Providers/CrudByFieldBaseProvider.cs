using Jaytas.Omilos.Common.Domain.Interfaces;
using Jaytas.Omilos.Common.Enumerations;
using Jaytas.Omilos.Messaging.ServiceBus.Topic.Interfaces;
using Jaytas.Omilos.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Providers
{
	public abstract class CrudByFieldBaseProvider<TEntity, TRepository, TBaseEntityType, TFieldEntityType> : ICrudByFieldBaseProvider<TEntity, TBaseEntityType, TFieldEntityType>
							where TEntity : class, IFieldEntity<TFieldEntityType>, IBaseEntity<TBaseEntityType>
							where TRepository : class, ICrudByFieldBaseEntityRepository<TEntity, TBaseEntityType, TFieldEntityType>
							where TBaseEntityType : struct
							where TFieldEntityType : struct
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="CrudBaseProvider{TEntity, TRepository,TBaseEntityType}" /> class.
		/// </summary>
		/// <param name="repository">The repository.</param>
		protected CrudByFieldBaseProvider(TRepository repository)
		{
			Repository = repository;
		}

		/// <summary>
		/// The repository.
		/// </summary>
		public TRepository Repository { get; }

		/// <summary>
		/// 
		/// </summary>
		public IMessageSenderFactory MessageSenderFactory { get; protected set; }

		/// <summary>
		/// 
		/// </summary>
		public IMessageBusMessageFactory MessageBusMessageFactory { get; protected set; }

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
		public abstract Task AssertEntityToDeleteIsValidAsync(IEnumerable<TFieldEntityType> identifiers);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="domain"></param>
		/// <returns></returns>
		public async virtual Task<TFieldEntityType> CreateAsync(TEntity domain)
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
		public async virtual Task DeleteAsync(TFieldEntityType id)
		{
			await AssertEntityToDeleteIsValidAsync(new List<TFieldEntityType> { id }).ConfigureAwait(true);

			await Repository.DeleteAsync(id);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public async virtual Task DeleteAsync(TEntity entity)
		{
			await AssertEntityToDeleteIsValidAsync(new List<TFieldEntityType> { entity.ExposedId }).ConfigureAwait(true);

			await Repository.DeleteAsync(entity);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async virtual Task<TEntity> GetAsync(TFieldEntityType id)
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="campaignIdentifier"></param>
		/// <param name="campaignEvent"></param>
		/// <param name="additionalProperties"></param>
		/// <returns></returns>
		protected async Task RaiseDomainEventAsync(Guid campaignIdentifier, Events campaignEvent, Dictionary<string, object> additionalProperties)
		{
			var message = MessageBusMessageFactory.CreateMessage(AppMessageType.CampaignManagement, campaignIdentifier, campaignEvent, additionalProperties);
			await MessageSenderFactory.Resolve(AppMessageType.CampaignManagement).SendAsync(message);
		}
	}
}
