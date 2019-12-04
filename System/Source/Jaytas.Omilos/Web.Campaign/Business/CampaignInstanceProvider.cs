using Jaytas.Omilos.Web.Providers;
using Jaytas.Omilos.Web.Service.Campaign.Business.Interfaces;
using Jaytas.Omilos.Web.Service.Campaign.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Campaign.Business
{
	/// <summary>
	/// 
	/// </summary>
	public class CampaignInstanceProvider : CrudByFieldBaseProvider<DomainModel.CampaignInstance, ICampaignInstanceRepository, long, Guid>, ICampaignInstanceProvider
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="campaignInstanceRepository"></param>
		public CampaignInstanceProvider(ICampaignInstanceRepository campaignInstanceRepository) : base(campaignInstanceRepository)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="domains"></param>
		/// <returns></returns>
		public async override Task AssertEntityToCreateIsValidAsync(IEnumerable<DomainModel.CampaignInstance> domains)
		{
			//throw new NotImplementedException();
			await Task.CompletedTask;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="identifiers"></param>
		/// <returns></returns>
		public async override Task AssertEntityToDeleteIsValidAsync(IEnumerable<Guid> identifiers)
		{
			//throw new NotImplementedException();
			await Task.CompletedTask;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="domains"></param>
		/// <returns></returns>
		public async override Task AssertEntityToUpdateIsValidAsync(IEnumerable<DomainModel.CampaignInstance> domains)
		{
			//throw new NotImplementedException();
			await Task.CompletedTask;
		}
	}
}
