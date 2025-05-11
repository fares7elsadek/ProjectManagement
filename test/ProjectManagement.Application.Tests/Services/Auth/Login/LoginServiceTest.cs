using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;
using ProjectManagement.Application.Services.Auth.Jwt;
using ProjectManagement.Application.Services.Auth.Login;
using ProjectManagement.Application.Services.Auth.Login.Dtos;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Helpers;
using Xunit;

public class LoginServiceTest
{
    private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
    private readonly Mock<IJwtService> _jwtServiceMock;
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;

    private readonly LoginService _loginService;

    public LoginServiceTest()
    {
        var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
        _userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

        var contextAccessor = new Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
        var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
        _signInManagerMock = new Mock<SignInManager<ApplicationUser>>(_userManagerMock.Object, contextAccessor.Object, userPrincipalFactory.Object, null, null, null, null);

        _jwtServiceMock = new Mock<IJwtService>();

        _loginService = new LoginService(_signInManagerMock.Object, _jwtServiceMock.Object, _userManagerMock.Object);
    }

    [Fact]
    public async Task Handler_ShouldThrowException_WhenUserNotFound()
    {
        // Arrange
        var request = new LoginRequestDto("nonexistent@example.com","pass");
        _userManagerMock.Setup(x => x.FindByEmailAsync(request.Email)).ReturnsAsync((ApplicationUser)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _loginService.Handler(request));
    }

    [Fact]
    public async Task Handler_ShouldThrowException_WhenUserNameIsNull()
    {
        // Arrange
        var user = new ApplicationUser { Email = "test@example.com", UserName = null };
        var request = new LoginRequestDto("nonexistent@example.com","pass");
        _userManagerMock.Setup(x => x.FindByEmailAsync(request.Email)).ReturnsAsync(user);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _loginService.Handler(request));
    }

    [Fact]
    public async Task Handler_ShouldReturnAuthResponse_WhenLoginIsSuccessful()
    {
        // Arrange
        var user = new ApplicationUser { Email = "test@example.com", UserName = "testuser" };
        var request = new LoginRequestDto("nonexistent@example.com","pass");
        var roles = new List<string> { "Admin" };
        var authResponse = new AuthResponse { Token = "jwt_token" };

        _userManagerMock.Setup(x => x.FindByEmailAsync(request.Email)).ReturnsAsync(user);
        _signInManagerMock.Setup(x => x.PasswordSignInAsync(user.UserName, request.Password, false, false))
            .ReturnsAsync(SignInResult.Success);
        _userManagerMock.Setup(x => x.GetRolesAsync(user)).ReturnsAsync(roles);
        _jwtServiceMock.Setup(x => x.GetJwtToken(user, roles)).ReturnsAsync(authResponse);

        // Act
        var result = await _loginService.Handler(request);

        // Assert
        Assert.Equal("jwt_token", result.Token);
    }
}
