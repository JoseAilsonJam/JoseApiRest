using JoseApiRest.Application.Queries;
using JoseApiRest.Domain.Entitys;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace JoseApiRest.Tests.Controllers;

public class TaskControllerTests
{
    private readonly Mock<IMediator> _mock;
    private readonly TaskController _controller;

    public TaskControllerTests()
    {
        _mock = new Mock<IMediator>();
        _controller = new TaskController(_mock.Object);
    }

    [Fact]
    public async Task GetTasks_ReturnOkResult_WithTaskList()
    {
        // Arrange
        var tasks = new List<TaskItem>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Test Task 1",
                Description = "Test Description 1",
                CompletionDate = DateTime.Now.AddDays(7).Date,
                IsCompleted = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Test Task 2",
                Description = "Test Description 2",
                CompletionDate = DateTime.Now.AddDays(7).Date,
                IsCompleted = false
            }
        };

        _mock.Setup(s => s.Send(It.IsAny<GetAllTasksQuery>(), default))
            .ReturnsAsync(tasks);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedTasks = Assert.IsAssignableFrom<IEnumerable<TaskItem>>(okResult.Value);
        Assert.Equal(2, returnedTasks.Count());
    }
}
