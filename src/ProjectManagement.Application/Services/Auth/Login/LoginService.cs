using Microsoft.AspNetCore.Identity;
using ProjectManagement.Application.Services.Auth.Jwt;
using ProjectManagement.Application.Services.Auth.Login.Dtos;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Helpers;

namespace ProjectManagement.Application.Services.Auth.Login;

public class LoginService(SignInManager<ApplicationUser> signInManager,
    IJwtService jwtService,UserManager<ApplicationUser> userManager)
{
    public async Task<AuthResponse> Handler(LoginRequestDto request)
    {
        var email = request.Email;
        var password = request.Password;
        var user = await userManager.FindByEmailAsync(email);

        if (user == null || user.UserName == null)
            throw new Exception("Invalid credentials");

        var result = await signInManager.PasswordSignInAsync(user.UserName, password, isPersistent:false ,lockoutOnFailure: false);
        var roles = await userManager.GetRolesAsync(user);
        var authResponse = await jwtService.GetJwtToken(user, roles.ToList());
        return authResponse;
    }
}