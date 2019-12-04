using Jaytas.Omilos.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Messaging.ServiceBus.Topic.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface IMessageSenderFactory
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="appMessageType"></param>
		/// <returns></returns>
		IMessageSender Resolve(AppMessageType appMessageType);
	}
}