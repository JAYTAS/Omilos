using Jayatas.Omilos.Functions.Common.Common;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jayatas.Omilos.Functions.Common.ServiceClients.Bitly
{
	public interface IBitlyClient
	{
		
		[Post("/v4/shorten")]//Constants.Secrets.IdentityProviderSettings.Google.UserUri)]
		Task<ShortenResponseModel> Shorten([Header(Constants.Common.Authorization)] string accesstoken, [Body(BodySerializationMethod.Json)] ShortenRequestModel shortenRequestModel);
	}
}
