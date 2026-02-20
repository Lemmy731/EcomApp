using Commons.DTO.Auth;
using Commons.GenericResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApplication.Service.Interface
{
    public interface IAuthService
    {
        Task<ApiResponse<string>> Login(LoginDto loginDto);
    }
}
