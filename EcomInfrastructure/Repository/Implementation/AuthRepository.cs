using Commons.DTO.Auth;
using Commons.GenericResponse;
using EcomDomain.Entity;
using EcomInfrastructure.DataContext;
using EcomInfrastructure.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomInfrastructure.Repository.Implementation
{
    public class AuthRepository: IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        public AuthRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ApiResponse<User>> Login(LoginDto loginDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);
                if (user == null)
                {
                    return ApiResponse<User>.Failure(StatusCodes.Status404NotFound, "user not found");
                }
                var checkPassWord = await _userManager.CheckPasswordAsync(user, loginDto.Password);
                if (checkPassWord != null)
                {
                    return ApiResponse<User>.Success("user found", user); ;
                }
                return ApiResponse<User>.Failure(StatusCodes.Status500InternalServerError, "error");
            }
            catch (Exception ex)
            {
                return ApiResponse<User>.Failure(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
