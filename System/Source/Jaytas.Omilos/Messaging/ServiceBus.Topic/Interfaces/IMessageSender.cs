using Jaytas.Omilos.Common.Enumerations;
using Jaytas.Omilos.Messaging.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Messaging.ServiceBus.Topic.Interfaces
{
	public interface IMessageSender : ISender
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="appMessageType"></param>
		/// <returns></returns>
		bool CanSend(AppMessageType appMessageType);
	}
}