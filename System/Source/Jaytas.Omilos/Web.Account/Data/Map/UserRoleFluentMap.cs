using Jaytas.Omilos.Data.EntityFramework.BaseEntityConfigurations;
using Jaytas.Omilos.Web.Service.Account.DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jaytas.Omilos.Web.Service.Account.Data.Map
{
	/// <summary>
	/// 
	/// </summary>
	public class UserRoleFluentMap : BaseLongEntityConfiguration<UserRole>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RoleFluentMap" /> class.
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="schema"></param>
		/// <param name="isDatabaseGenerated"></param>
		public UserRoleFluentMap(string tableName, string schema, bool isDatabaseGenerated)
				: base(tableName, schema, isDatabaseGenerated, new Omilos.Common.Domain.DefaultAuditableBaseFieldMapper())
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		public override void Configure(EntityTypeBuilder<UserRole> builder)
		{
			base.Configure(builder);

			builder.Property(col => col.Scope)
				 .HasColumnName(nameof(UserRole.Scope))
				 .HasMaxLength(50)
				 .IsRequired();

			builder.Property(col => col.UserId)
				 .HasColumnName(nameof(UserRole.UserId))
				 .IsRequired();

			builder.Property(col => col.RoleId)
				 .HasColumnName(nameof(UserRole.RoleId))
				 .IsRequired();

			builder.Property(col => col.StartDate)
				 .HasColumnName(nameof(UserRole.StartDate));

			builder.Property(col => col.EndDate)
				 .HasColumnName(nameof(UserRole.EndDate));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		public override void ConfigureKey(EntityTypeBuilder<UserRole> builder)
		{
			base.ConfigureKey(builder);

			builder.HasOne<Role>(role => role.Role)
				   .WithMany(userRole => userRole.UserRoles)
				   .HasForeignKey(userRole => userRole.RoleId);
		}
	}
}
