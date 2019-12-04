using Jayatas.Omilos.Functions.Common.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Jayatas.Omilos.Functions.Common.ServiceClients.Bitly
{
	public class RestClient
	{
		public static async Task<T> GetAsync<T>(string baseAddress, string relativeUri, string accessToken)
		{
			using (HttpClient httpClient = new HttpClient())
			{
				httpClient.BaseAddress = new Uri(baseAddress);
				httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(Constants.Common.BearerScheme, accessToken);
				var response = await httpClient.GetAsync(relativeUri);
				if (!response.IsSuccessStatusCode)
				{
					return default(T);
				}

				var content = await response.Content.ReadAsStringAsync();

				return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
			}
		}

		public static async Task<T> PostAsync<T>(string baseAddress, string relativeUri, string accessToken, string body)
		{
			using (HttpClient httpClient = new HttpClient())
			{
				httpClient.BaseAddress = new Uri(baseAddress);
				
				//
				// Create HTTP transport objects
				//
				var httpRequest = new HttpRequestMessage
				{
					Method = HttpMethod.Post,
					RequestUri = new Uri(relativeUri, UriKind.RelativeOrAbsolute)
				};

				//
				// Set Headers
				//
				httpRequest.Headers.Authorization = new AuthenticationHeaderValue(Constants.Common.BearerScheme, accessToken);

				if (body != null)
				{
					httpRequest.Content = new StringContent(body);
					httpRequest.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
				}

				var response = await httpClient.SendAsync(httpRequest).ConfigureAwait(true);

				if (!response.IsSuccessStatusCode)
				{
					return default(T);
				}

				var content = await response.Content.ReadAsStringAsync();

				return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
			}
		}
	}
}
