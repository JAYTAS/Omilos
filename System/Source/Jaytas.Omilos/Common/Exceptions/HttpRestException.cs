using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Jaytas.Omilos.Common.Exceptions
{
	public class HttpRestException : Exception
	{
		public HttpRestException(HttpStatusCode statusCode, string message)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public HttpStatusCode StatusCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string ResponseContent { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public object Body { get; set; }
	}
}
