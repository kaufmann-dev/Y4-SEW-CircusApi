using System.Threading.Tasks;
using Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.Entities;


namespace CircusAPI.Controllers.Implementations;

[ApiController]
[Route("circus")]
public class CircusControllerStandalone : ControllerBase
{
    private ICircusRepository _repository { get; set; }
    private Logger<CircusController> _logger { get; set; }

    public CircusControllerStandalone(ICircusRepository ctx, Logger<CircusController> logger)
    {
        _repository = ctx;
        _logger = logger;
    }

    [HttpGet("{circusId:int}")]
    public async Task<ActionResult<Circus>> ReadAsync(int circusId)
    {
        var circus = await _repository.ReadAsync(circusId);
        if (circus is null)
        {
            _logger.LogInformation($"Could not read circus because it does not exist");
            return NotFound();
        }
        _logger.LogInformation($"Read circus {circus.ToString()}");
        return Ok(circus);
    }

    [HttpPost]
    public async Task<ActionResult<Circus>> CreateAsync(Circus circus)
    {
        await _repository.CreateAsync(circus);
        return Ok(circus);
    }

    [HttpPut("{circusId:int}")]
    public async Task<ActionResult<Circus>> UpdateAsync(Circus circus, int circusId)
    {
        var data = await _repository.ReadAsync(circusId);
        if (data is null || circusId != circus.Id)
        {
            return NotFound();
        }

        await _repository.UpdateAsync(circus);
        return NoContent();
    }

    [HttpDelete("{circusId:int}")]
    public async Task<ActionResult<Circus>> DeleteAsync(int circusId)
    {
        var data = await _repository.ReadAsync(circusId);
        if (data is null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(data);
        return NoContent();
    }

    [HttpGet]
    public string Ping()
    {
        return "pong";
    }
}