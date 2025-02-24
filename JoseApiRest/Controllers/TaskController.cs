using JoseApiRest.Application.Commands;
using JoseApiRest.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TaskController : ControllerBase
{
    private readonly IMediator _mediator;
    public TaskController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllTasksQuery());
        return result is not null 
            ? Ok(result) 
            : NotFound("Nenhuma tarefa encontrada.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetTaskByIdQuery(id));
        return result is not null 
            ? Ok(result) 
            : NotFound($"Tarefa com ID {id} não encontrada.");
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskCommand command)
    {
        if (command is null) return BadRequest("Dados da tarefa não fornecidos.");

        var createdTask = await _mediator.Send(command);
        return createdTask is not null 
            ? CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask) 
            : BadRequest("Falha ao criar a tarefa.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTaskCommand command)
    {
        if (command is null || id != command.Id) return BadRequest("Dados inválidos ou IDs não coincidem.");

        var updatedTask = await _mediator.Send(command);
        return updatedTask 
            ? Ok(updatedTask) 
            : NotFound($"Tarefa com ID {id} não encontrada para atualização.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _mediator.Send(new DeleteTaskCommand(id));
        return deleted 
            ? NoContent() 
            : NotFound($"Tarefa com ID {id} não encontrada para exclusão.");
    }
}
