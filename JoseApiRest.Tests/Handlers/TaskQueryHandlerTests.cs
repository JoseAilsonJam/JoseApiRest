using FluentAssertions;
using JoseApiRest.Application.Handlers;
using JoseApiRest.Application.Queries;
using JoseApiRest.Domain.Entitys;
using JoseApiRest.Infrastructure.Services.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace JoseApiRest.Tests.Handlers;

public class TaskQueryHandlerTests
{
    private DataContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new DataContext(options);
        context.TaskItems.AddRange(new List<TaskItem>
        {
            new (Guid.NewGuid(), "Task 1", "Description 1", DateTime.UtcNow.AddDays(1).Date, false),
            new (Guid.NewGuid(), "Task 2", "Description 2", DateTime.UtcNow.AddDays(2).Date, false)
        });
        context.SaveChanges();

        return context;
    }

    [Fact]
    public async Task Handle_GetAllTasksQuery_ShouldReturnAllTasks()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var handler = new TaskQueryHandler(context);
        var query = new GetAllTasksQuery();

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task Handle_GetTaskByIdQuery_ShouldReturnCorrectTask()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var existingTask = await context.TaskItems.FirstAsync();
        var handler = new TaskQueryHandler(context);
        var query = new GetTaskByIdQuery(existingTask.Id);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(existingTask.Id);
        result.Title.Should().Be(existingTask.Title);
    }

    [Fact]
    public async Task Handle_GetTaskByIdQuery_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var handler = new TaskQueryHandler(context);
        var query = new GetTaskByIdQuery(Guid.NewGuid());

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Should().BeNull();
    }
}
