using AutoMapper;
using Jaytas.Omilos.Security.TokenProvider;
using Jaytas.Omilos.ServiceClient.User.Interfaces;
using Jaytas.Omilos.Web.Service.Account.Business.Interfaces;
using Jaytas.Omilos.Web.Service.Account.Data.Repositories.Interfaces;
using Jaytas.Omilos.Web.Service.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jaytas.Omilos.Web.Service.Account.Business
{
	/// <summary>
	/// 
	/// </summary>
	public class AccountProvider : IAccountProvider
	{
		readonly IFacebookUserServiceClient _facebookUserServiceClient;
		readonly IGoogleUserServiceClient _googleUserServiceClient;
		IUserRepository _userRepository;
		readonly IMapper _mapper;
		ITokenProvider _tokenProvider;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="facebookUserServiceClient"></param>
		/// <param name="googleUserServiceClient"></param>
		/// <param name="userRepository"></param>
		/// <param name="mapper"></param>
		/// <param name="tokenProvider"></param>
		public AccountProvider(IFacebookUserServiceClient facebookUserServiceClient, IGoogleUserServiceClient googleUserServiceClient, IMapper mapper, IUserRepository userRepository, ITokenProvider tokenProvider)
		{
			_facebookUserServiceClient = facebookUserServiceClient;
			_googleUserServiceClient = googleUserServiceClient;
			_userRepository = userRepository;
			_mapper = mapper;
			_tokenProvider = tokenProvider;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="signinRequest"></param>
		/// <returns></returns>
		public async Task<string> AcquireFacebookAccessToken(ExternalSigninRequest signinRequest)
		{
			var facebookUserData = await _facebookUserServiceClient.WhoAmIByCodeAsync(signinRequest.Code);
			var userData = (await _userRepository.GetAsync(user => user.EmailId == facebookUserData.Email)).FirstOrDefault();
			var omilosUserData = await RegisterOrUpdateUserDataAsync(userData, facebookUserData);

			var apiModel = _mapper.Map<User>(omilosUserData);

			return _tokenProvider.AcquireToken(apiModel);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="signinRequest"></param>
		/// <returns></returns>
		public async Task<string> AcquireGoogleAccessToken(ExternalSigninRequest signinRequest)
		{
			var googleUserData = await _googleUserServiceClient.WhoAmIByCodeAsync(signinRequest.Code);
			var userData = (await _userRepository.GetAsync(user => user.EmailId == googleUserData.Email)).FirstOrDefault();
			var omilosUserData = await RegisterOrUpdateUserDataAsync(userData, googleUserData);

			var apiModel = _mapper.Map<User>(omilosUserData);

			return _tokenProvider.AcquireToken(apiModel);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		/// <param name="userData"></param>
		/// <returns></returns>
		private async Task<DomainModel.User> RegisterOrUpdateUserDataAsync(DomainModel.User user, UserData userData)
		{
			if (user == null)
			{
				user = _mapper.Map<DomainModel.User>(userData);
				await _userRepository.AddAsync(user);
				return user;
			}

			var sourceLoginDetail = user.UserLoginDetail ?? new DomainModel.UserLoginDetail();
			var mappedLoginDetail  = _mapper.Map<UserData, DomainModel.UserLoginDetail>(userData);

			if (!sourceLoginDetail.Equals(mappedLoginDetail))
			{
				await _userRepository.UpdateAsync(user);
			}

			return user;
		}
	}
}
