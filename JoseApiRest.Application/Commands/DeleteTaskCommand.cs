using MediatR;

namespace JoseApiRest.Application.Commands;

public record DeleteTaskCommand(Guid Id) : IRequest<bool>;