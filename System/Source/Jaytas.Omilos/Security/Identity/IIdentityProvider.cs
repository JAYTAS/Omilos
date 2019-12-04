using System;
using System.Collections.Generic;
using System.Text;

namespace Jaytas.Omilos.Security.Identity
{
	public interface IIdentityProvider
	{
		/// <summary>
		/// Gets a value indicating whether this user is authenticated.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is authenticated; otherwise, <c>false</c>.
		/// </value>
		bool IsAuthenticated { get; }

		/// <summary>
		/// Gets the user's ID. This is pulled from claims.
		/// </summary>
		/// <value>The identifier.</value>
		Guid UserId { get; }

		/// <summary>
		/// Gets the first name, aka given name
		/// </summary>
		/// <value>
		/// The first name.
		/// </value>
		string FirstName { get; }

		/// <summary>
		/// Gets the last name, aka family name
		/// </summary>
		/// <value>
		/// The last name.
		/// </value>
		string LastName { get; }

		/// <summary>
		/// Gets the email.
		/// </summary>
		/// <value>
		/// The email.
		/// </value>
		string Email { get; }
		
		/// <summary>
		/// Determines whether call made with app credentials
		/// </summary>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		bool IsRootUser { get; }
	}
}
