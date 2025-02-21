using JoseApiRest.Domain.Entitys;
using MediatR;

namespace JoseApiRest.Application.Queries;

public record GetAllTasksQuery() : IRequest<IEnumerable<TaskItem>>;