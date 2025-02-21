using JoseApiRest.Application.Commands;
using JoseApiRest.Domain.Entitys;
using JoseApiRest.Infrastructure.Services.EntityFramework;
using MediatR;

namespace JoseApiRest.Application.Handlers;

public class TaskCommandHandler :
    IRequestHandler<CreateTaskCommand, int>,
    IRequestHandler<UpdateTaskCommand, bool>,
    IRequestHandler<DeleteTaskCommand, bool>
{
    private readonly DataContext _context;

    public TaskCommandHandler(DataContext context) => _context = context;

    public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken = default)
    {
        var task = new TaskItem
        {
            Title = request.Title,
            Description = request.Description,
            CompletionDate = request.CompletionDate
        };

        _context.TaskItems.Add(task);
        await _context.SaveChangesAsync(cancellationToken);
        return task.Id;
    }

    public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken = default)
    {
        var task = await _context.TaskItems.FindAsync(request.Id, cancellationToken);
        if (task is null)
            return false;

        task.Title = request.Title;
        task.Description = request.Description;
        task.CompletionDate = request.CompletionDate;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken = default)
    {
        var task = await _context.TaskItems.FindAsync(request.Id, cancellationToken);
        if (task is null)
            return false;

        _context.TaskItems.Remove(task);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}