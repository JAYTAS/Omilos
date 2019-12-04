using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jaytas.Omilos.Common.Models;
using Jaytas.Omilos.Web.Service.Models.Common;

namespace Jaytas.Omilos.Web.Service.Campaign.Business.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface ICampaignProvider : Providers.ICrudByFieldBaseProvider<DomainModel.Campaign, long, Guid>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="campaignId"></param>
		/// <returns></returns>
		Task PublishCampaign(Guid campaignId);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="campaignId"></param>
		/// <param name="groupId"></param>
		/// <returns></returns>
		Task AssignGroup(Guid campaignId, Guid groupId);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pageDetails"></param>
		/// <param name="subscriptionId"></param>
		/// <returns></returns>
		Task<PagedResultSet<Models.Campaign.CampaignSummary>> GetMyCampaigns(Guid? subscriptionId, PageDetails pageDetails);
	}
}
