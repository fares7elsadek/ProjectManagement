namespace ProjectManagement.Application.Services.Auth.Register.Dtos;

public record RegisterRequestDto(string UserName
    ,string Email,string Password);
