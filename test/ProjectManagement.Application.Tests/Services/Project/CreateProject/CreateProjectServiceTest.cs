using System.Threading.Tasks;
using Moq;
using ProjectManagement.Application.Services.Project.CreateProject;
using ProjectManagement.Application.Services.Project.CreateProject.Dtos;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Repositories;
using Xunit;

public class CreateProjectServiceTest
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly CreateProjectService _createProjectService;

    public CreateProjectServiceTest()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _projectRepositoryMock = new Mock<IProjectRepository>();

        _unitOfWorkMock.Setup(x => x.Project).Returns(_projectRepositoryMock.Object);

        _createProjectService = new CreateProjectService(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handler_ShouldCallAddAsync_WithCorrectProject()
    {
        // Arrange
        var request = new CreateProjectRequestDto("Test Project","Test Description");

        // Act
        await _createProjectService.Handler(request);

        // Assert
        _projectRepositoryMock.Verify(x => x.AddAsync(It.Is<Project>(p =>
            p.Name == request.Name && p.Description == request.Description)), Times.Once);
    }

    [Fact]
    public async Task Handler_ShouldCallSaveAsync()
    {
        // Arrange
        var request = new CreateProjectRequestDto("Test Project", "Test Description");

        // Act
        await _createProjectService.Handler(request);

        // Assert
        _unitOfWorkMock.Verify(x => x.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task Handler_ShouldNotThrowException_WhenValidRequest()
    {
        // Arrange
        var request = new CreateProjectRequestDto("Test Project", "Test Description");

        // Act & Assert
        var exception = await Record.ExceptionAsync(() => _createProjectService.Handler(request));
        Assert.Null(exception);
    }
}
