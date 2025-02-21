using MediatR;

namespace JoseApiRest.Application.Commands;

public record DeleteTaskCommand(int Id) : IRequest<bool>;