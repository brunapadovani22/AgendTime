using AgendTime.Application.DTOs;
using AgendTime.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AgendTime.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController(IClientService clientService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientDto>>> GetAll(CancellationToken cancellationToken)
    {
        var clients = await clientService.GetAllAsync(cancellationToken);
        return Ok(clients);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ClientDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var client = await clientService.GetByIdAsync(id, cancellationToken);

        if (client is null)
            return NotFound();

        return Ok(client);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<ClientDto>>> Search([FromQuery] string term, CancellationToken cancellationToken)
    {
        var clients = await clientService.SearchAsync(term, cancellationToken);
        return Ok(clients);
    }

    [HttpPost]
    public async Task<ActionResult<ClientDto>> Create(CreateClientDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var client = await clientService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ClientDto>> Update(Guid id, UpdateClientDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var client = await clientService.UpdateAsync(id, dto, cancellationToken);
            return Ok(client);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await clientService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}