using JoseApiRest.Application.Queries;
using JoseApiRest.Domain.Entitys;
using JoseApiRest.Infrastructure.Services.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JoseApiRest.Application.Handlers;

public class TaskQueryHandler :
    IRequestHandler<GetAllTasksQuery, IEnumerable<TaskItem>>,
    IRequestHandler<GetTaskByIdQuery, TaskItem>
{
    private readonly DataContext _context;

    public TaskQueryHandler(DataContext context) => _context = context;

    public async Task<IEnumerable<TaskItem>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken = default)
        => await _context.TaskItems.ToListAsync(cancellationToken);

    public async Task<TaskItem> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken = default)
        => await _context.TaskItems.FindAsync(request.Id, cancellationToken);
}
