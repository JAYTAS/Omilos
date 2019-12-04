using Jaytas.Omilos.Common;
using Jaytas.Omilos.Data.EntityFramework.BaseEntityConfigurations;
using Jaytas.Omilos.Web.Service.Account.DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jaytas.Omilos.Web.Service.Account.Data.Map
{
	/// <summary>
	/// 
	/// </summary>
	public class UserFluentMap : BaseLongEntityConfiguration<User>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RoleFluentMap" /> class.
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="schema"></param>
		/// <param name="isDatabaseGenerated"></param>
		public UserFluentMap(string tableName, string schema, bool isDatabaseGenerated)
				: base(tableName, schema, isDatabaseGenerated, new Omilos.Common.Domain.DefaultAuditableBaseFieldMapper())
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		public override void Configure(EntityTypeBuilder<User> builder)
		{
			base.Configure(builder);

			builder.Property(col => col.ExposedId)
				 .HasColumnName(nameof(Constants.CustomFeildMappings.GraphId))
				 .IsRequired();

			builder.Property(col => col.FirstName)
				 .HasColumnName(nameof(User.FirstName))
				 .HasMaxLength(100);

			builder.Property(col => col.LastName)
				 .HasColumnName(nameof(User.LastName))
				 .HasMaxLength(100);

			builder.Property(col => col.EmailId)
				 .HasColumnName(nameof(User.EmailId));

			builder.Property(col => col.CountryCode)
				 .HasColumnName(nameof(User.CountryCode));

			builder.Property(col => col.PhoneNumber)
				 .HasColumnName(nameof(User.PhoneNumber));

			builder.Property(col => col.IsActive)
				 .HasColumnName(nameof(User.IsActive));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		public override void ConfigureKey(EntityTypeBuilder<User> builder)
		{
			base.ConfigureKey(builder);

			builder.HasOne(user => user.UserLoginDetail)
				   .WithOne(loginDetail => loginDetail.User)
				   .HasForeignKey<UserLoginDetail>(loginDetail => loginDetail.Id);

			builder.HasMany(user => user.UserRoles)
				   .WithOne(userRole => userRole.User)
				   .HasForeignKey(user => user.UserId)
				   .HasPrincipalKey(user => user.ExposedId);
		}
	}
}
