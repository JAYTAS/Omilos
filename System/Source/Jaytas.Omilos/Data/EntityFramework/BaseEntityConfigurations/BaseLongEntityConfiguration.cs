using Jaytas.Omilos.Common;
using Jaytas.Omilos.Common.Domain;
using Jaytas.Omilos.Common.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Data.EntityFramework.BaseEntityConfigurations
{
	/// <summary>
	/// A data model configuration.
	/// </summary>
	/// <typeparam name="TEntity">The type of the entity.</typeparam>
	/// <seealso cref="BaseEntityConfigurations.BaseEntityConfiguration{TEntity}"/>
	public abstract class BaseLongEntityConfiguration<TEntity> : BaseEntityConfiguration<TEntity> where TEntity : LongBaseEntity
	{
		IBaseFieldMapper _baseFieldMapper;
		bool _isDatabaseGenerated;

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseLongEntityConfiguration{TEntity}"/> class.
		/// </summary>
		/// <param name="tableName">Name of the table.</param>
		/// <param name="schema">The schema. dbo will be used if null or empty </param>
		/// <param name="isDatabaseGenerated">Value of this generated at database</param>
		protected BaseLongEntityConfiguration(string tableName, string schema, bool isDatabaseGenerated, IBaseFieldMapper baseFieldMapper) : base(tableName, schema)
		{
			_baseFieldMapper = baseFieldMapper;
			_isDatabaseGenerated = isDatabaseGenerated;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		public override void Configure(EntityTypeBuilder<TEntity> builder)
		{
			base.Configure(builder);

			// Default shared properties
			builder.Property(x => x.Id)
				.HasColumnName(_baseFieldMapper.Id)
				.IsRequired();

			if(_isDatabaseGenerated)
			{
				builder.Property(_baseFieldMapper.Id).ValueGeneratedOnAdd();
			}
			else
			{
				builder.Property(_baseFieldMapper.Id).ValueGeneratedNever();
			}

			ConfigureKey(builder);
		}

		/// <summary>
		/// Configures the primary key for this table as just the Primitive Id.
		/// </summary>
		public virtual void ConfigureKey(EntityTypeBuilder<TEntity> builder)
		{
			builder.HasKey(x => x.Id);
		}
	}
}
