using Jaytas.Omilos.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Messaging.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface ISender
	{
		/// <summary>
		/// Sends message asynchronously
		/// </summary>
		/// <param name="message">Message to be sent</param>
		/// <returns>returns the send message task</returns>
		Task SendAsync(IMessage message);
	}
}