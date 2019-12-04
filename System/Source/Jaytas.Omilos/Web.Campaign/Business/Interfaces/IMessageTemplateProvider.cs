using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Campaign.Business.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface IMessageTemplateProvider : Providers.ICrudByFieldBaseProvider<DomainModel.MessageTemplate, long, Guid>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="campaignId"></param>
		/// <returns></returns>
		Task<IEnumerable<DomainModel.MessageTemplate>> GetCampaignMessages(Guid campaignId);
	}
}
