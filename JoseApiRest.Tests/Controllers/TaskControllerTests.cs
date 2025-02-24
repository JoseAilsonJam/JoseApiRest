using JoseApiRest.Application.Commands;
using JoseApiRest.Application.Queries;
using JoseApiRest.Domain.Entitys;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace JoseApiRest.Tests.Controllers;

public class TaskControllerTests
{
    private readonly Mock<IMediator> _mock;
    private readonly TaskController _controller;

    public TaskControllerTests()
    {
        _mock = new Mock<IMediator>();
        _controller = new TaskController(_mock.Object);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "test-user-id"),
            new Claim(ClaimTypes.Name, "testuser")
        }, "TestAuth"));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };
    }

    [Fact]
    public async Task GetTasks_ReturnOkResult_WithTaskList()
    {
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

        var result = await _controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedTasks = Assert.IsAssignableFrom<IEnumerable<TaskItem>>(okResult.Value);
        Assert.Equal(2, returnedTasks.Count());
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WhenTaskExists()
    {
        var taskId = Guid.NewGuid();
        var task = new TaskItem 
        { 
            Id = taskId, 
            Title = "Test Task", 
            Description = "Test Desc", 
            CompletionDate = DateTime.Today, 
            IsCompleted = false 
        };

        _mock.Setup(s => s.Send(It.Is<GetTaskByIdQuery>(q => q.Id == taskId), default)).ReturnsAsync(task);

        var result = await _controller.GetById(taskId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedTask = Assert.IsType<TaskItem>(okResult.Value);
        Assert.Equal(taskId, returnedTask.Id);
    }

    [Fact]
    public async Task Create_ReturnsCreatedResult_WithNewTask()
    {
        var command = new CreateTaskCommand("New Task", "Test Desc", DateTime.Now.AddDays(3).Date, false);

        var createdTask = new TaskItem 
        { 
            Id = Guid.NewGuid(), 
            Title = command.Title, 
            Description = command.Description, 
            CompletionDate = command.CompletionDate, 
            IsCompleted = false 
        };

        _mock.Setup(s => s.Send(command, default)).ReturnsAsync(createdTask);

        var result = await _controller.Create(command);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnedTask = Assert.IsType<TaskItem>(createdResult.Value);
        Assert.Equal(command.Title, returnedTask.Title);
    }

    [Fact]
    public async Task Update_ReturnsOk_WhenTaskIsUpdated()
    {
        var taskId = Guid.NewGuid();
        var command = new UpdateTaskCommand(taskId, "Updated Task", "Updated Desc", DateTime.Today, true);

        _mock.Setup(s => s.Send(command, default)).ReturnsAsync(true);

        var result = await _controller.Update(taskId, command);

        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenTaskIsDeleted()
    {
        var taskId = Guid.NewGuid();
        _mock.Setup(s => s.Send(It.Is<DeleteTaskCommand>(c => c.Id == taskId), default)).ReturnsAsync(true);

        var result = await _controller.Delete(taskId);

        Assert.IsType<NoContentResult>(result);
    }
}
