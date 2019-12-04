using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Jaytas.Omilos.Common;
using Jaytas.Omilos.Configuration.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Jaytas.Omilos.Security.TokenProvider
{
	/// <summary>
	/// 
	/// </summary>
	public class JwtBearerTokenProvider : ITokenProvider
	{
		IBaseConfiguration _baseConfiguration;
		IMapper _mapper;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="baseConfiguration"></param>
		public JwtBearerTokenProvider(IBaseConfiguration baseConfiguration, IMapper mapper)
		{
			_baseConfiguration = baseConfiguration;
			_mapper = mapper;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public SymmetricSecurityKey GetSecurityKey()
		{
			return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_baseConfiguration.JwtBearerAuthTokenProviderSettings.SingingSecret));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public string AcquireToken(Web.Service.Models.Account.User user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Issuer = Constants.BearerOptions.TokenValidationParameters.Issuer,
				Audience = Constants.BearerOptions.TokenValidationParameters.Audience,
				Subject = new ClaimsIdentity(_mapper.Map<List<Claim>>(user)),
				Expires = DateTime.UtcNow.AddMinutes(_baseConfiguration.JwtBearerAuthTokenProviderSettings.ExpiryTimeInMinutes),
				SigningCredentials = new SigningCredentials(GetSecurityKey(), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}
