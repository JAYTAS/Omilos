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
	public class GroupContactAssociationFluentMap : BaseEntityConfiguration<DomainModel.GroupContactAssociation>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GroupContactAssociationFluentMap" /> class.
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="schema"></param>
		public GroupContactAssociationFluentMap(string tableName, string schema) : base(tableName, schema)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		public override void Configure(EntityTypeBuilder<DomainModel.GroupContactAssociation> builder)
		{
			base.Configure(builder);

			builder.Property(col => col.GroupId)
				 .HasColumnName(nameof(DomainModel.GroupContactAssociation.GroupId))
				 .IsRequired();

			builder.Property(col => col.ContactId)
				 .HasColumnName(nameof(DomainModel.GroupContactAssociation.ContactId))
				 .IsRequired();

			builder.Property(col => col.HasOptedOut)
				 .HasColumnName(nameof(DomainModel.GroupContactAssociation.HasOptedOut))
				 .IsRequired();

			builder.Property(col => col.HasWelcomeMessageSent)
				 .HasColumnName(nameof(DomainModel.GroupContactAssociation.HasWelcomeMessageSent))
				 .IsRequired();

			ConfigureKey(builder);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		public virtual void ConfigureKey(EntityTypeBuilder<DomainModel.GroupContactAssociation> builder)
		{
			builder.HasKey(groupContactAssociation => new { groupContactAssociation.ContactId, groupContactAssociation.GroupId });

			builder.HasOne(groupContactAssociation => groupContactAssociation.Contact)
				   .WithMany(contact => contact.GroupContactAssociations)
				   .HasForeignKey(groupContactAssociation => groupContactAssociation.ContactId)
				   .HasPrincipalKey(contact => contact.ExposedId);

			builder.HasOne(groupContactAssociation => groupContactAssociation.Group)
				   .WithMany(group => group.GroupContactAssociations)
				   .HasForeignKey(groupContactAssociation => groupContactAssociation.GroupId)
				   .HasPrincipalKey(group => group.ExposedId);
		}
	}
}