using FluentAssertions;
using JoseApiRest.Application.Commands;
using JoseApiRest.Application.Handlers;
using JoseApiRest.Infrastructure.Services.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace JoseApiRest.Tests.Handlers;

public class TaskCommandHandlerTests
{
    [Fact]
    public async Task Handle_WithValidCommand_ShouldAddTaskToDatabase()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new DataContext(options);
        var handler = new TaskCommandHandler(context);
        var command = new CreateTaskCommand("Task Title", "Task Description", DateTime.UtcNow.AddDays(7).Date, false);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.Should().NotBeNull();
        var createdTask = await context.TaskItems.FindAsync(result.Id);
        createdTask.Should().NotBeNull();
        createdTask.Title.Should().Be("Task Title");
        createdTask.Description.Should().Be("Task Description");
    }
}
