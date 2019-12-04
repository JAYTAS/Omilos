using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Common.DelegationHandlers
{
	/// <summary>
	/// 
	/// </summary>
	public class HttpBootstrapHandler : DelegatingHandler
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		async protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var req = request;

			var start = DateTime.Now;

			var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

			var end = DateTime.Now;

			return response;
		}
	}
}
