using Jaytas.Omilos.Common;
using Jaytas.Omilos.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Jaytas.Omilos.Security.Identity
{
	public class IdentityProvider : IIdentityProvider
	{
		IPrincipal _principal;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="principal"></param>
		public IdentityProvider(IPrincipal principal)
		{
			_principal = principal;
		}

		/// <summary>
		/// Gets a value indicating whether this user is authenticated.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is authenticated; otherwise, <c>false</c>.
		/// </value>
		public bool IsAuthenticated => Identity.IsAuthenticated;

		/// <summary>
		/// Gets the user's ID. This is pulled from claims.
		/// </summary>
		/// <value>The identifier.</value>
		public Guid UserId => Identity.GetClaim<Guid>(Constants.Claims.Email, Guid.Empty);

		/// <summary>
		/// Gets the first name, aka given name
		/// </summary>
		/// <value>
		/// The first name.
		/// </value>
		public string FirstName => Identity.GetClaim<string>(Constants.Claims.FirstName, default(string));

		/// <summary>
		/// Gets the last name, aka family name
		/// </summary>
		/// <value>
		/// The last name.
		/// </value>
		public string LastName => Identity.GetClaim<string>(Constants.Claims.Surname, default(string));

		/// <summary>
		/// Gets the email.
		/// </summary>
		/// <value>
		/// The email.
		/// </value>
		public string Email => Identity.GetClaim<string>(Constants.Claims.Email, default(string));

		/// <summary>
		/// Determines whether call made with app credentials
		/// </summary>
		/// <returns></returns>
		public bool IsRootUser
		{
			get
			{
				var appIdClaim = Identity.FindFirst(Constants.Claims.AppId);

				// If we have this claim, then the call is coming from another microservice.
				// This is becasue the other microservice requested an access token as itself - an app -
				// and the STS issues the token to *the app* and it identifies that identity via this claim
				return appIdClaim != null && string.IsNullOrWhiteSpace(FirstName);
			}
		}

		/// <summary>
		/// Gets the user's identity.
		/// </summary>
		/// 
		private ClaimsIdentity Identity
		{
			get { return (ClaimsIdentity)_principal.Identity; }
		}
	}
}
