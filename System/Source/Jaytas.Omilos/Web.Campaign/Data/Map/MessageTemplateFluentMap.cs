using Jaytas.Omilos.Common;
using Jaytas.Omilos.Data.EntityFramework.BaseEntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jaytas.Omilos.Web.Service.Campaign.Data.Map
{
	/// <summary>
	/// 
	/// </summary>
	public class MessageTemplateFluentMap : BaseLongEntityConfiguration<DomainModel.MessageTemplate>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MessageTemplateFluentMap" /> class.
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="schema"></param>
		/// <param name="isDatabaseGenerated"></param>
		public MessageTemplateFluentMap(string tableName, string schema, bool isDatabaseGenerated)
				: base(tableName, schema, isDatabaseGenerated, new Omilos.Common.Domain.DefaultAuditableBaseFieldMapper())
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		public override void Configure(EntityTypeBuilder<DomainModel.MessageTemplate> builder)
		{
			base.Configure(builder);

			builder.Property(col => col.ExposedId)
				 .HasColumnName(Constants.CustomFeildMappings.MessageId)
				 .IsRequired();

			builder.Property(col => col.CampaignId)
				 .HasColumnName(nameof(DomainModel.MessageTemplate.CampaignId))
				 .IsRequired();

			builder.Property(col => col.NotificationChannel)
				 .HasColumnName(nameof(DomainModel.MessageTemplate.NotificationChannel))
				 .IsRequired();

			builder.Property(col => col.WelcomeMessage)
				 .HasColumnName(nameof(DomainModel.MessageTemplate.WelcomeMessage))
				 .HasMaxLength(250);

			builder.Property(col => col.RemainderMessage)
				 .HasColumnName(nameof(DomainModel.MessageTemplate.RemainderMessage))
				 .HasMaxLength(250);

			builder.Property(col => col.OverDueMessage)
				 .HasColumnName(nameof(DomainModel.MessageTemplate.OverDueMessage))
				 .HasMaxLength(250);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		public override void ConfigureKey(EntityTypeBuilder<DomainModel.MessageTemplate> builder)
		{
			base.ConfigureKey(builder);

		}
	}
}
