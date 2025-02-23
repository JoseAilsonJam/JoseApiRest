using JoseApiRest.Domain.Entitys;
using MediatR;

namespace JoseApiRest.Application.Commands;

public record CreateTaskCommand(string Title, string Description, DateTime CompletionDate, bool IsCompleted) : IRequest<TaskItem>;