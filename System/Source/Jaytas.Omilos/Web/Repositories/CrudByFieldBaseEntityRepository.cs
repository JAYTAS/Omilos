using Jaytas.Omilos.Common.Domain.Interfaces;
using Jaytas.Omilos.Common.Extensions;
using Jaytas.Omilos.Data.EntityFramework.BaseImplementations;
using Jaytas.Omilos.Data.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Repositories
{
	public abstract class CrudByFieldBaseEntityRepository<TContext, TEntity, TBaseEntityType, TFieldEntityType> : BaseEntityRepository<TContext>, ICrudByFieldBaseEntityRepository<TEntity, TBaseEntityType, TFieldEntityType>
							where TEntity : class, IFieldEntity<TFieldEntityType>, IBaseEntity<TBaseEntityType>
							where TContext : IDbContext
							where TBaseEntityType : struct
							where TFieldEntityType : struct
	{
		protected DbSet<TEntity> DbSet;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dbContext"></param>
		/// <param name="dbSet"></param>
		public CrudByFieldBaseEntityRepository(TContext dbContext, DbSet<TEntity> dbSet) : base(dbContext)
		{
			DbSet = dbSet;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public virtual async Task<TFieldEntityType> AddAsync(TEntity entity)
		{
			entity.GenerateExposedField();

			await DbSet.AddAsync(entity);
			await DbContext.SaveChangesAsync();
			return entity.ExposedId;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entities"></param>
		/// <returns></returns>
		public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
		{
			entities.EnsureExposedIdField<TEntity, TFieldEntityType>();
			await DbSet.AddRangeAsync(entities);
			await DbContext.SaveChangesAsync();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="expression"></param>
		public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> expression)
		{
			var entities = await GetAsync(expression);
			DbSet.RemoveRange(entities);
			await DbContext.SaveChangesAsync();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public virtual async Task DeleteAsync(TEntity entity)
		{
			DbSet.Remove(entity);
			await DbContext.SaveChangesAsync();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public virtual async Task DeleteAsync(TFieldEntityType id)
		{
			var entity = await GetAsync(id);
			await DeleteAsync(entity);
			await DbContext.SaveChangesAsync();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public abstract Task<TEntity> GetAsync(TFieldEntityType id);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
		{
			return await DbSet.Where(expression).ToListAsync();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public virtual async Task UpdateAsync(TEntity entity)
		{
			DbSet.Update(entity);
			await DbContext.SaveChangesAsync();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entities"></param>
		/// <returns></returns>
		public virtual async Task UpdateAsync(IEnumerable<TEntity> entities)
		{
			DbSet.UpdateRange(entities);
			await DbContext.SaveChangesAsync();
		}
	}
}
