using Commons.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApplication.Service.JWT.Interface
{
    public interface IJwtTokenService
    {
        string GenerateAccessTokenAsync(JwtClaimsModel claims);
    }
}
