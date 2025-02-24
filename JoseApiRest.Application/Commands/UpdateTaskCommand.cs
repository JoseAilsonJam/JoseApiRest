using MediatR;

namespace JoseApiRest.Application.Commands;

public record UpdateTaskCommand(Guid Id, string Title, string Description, DateTime CompletionDate, bool IsCompleted) : IRequest<bool>;