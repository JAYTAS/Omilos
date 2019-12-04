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
	public class ContactFluentMap : BaseLongEntityConfiguration<DomainModel.Contact>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ContactFluentMap" /> class.
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="schema"></param>
		/// <param name="isDatabaseGenerated"></param>
		public ContactFluentMap(string tableName, string schema, bool isDatabaseGenerated)
				: base(tableName, schema, isDatabaseGenerated, new Omilos.Common.Domain.DefaultAuditableBaseFieldMapper())
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		public override void Configure(EntityTypeBuilder<DomainModel.Contact> builder)
		{
			base.Configure(builder);

			builder.Property(col => col.ExposedId)
				 .HasColumnName(nameof(Constants.CustomFeildMappings.ContactId))
				 .IsRequired();

			builder.Property(col => col.SubscriptionId)
				 .HasColumnName(nameof(DomainModel.Contact.SubscriptionId))
				 .IsRequired();

			builder.Property(col => col.FirstName)
				 .HasColumnName(nameof(DomainModel.Contact.FirstName))
				 .IsRequired();

			builder.Property(col => col.LastName)
				 .HasColumnName(nameof(DomainModel.Contact.LastName))
				 .IsRequired();

			builder.Property(col => col.Email)
				 .HasColumnName(nameof(DomainModel.Contact.Email));

			builder.Property(col => col.PhoneNumber)
				 .HasColumnName(nameof(DomainModel.Contact.PhoneNumber));

			builder.Property(col => col.TimeZone)
				 .HasColumnName(nameof(DomainModel.Contact.TimeZone))
				 .IsRequired();

			builder.Property(col => col.CustomColumn1)
				 .HasColumnName(nameof(DomainModel.Contact.CustomColumn1));

			builder.Property(col => col.CustomColumn2)
				 .HasColumnName(nameof(DomainModel.Contact.CustomColumn2));

			builder.Property(col => col.CustomColumn3)
				 .HasColumnName(nameof(DomainModel.Contact.CustomColumn3));

			builder.Property(col => col.CustomColumn4)
				 .HasColumnName(nameof(DomainModel.Contact.CustomColumn4));

			builder.Property(col => col.CustomColumn5)
				 .HasColumnName(nameof(DomainModel.Contact.CustomColumn5));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		public override void ConfigureKey(EntityTypeBuilder<DomainModel.Contact> builder)
		{
			base.ConfigureKey(builder);
		}
	}
}
