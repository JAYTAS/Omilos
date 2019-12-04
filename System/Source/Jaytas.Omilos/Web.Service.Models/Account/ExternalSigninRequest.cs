using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Jaytas.Omilos.Web.Service.Models.Account
{
	public class ExternalSigninRequest
	{
		[Required(AllowEmptyStrings = false, ErrorMessage = "")]
		public string Code { get; set; }
	}
}
