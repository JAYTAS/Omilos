using Jaytas.Omilos.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Messaging.Interfaces
{
	/// <summary>
	/// Base Interface for Messages
	/// </summary>
	public interface IMessage
	{
		/// <summary>
		/// Gets or sets the Message Id
		/// </summary>
		string Id { get; }

		/// <summary>
		/// Gets or Sets the message type
		/// </summary>
		MessageType Type { get; }
	}
}