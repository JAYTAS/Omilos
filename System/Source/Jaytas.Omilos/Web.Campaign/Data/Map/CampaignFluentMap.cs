using Jaytas.Omilos.Common;
using Jaytas.Omilos.Data.EntityFramework.BaseEntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jaytas.Omilos.Web.Service.Campaign.Data.Map
{
	/// <summary>
	/// 
	/// </summary>
	public class CampaignFluentMap : BaseLongEntityConfiguration<DomainModel.Campaign>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CampaignFluentMap" /> class.
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="schema"></param>
		/// <param name="isDatabaseGenerated"></param>
		public CampaignFluentMap(string tableName, string schema, bool isDatabaseGenerated)
				: base(tableName, schema, isDatabaseGenerated, new Omilos.Common.Domain.DefaultAuditableBaseFieldMapper())
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		public override void Configure(EntityTypeBuilder<DomainModel.Campaign> builder)
		{
			base.Configure(builder);

			builder.Property(col => col.ExposedId)
				 .HasColumnName(nameof(Constants.CustomFeildMappings.CampaignId))
				 .IsRequired();

			builder.Property(col => col.SubscriptionId)
				 .HasColumnName(nameof(DomainModel.Campaign.SubscriptionId))
				 .IsRequired();

			builder.Property(col => col.GroupId)
				 .HasColumnName(nameof(DomainModel.Campaign.GroupId));

			builder.Property(col => col.Name)
				 .HasColumnName(nameof(DomainModel.Campaign.Name))
				 .HasMaxLength(150)
				 .IsRequired();

			builder.Property(col => col.NotificationChannels)
				 .HasColumnName(nameof(DomainModel.Campaign.NotificationChannels))
				 .IsRequired();

			builder.Property(col => col.IsWelcomeMessageRequired)
				 .HasColumnName(nameof(DomainModel.Campaign.IsWelcomeMessageRequired))
				 .IsRequired();

			builder.Property(col => col.IsRemainderMessageRequired)
				 .HasColumnName(nameof(DomainModel.Campaign.IsRemainderMessageRequired))
				 .IsRequired();

			builder.Property(col => col.IsOverDueMessageRequired)
				 .HasColumnName(nameof(DomainModel.Campaign.IsOverDueMessageRequired))
				 .IsRequired();

			builder.Property(col => col.Status)
				 .HasColumnName(nameof(DomainModel.Campaign.Status))
				 .IsRequired();

			builder.Property(col => col.CampaignManagerEmailId)
				 .HasColumnName(nameof(DomainModel.Campaign.CampaignManagerEmailId));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		public override void ConfigureKey(EntityTypeBuilder<DomainModel.Campaign> builder)
		{
			base.ConfigureKey(builder);

			builder.HasMany(campaign => campaign.CampaignInstances)
				   .WithOne(campaignInstance => campaignInstance.Campaign)
				   .HasForeignKey(campaignInstance => campaignInstance.CampaignId)
				   .HasPrincipalKey(campaign => campaign.ExposedId);

			builder.HasMany(campaign => campaign.MessageTemplates)
				   .WithOne(messageTemplate => messageTemplate.Campaign)
				   .HasForeignKey(messageTemplate => messageTemplate.CampaignId)
				   .HasPrincipalKey(campaign => campaign.ExposedId);

			builder.HasOne(campaign => campaign.Schedule)
				   .WithOne(schedule => schedule.Campaign)
				   .HasForeignKey<DomainModel.Schedule>(schedule => schedule.CampaignId)
				   .HasPrincipalKey<DomainModel.Campaign>(campaign => campaign.ExposedId);
		}
	}
}
