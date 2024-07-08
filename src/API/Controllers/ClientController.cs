using API.Requests;
using Application.Commands.AddClientCommand;
using Application.Commands.DeleteClientCommand;
using Application.Commands.UpdateClientCommand;
using Application.Queries.GetClientByIdQuery;
using Application.Queries.GetClientQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost]
    public async Task<ActionResult<int>> CreateClient([FromBody] CreateUpdateClient request, CancellationToken cancellationToken)
    {
        var command = new AddClientCommandInput(request.Nome, request.Porte);
        var client = await _mediator.Send(command, cancellationToken);

        if (client.Id == Guid.Empty)
            return BadRequest("Erro ao cadastrar o cliente.");

        return CreatedAtAction(nameof(CreateClient), client);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> UpdateClient([FromRoute] Guid id, [FromBody] CreateUpdateClient request, CancellationToken cancellationToken)
    {
        var command = new UpdateClientCommandInput(id, request.Nome, request.Porte);
        var client = await _mediator.Send(command, cancellationToken);

        if (client.Id == Guid.Empty)
            return BadRequest($"Erro ao atualizar o cliente {command.Id}.");

        return Ok(client);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteClient([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var client = await _mediator.Send(new DeleteClientCommandInput(id), cancellationToken);

        if (client.Id == Guid.Empty)
            return BadRequest($"Erro ao excluir o cliente {id}.");

        return Ok(client);
    }

    [HttpGet]
    public async Task<ActionResult<GetClientQueryResult>> GetClients(CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetClientQueryInput(), cancellationToken));

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<GetClientByIdQueryResult>> GetClient([FromRoute] Guid id, CancellationToken cancellationToken)
        => await _mediator.Send(new GetClientByIdQueryInput(id), cancellationToken);
}