using Jaytas.Omilos.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Account.DomainModel
{
	/// <summary>
	/// Role Entity representation of database table.
	/// </summary>
	public class Role : IntEntity
	{
		/// <summary>
		/// RoleCode property represenation of database column.
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// Description property represenation of database column.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// IsActive property represenation of database column.
		/// </summary>
		public bool IsActive { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public virtual ICollection<UserRole> UserRoles {get; set;}
	}
}
