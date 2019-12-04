using AutoMapper;
using Jaytas.Omilos.Common.Web;
using Jaytas.Omilos.Web.Service.Account.Business;
using Jaytas.Omilos.Web.Controllers;
using Jaytas.Omilos.Web.Service.Models.Account;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Account.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	[Route("api/Account/Role")]
	public class RoleController : BaseApiController
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RoleController" /> class.
		/// </summary>
		private readonly IRoleProvider _roleProvider;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="roleProvider"></param>
		/// <param name="mapper"></param>
		public RoleController(IRoleProvider roleProvider, IMapper mapper) : base(mapper)
		{
			_roleProvider = roleProvider;
		}

		/// <summary>
		/// Gets all the roles from the table.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[HttpHead]
		[Route("GetRoles")]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.Unauthorized)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType(typeof(FriendlyError), (int)HttpStatusCode.NotFound)]
		[ProducesResponseType(typeof(IEnumerable<Role>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetRoles()
		{
			return await ExecuteWithExceptionHandlingAsync<IEnumerable<DomainModel.Role>, IEnumerable<Role>>(() => _roleProvider.GetRoles());
		}

	}
}
