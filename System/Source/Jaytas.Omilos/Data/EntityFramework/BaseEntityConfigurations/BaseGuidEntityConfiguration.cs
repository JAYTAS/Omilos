using Jaytas.Omilos.Common;
using Jaytas.Omilos.Common.Domain;
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
	public abstract class BaseGuidEntityConfiguration<TEntity> : BaseEntityConfiguration<TEntity> where TEntity : GuidBaseEntity
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BaseGuidEntityConfiguration{TEntity}"/> class.
		/// </summary>
		/// <param name="tableName">Name of the table.</param>
		/// <param name="schema">The schema.</param>
		protected BaseGuidEntityConfiguration(string tableName, string schema) : base(tableName, schema)
		{
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
				   .HasColumnName(nameof(GuidBaseEntity.Id))
				   .IsRequired()
				   .ValueGeneratedNever();

			ConfigureKey(builder);
		}


		/// <summary>
		/// Configures the primary key for this table as just the Primitive Id.
		/// </summary>
		protected virtual void ConfigureKey(EntityTypeBuilder<TEntity> builder)
		{
			builder.HasKey(x => x.Id);
		}
	}
}
