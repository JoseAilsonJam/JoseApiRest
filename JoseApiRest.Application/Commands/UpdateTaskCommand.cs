using MediatR;

namespace JoseApiRest.Application.Commands;

public record UpdateTaskCommand(int Id, string Title, string Description, DateTime CompletionDate, bool IsCompleted) : IRequest<bool>;