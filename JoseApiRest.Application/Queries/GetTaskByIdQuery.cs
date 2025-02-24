using JoseApiRest.Domain.Entitys;
using MediatR;

namespace JoseApiRest.Application.Queries;

public record GetTaskByIdQuery(Guid Id) : IRequest<TaskItem?>;