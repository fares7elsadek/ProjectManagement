using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Helpers;

namespace ProjectManagement.Application.Services.Auth.Jwt;

public class JwtService:IJwtService
{
    private readonly JwtOptions jwt;
    private readonly UserManager<ApplicationUser> userManager;
    public JwtService(IOptions<JwtOptions> Jwt,
        UserManager<ApplicationUser> userManager)
    {
        jwt = Jwt.Value;
        this.userManager = userManager;
    }
    
    public async Task<JwtSecurityToken> CreatJwtToken(ApplicationUser user)
    {
        var userClaim = await userManager.GetClaimsAsync(user);
        var role_list = await userManager.GetRolesAsync(user);
        var roleClaims = new List<Claim>();
        foreach (var role in role_list)
            roleClaims.Add(new Claim("role", role));

        var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid",user.Id)
            }
            .Union(userClaim)
            .Union(roleClaims);

        var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
        var SigningCredentials = new SigningCredentials(symetricSecurityKey,SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer:jwt.Issure,
            audience:jwt.Audience,
            claims:claims,
            expires:DateTime.Now.AddMinutes(jwt.DurationInMinutes),
            signingCredentials: SigningCredentials
        );
        return jwtSecurityToken;
    }

    public async Task<AuthResponse> GetJwtToken(ApplicationUser user, List<string> roles)
    {
        AuthResponse authResponse = new AuthResponse();
        var jwtSecurityToken = await CreatJwtToken(user);
        authResponse.Message = "User logged in succeefully";
        authResponse.IsAuthenticated = true;
        authResponse.Email = user.Email;
        authResponse.Username = user.UserName;
        authResponse.Roles = roles;
        authResponse.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return authResponse;
    }
}