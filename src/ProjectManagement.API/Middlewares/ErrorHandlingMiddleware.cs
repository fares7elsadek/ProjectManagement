using System.Net;
using ProjectManagement.Domain.Exceptions;
using ProjectManagement.Domain.Helpers;

namespace ProjectManagement.API.Middlewares;

public class ErrorHandlingMiddleware:IMiddleware
{
    private ApiResponse apiResponse;
    public ErrorHandlingMiddleware()
    {
        apiResponse = new ApiResponse();
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException ex)
        {
            apiResponse.StatusCode = HttpStatusCode.NotFound;
            apiResponse.IsSuccess = false;
            apiResponse.Errors.Add(ex.Message);
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync(apiResponse);
        }
        catch (Exception ex)
        {
            //log here
            apiResponse.StatusCode = HttpStatusCode.InternalServerError;
            apiResponse.IsSuccess = false;
            apiResponse.Errors.Add(ex.Message);
            context.Response.StatusCode = 500;
            Console.WriteLine(ex.Message);
            await context.Response.WriteAsJsonAsync(apiResponse);
        }
    }
}