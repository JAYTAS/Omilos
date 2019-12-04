using Jaytas.Omilos.Common.Domain;
using System;
using System.Collections.Generic;

namespace Jaytas.Omilos.Web.Service.Account.DomainModel
{
	/// <summary>
	/// User Entity representation of database table.
	/// </summary>
	public partial class User : GuidFieldLongBaseEntity
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="User" /> class and also UerRoles.
		/// </summary>
		public User()
		{
			UserRoles = new HashSet<UserRole>();
		}
		
		/// <summary>
		/// FirstName property represenation of database column.
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// LastName property represenation of database column.
		/// </summary>
		public string LastName { get; set; }
		
		/// <summary>
		/// Email property represenation of database column.
		/// </summary>
		public string EmailId { get; set; }

		/// <summary>
		/// Cuntry code property represenation of database column.
		/// </summary>
		public string CountryCode { get; set; }

		/// <summary>
		/// Phonenumber property represenation of database column.
		/// </summary>
		public string PhoneNumber { get; set; }

		/// <summary>
		/// IsActive property represenation of database column.
		/// </summary>
		public bool IsActive { get; set; }

		/// <summary>
		/// UserRoles Entity represenation of database Table.
		/// </summary>
		public virtual ICollection<UserRole> UserRoles { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual UserLoginDetail UserLoginDetail { get; set; }
	}
}
