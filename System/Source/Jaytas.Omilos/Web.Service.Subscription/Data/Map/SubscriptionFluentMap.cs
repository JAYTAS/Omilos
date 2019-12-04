using Jaytas.Omilos.Common;
using Jaytas.Omilos.Data.EntityFramework.BaseEntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Subscription.Data.Map
{
	/// <summary>
	/// 
	/// </summary>
	public class SubscriptionFluentMap : BaseLongEntityConfiguration<DomainModel.Subscription>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SubscriptionFluentMap" /> class.
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="schema"></param>
		/// <param name="isDatabaseGenerated"></param>
		public SubscriptionFluentMap(string tableName, string schema, bool isDatabaseGenerated)
				: base(tableName, schema, isDatabaseGenerated, new Omilos.Common.Domain.DefaultAuditableBaseFieldMapper())
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		public override void Configure(EntityTypeBuilder<DomainModel.Subscription> builder)
		{
			base.Configure(builder);

			builder.Property(col => col.ExposedId)
				 .HasColumnName(nameof(Constants.CustomFeildMappings.SubscriptionId))
				 .IsRequired();

			builder.Property(col => col.Name)
				 .HasColumnName(nameof(DomainModel.Subscription.Name))
				 .IsRequired();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		public override void ConfigureKey(EntityTypeBuilder<DomainModel.Subscription> builder)
		{
			base.ConfigureKey(builder);

			builder.HasMany(subscription => subscription.Groups)
				   .WithOne(group => group.Subscription)
				   .HasForeignKey(group => group.SubscriptionId)
				   .HasPrincipalKey(subscription => subscription.ExposedId);

			builder.HasMany(subscription => subscription.Contacts)
				   .WithOne(contact => contact.Subscription)
				   .HasForeignKey(contact => contact.SubscriptionId)
				   .HasPrincipalKey(subscription => subscription.ExposedId);
		}
	}
}
