using ProjectManagement.Application.Services.User.Dtos;
using ProjectManagement.Domain.Repositories;

namespace ProjectManagement.Application.Services.User;

public class GetUserService(IUnitOfWork unitOfWork)
{
    public async Task<List<GetUsersResponseDto>> Handler()
    {
        var response = await unitOfWork.User.GetAllAsync();
        return response.Select(x 
            => new GetUsersResponseDto(x.Id,x.UserName, x.Email)).ToList();
    }
}