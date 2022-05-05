using Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CircusAPI.Controllers.Implementations;

public abstract class AController<TEntity> : ControllerBase where TEntity : class {
    protected IRepository<TEntity> _repository;

    public AController(IRepository<TEntity> repository) {
        _repository = repository;
    }

    [HttpPost]
    public async Task<ActionResult<TEntity>> CreateAsync(TEntity entity) {
        await _repository.CreateAsync(entity);
        return Ok(entity);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TEntity>> ReadAsync(int id) =>
        (await _repository.ReadAsync(id) is null) ? NotFound() : Ok(await _repository.ReadAsync(id));

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateAsync(TEntity entity, int id) {
        var data = await _repository.ReadAsync(id);
        if (data is null)
            return NotFound();

        await _repository.UpdateAsync(entity);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<TEntity>> DeleteAsync(TEntity entity, int id)
    {
        var data = await _repository.ReadAsync(id);
        if (data is null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(entity);
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<TEntity>> CountAsync()
    {
        var data = await _repository.ReadAllAsync();
        if (data is null)
        {
            return NotFound();
        }

        return Ok(data.Count);
    }
}