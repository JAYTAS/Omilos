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
	public class GroupFluentMap : BaseLongEntityConfiguration<DomainModel.Group>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GroupFluentMap" /> class.
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="schema"></param>
		/// <param name="isDatabaseGenerated"></param>
		public GroupFluentMap(string tableName, string schema, bool isDatabaseGenerated)
				: base(tableName, schema, isDatabaseGenerated, new Omilos.Common.Domain.DefaultAuditableBaseFieldMapper())
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		public override void Configure(EntityTypeBuilder<DomainModel.Group> builder)
		{
			base.Configure(builder);

			builder.Property(col => col.ExposedId)
				 .HasColumnName(nameof(Constants.CustomFeildMappings.GroupId))
				 .IsRequired();

			builder.Property(col => col.Name)
				 .HasColumnName(nameof(DomainModel.Group.Name))
				 .IsRequired();

			builder.Property(col => col.IsUsed)
				 .HasColumnName(nameof(DomainModel.Group.IsUsed))
				 .IsRequired();

			builder.Property(col => col.SubscriptionId)
				 .HasColumnName(nameof(DomainModel.Group.SubscriptionId))
				 .IsRequired();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		public override void ConfigureKey(EntityTypeBuilder<DomainModel.Group> builder)
		{
			base.ConfigureKey(builder);

			builder.HasMany(group => group.GroupContactAssociations)
				   .WithOne(groupContactAssociation => groupContactAssociation.Group)
				   .HasForeignKey(groupContactAssociation => groupContactAssociation.GroupId)
				   .HasPrincipalKey(group => group.ExposedId);
		}
	}
}