using Commons.DTO.Auth;
using Commons.GenericResponse;
using EcomApplication.Service.Interface;
using EcomApplication.Service.JWT.Interface;
using EcomInfrastructure.Repository.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApplication.Service.Implementation
{
    public class AuthService: IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IJwtTokenService _jwtTokenService;
        public AuthService(IJwtTokenService jwtTokenService, IAuthRepository authRepository)
        {
            _jwtTokenService = jwtTokenService;
            _authRepository = authRepository;
        }
        public async Task<ApiResponse<string>> Login(LoginDto loginDto)
        {
            var user = await _authRepository.Login(loginDto);
            if (user.StatusCode == StatusCodes.Status200OK)
            {
                var jwtClaims = new JwtClaimsModel
                {
                    UserId = user.Data.Id,
                    Email = user.Data.Email,
                    FirstName = user.Data.FirstName,
                    LastName = user.Data.LastName
                };
                var accessToken = _jwtTokenService.GenerateAccessTokenAsync(jwtClaims);
                if (accessToken != null)
                {
                    return ApiResponse<string>.Success("jwt token generated", accessToken);
                }
            }
            return ApiResponse<string>.Failure(StatusCodes.Status400BadRequest, "no user found");
        }
    }
}
