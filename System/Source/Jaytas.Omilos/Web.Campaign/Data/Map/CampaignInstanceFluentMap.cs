using Jaytas.Omilos.Common;
using Jaytas.Omilos.Data.EntityFramework.BaseEntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jaytas.Omilos.Web.Service.Campaign.Data.Map
{
	/// <summary>
	/// 
	/// </summary>
	public class CampaignInstanceFluentMap : BaseLongEntityConfiguration<DomainModel.CampaignInstance>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CampaignInstanceFluentMap" /> class.
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="schema"></param>
		/// <param name="isDatabaseGenerated"></param>
		public CampaignInstanceFluentMap(string tableName, string schema, bool isDatabaseGenerated)
				: base(tableName, schema, isDatabaseGenerated, new Omilos.Common.Domain.DefaultAuditableBaseFieldMapper())
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		public override void Configure(EntityTypeBuilder<DomainModel.CampaignInstance> builder)
		{
			base.Configure(builder);

			builder.Property(col => col.ExposedId)
				 .HasColumnName(Constants.CustomFeildMappings.InstanceId)
				 .IsRequired();

			builder.Property(col => col.StartDate)
				 .HasColumnName(nameof(DomainModel.CampaignInstance.StartDate))
				 .IsRequired();

			builder.Property(col => col.EndDate)
				 .HasColumnName(nameof(DomainModel.CampaignInstance.EndDate))
				 .IsRequired();

			builder.Property(col => col.StartTime)
				 .HasColumnName(nameof(DomainModel.CampaignInstance.StartTime))
				 .IsRequired();

			builder.Property(col => col.EndTime)
				 .HasColumnName(nameof(DomainModel.CampaignInstance.EndTime))
				 .IsRequired();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		public override void ConfigureKey(EntityTypeBuilder<DomainModel.CampaignInstance> builder)
		{
			base.ConfigureKey(builder);

			builder.HasOne(campaignInstance => campaignInstance.CampaignInstanceException)
				   .WithOne(campaignInstanceException => campaignInstanceException.CampaignInstance)
				   .HasForeignKey<DomainModel.CampaignInstanceException>(campaignInstanceException => campaignInstanceException.InstanceId)
				   .HasPrincipalKey<DomainModel.CampaignInstance>(campaignInstance => campaignInstance.ExposedId);
		}
	}
}
