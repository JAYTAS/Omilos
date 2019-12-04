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
	public class CampaignInstanceExceptionProvider : CrudByFieldBaseProvider<DomainModel.CampaignInstanceException, ICampaignInstanceExceptionRepository, long, Guid>, ICampaignInstanceExceptionProvider
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="campaignInstanceExceptionRepository"></param>
		public CampaignInstanceExceptionProvider(ICampaignInstanceExceptionRepository campaignInstanceExceptionRepository) : base(campaignInstanceExceptionRepository)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="domains"></param>
		/// <returns></returns>
		public async override Task AssertEntityToCreateIsValidAsync(IEnumerable<DomainModel.CampaignInstanceException> domains)
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
		public async override Task AssertEntityToUpdateIsValidAsync(IEnumerable<DomainModel.CampaignInstanceException> domains)
		{
			//throw new NotImplementedException();
			await Task.CompletedTask;
		}
	}
}
