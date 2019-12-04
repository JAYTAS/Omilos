using Jayatas.Omilos.Functions.Common.Common;
using Newtonsoft.Json;

namespace Jayatas.Omilos.Functions.Common.ServiceClients.Bitly
{
	public class ShortenRequestModel
	{
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty(Constants.RequestParameterAlias.LongUrl)]
		public string LongUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty(Constants.RequestParameterAlias.GroupId)]
		public string GroupId { get; set; }
	}
}
