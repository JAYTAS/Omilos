using Microsoft.EntityFrameworkCore;
using Jaytas.Omilos.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jaytas.Omilos.Data.EntityFramework.BaseEntityConfigurations
{
	/// <summary>
	/// A data model configuration.
	/// </summary>
	/// <typeparam name="TEntity">The type of the entity.</typeparam>
	/// <seealso cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{TEntity}"/>
	public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
					where TEntity : class
	{
		protected string TableName, Schema;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="schema"></param>
		public BaseEntityConfiguration(string tableName, string schema)
		{
			Schema = string.IsNullOrWhiteSpace(schema) ? Constants.Schemas.Dbo.Name : schema;
			TableName = tableName;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		public virtual void Configure(EntityTypeBuilder<TEntity> builder)
		{
			builder.ToTable(TableName, Schema);
		}

	}
}
