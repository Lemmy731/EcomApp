using Commons.DTO.Auth;
using Commons.GenericResponse;
using EcomDomain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomInfrastructure.Repository.Interface
{
    public interface IAuthRepository
    {
        Task<ApiResponse<User>> Login(LoginDto loginDto);
    }
}
