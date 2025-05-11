using System.Net;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Services.User;
using ProjectManagement.Domain.Helpers;

namespace ProjectManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private ApiResponse apiResponse;
    private GetUserService getUserService;
    public UserController(GetUserService getUserService)
    {
        this.getUserService = getUserService;
        apiResponse = new ApiResponse();
    }
    
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetUsers()
    {
        var response = await getUserService.Handler();
        apiResponse.IsSuccess = true;
        apiResponse.Errors = null;
        apiResponse.StatusCode = HttpStatusCode.OK;
        apiResponse.Result = response;
        return Ok(apiResponse);
    }
}