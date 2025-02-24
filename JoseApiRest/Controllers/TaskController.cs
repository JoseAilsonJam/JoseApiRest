using JoseApiRest.Application.Commands;
using JoseApiRest.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;
    public TasksController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _mediator.Send(new GetAllTasksQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id) 
        => Ok(await _mediator.Send(new GetTaskByIdQuery(id)));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskCommand command)
    {
        var task = await _mediator.Send(command);
        return Ok(task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTaskCommand command)
    {
        if (id != command.Id) return BadRequest();
        return Ok(await _mediator.Send(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) 
        => Ok(await _mediator.Send(new DeleteTaskCommand(id)));
}
