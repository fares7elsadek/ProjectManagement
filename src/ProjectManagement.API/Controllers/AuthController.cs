using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Services.Auth.Login;
using ProjectManagement.Application.Services.Auth.Login.Dtos;
using ProjectManagement.Application.Services.Auth.Register;
using ProjectManagement.Application.Services.Auth.Register.Dtos;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Helpers;

namespace ProjectManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController: ControllerBase
{
    private ApiResponse apiResponse;
    private readonly LoginService _loginService;
    private readonly RegisterService _registerService;
    public AuthController(LoginService loginService,RegisterService registerService)
    {
        _loginService = loginService;
        apiResponse = new ApiResponse();
        _registerService = registerService;
    }
    
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> RegisterUser([FromBody] RegisterRequestDto command)
    {
        var authResponse = await _registerService.Handler(command);
            
        var response = new
        {
            message = authResponse.Message,
            email = authResponse.Email,
        };

        apiResponse.IsSuccess = true;
        apiResponse.Errors = null;
        apiResponse.StatusCode = HttpStatusCode.OK;
        apiResponse.Result = response;
        return Ok(apiResponse);
    }
    
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> LoginUser([FromBody] LoginRequestDto command)
    {
        var authResponse = await _loginService.Handler(command);
        apiResponse.IsSuccess = true;
        apiResponse.Errors = null;
        apiResponse.StatusCode = HttpStatusCode.OK;
        apiResponse.Result = authResponse;
        return Ok(apiResponse);
    }
}