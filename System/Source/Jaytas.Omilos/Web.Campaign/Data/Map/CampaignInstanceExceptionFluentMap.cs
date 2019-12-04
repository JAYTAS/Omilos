using Jaytas.Omilos.Common;
using Jaytas.Omilos.Data.EntityFramework.BaseEntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jaytas.Omilos.Web.Service.Campaign.Data.Map
{
	/// <summary>
	/// 
	/// </summary>
	public class CampaignInstanceExceptionFluentMap : BaseLongEntityConfiguration<DomainModel.CampaignInstanceException>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CampaignInstanceExceptionFluentMap" /> class.
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="schema"></param>
		/// <param name="isDatabaseGenerated"></param>
		public CampaignInstanceExceptionFluentMap(string tableName, string schema, bool isDatabaseGenerated)
				: base(tableName, schema, isDatabaseGenerated, new Omilos.Common.Domain.DefaultAuditableBaseFieldMapper())
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		public override void Configure(EntityTypeBuilder<DomainModel.CampaignInstanceException> builder)
		{
			base.Configure(builder);

			builder.Property(col => col.ExposedId)
				 .HasColumnName(Constants.CustomFeildMappings.CampaignInstanceExceptionId)
				 .IsRequired();

			builder.Property(col => col.InstanceId)
				 .HasColumnName(nameof(DomainModel.CampaignInstanceException.InstanceId))
				 .IsRequired();

			builder.Property(col => col.IsRescheduled)
				 .HasColumnName(nameof(DomainModel.CampaignInstanceException.IsRescheduled));

			builder.Property(col => col.IsCancelled)
				 .HasColumnName(nameof(DomainModel.CampaignInstanceException.IsCancelled));

			builder.Property(col => col.StartDate)
				 .HasColumnName(nameof(DomainModel.CampaignInstanceException.StartDate));

			builder.Property(col => col.EndDate)
				 .HasColumnName(nameof(DomainModel.CampaignInstanceException.EndDate));

			builder.Property(col => col.StartTime)
				 .HasColumnName(nameof(DomainModel.CampaignInstanceException.StartTime));

			builder.Property(col => col.EndTime)
				 .HasColumnName(nameof(DomainModel.CampaignInstanceException.EndTime));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="builder"></param>
		public override void ConfigureKey(EntityTypeBuilder<DomainModel.CampaignInstanceException> builder)
		{
			base.ConfigureKey(builder);

		}
	}
}
