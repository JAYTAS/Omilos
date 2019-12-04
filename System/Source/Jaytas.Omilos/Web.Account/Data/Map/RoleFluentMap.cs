using Jaytas.Omilos.Data.EntityFramework.BaseEntityConfigurations;
using Jaytas.Omilos.Web.Service.Account.DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jaytas.Omilos.Web.Service.Account.Data.Map
{
	/// <summary>
	/// Mapping entity for role table with the database model.
	/// </summary>
	public class RoleFluentMap : BaseIntEntityConfiguration<Role>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RoleFluentMap" /> class.
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="schema"></param>
		/// <param name="isDatabaseGenerated"></param>
		public RoleFluentMap(string tableName, string schema, bool isDatabaseGenerated) 
				: base(tableName, schema, isDatabaseGenerated, new Omilos.Common.Domain.DefaultAuditableBaseFieldMapper())
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		public override void Configure(EntityTypeBuilder<Role> builder)
		{
			base.Configure(builder);

			builder.Property(col => col.Code)
				 .HasColumnName(nameof(Role.Code))
				 .HasMaxLength(50)
				 .IsRequired();

			builder.Property(col => col.Description)
				 .HasColumnName(nameof(Role.Description))
				 .HasMaxLength(100);

			builder.Property(col => col.IsActive)
				 .HasColumnName(nameof(Role.IsActive))
				 .IsRequired();
		}
	}
}
