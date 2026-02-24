using EcomInfrastructure.DataContext;
using EcomInfrastructure.Repository.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcomInfrastructure.Repository.Implementation
{
    public class LoginUser: ILoginUser
    {
        private readonly IHttpContextAccessor _http;
        private ClaimsPrincipal? _user;
        private readonly EcomDbContext _context;

        public LoginUser(IHttpContextAccessor http, EcomDbContext context)
        {
            _http = http;
            _user = _http.HttpContext?.User;
            _context = context;
        }
        public string? Name => _user?.Identity?.Name;
        private string _userId = string.Empty;
        
        public string GetUserId() =>
           IsAuthenticated()
           ? _user?.FindFirst(ClaimTypes.NameIdentifier)?.Value
             ?? _user?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
             ?? Guid.Empty.ToString()
           : _userId;

        public bool IsAuthenticated() =>
           _user?.Identity?.IsAuthenticated is true;
    }
}

