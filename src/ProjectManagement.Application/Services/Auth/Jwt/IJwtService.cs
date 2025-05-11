using System.IdentityModel.Tokens.Jwt;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Helpers;

namespace ProjectManagement.Application.Services.Auth.Jwt;

public interface IJwtService
{
    Task<JwtSecurityToken> CreatJwtToken(ApplicationUser user);
    Task<AuthResponse> GetJwtToken(ApplicationUser user,List<string> roles);
}