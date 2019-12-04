using AutoMapper;
using Jaytas.Omilos.Common;
using Jaytas.Omilos.Common.Enumerations;
using Jaytas.Omilos.Common.Exceptions;
using Jaytas.Omilos.Common.Web;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Jaytas.Omilos.Web.Mapping.Profiles
{
	/// <summary>
	/// Registers the globally shared AutoMapper rules necessary for proper microservice functionality, i.e. to call the Authorization microservice.
	/// </summary>
	/// <seealso cref="AutoMapper.Profile" />
	public class GlobalMappingProfile : Profile
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GlobalMappingProfile"/> class.
		/// </summary>
		public GlobalMappingProfile()
		{
			// create the singleton instance to be used for all maps
			FreindlyErrorTypeConverter _errorTypeConverter = new FreindlyErrorTypeConverter();
			CreateMap<ApiErrors, FriendlyError>().ConvertUsing(_errorTypeConverter);
			CreateMap<BusinessValidationException, FriendlyError>().ConvertUsing(_errorTypeConverter);
			CreateMap<Service.Models.Account.User, List<Claim>>().ConvertUsing(Map);
		}

		/// <summary>
		/// Gets the name of the profile.
		/// </summary>
		/// <value>
		/// The name of the profile.
		/// </value>
		public override string ProfileName => typeof(GlobalMappingProfile).FullName;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		private List<Claim> Map(Service.Models.Account.User user)
		{
			return new List<Claim>()
			{
				new Claim(Constants.Claims.Email, user.Email),
				new Claim(Constants.Claims.Upn, user.UserId.ToString()),
				new Claim(Constants.Claims.FirstName, user.FirstName ?? string.Empty),
				new Claim(Constants.Claims.Surname, user.LastName ?? string.Empty)
			};
		}
	}
}
