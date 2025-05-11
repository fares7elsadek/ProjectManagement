using Microsoft.AspNetCore.Identity;
using ProjectManagement.Application.Services.Auth.Register.Dtos;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Helpers;

namespace ProjectManagement.Application.Services.Auth.Register;

public class RegisterService(UserManager<ApplicationUser> userManager)
{
    public async Task<AuthResponse> Handler(RegisterRequestDto request)
    {
        if (await userManager.FindByEmailAsync(request.Email) is not null)
            throw new Exception("User email already exsit");
        

        if (await userManager.FindByNameAsync(request.UserName) is not null)
            throw new Exception("Username already exsit");

        var user = new ApplicationUser
        {
            UserName = request.UserName,
            Email = request.Email,
        };
        var result = await userManager.CreateAsync(user, request.Password);
        
        if (!result.Succeeded)
        {
            List<string> errors = new List<string>();
            foreach (var error in result.Errors)
            {
                errors.Add(error.Description);
            }
            throw new Exception(string.Join(',', errors));
        }
        await userManager.AddToRoleAsync(user, UserRoles.USER);
        
        var authResponse = new AuthResponse
        {
            Email = request.Email,
            Username = request.UserName,
            IsAuthenticated = true,
            Message = "User registered successfully"
        };
        return authResponse;
    }
}