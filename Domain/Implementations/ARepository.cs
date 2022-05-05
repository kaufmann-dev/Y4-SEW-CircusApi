using System.Linq.Expressions;
using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configurations;

namespace Domain.Repositories.Implementations; 

public class ARepository<TEntity> : IRepository<TEntity> where TEntity : class {

    protected CircusDbContext _testDbContext;
    protected DbSet<TEntity> _table;

    public ARepository(CircusDbContext dbContext) {
        this._testDbContext = dbContext;
        _table = _testDbContext.Set<TEntity>();
    }

    public async Task<TEntity> ReadAsync(int id) => await _table.FindAsync(id);

    public async Task<List<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> filter) =>
        await _table.Where(filter).ToListAsync();

    public async Task<List<TEntity>> ReadAllAsync() =>
        await _table.ToListAsync();

    public async Task<List<TEntity>> ReadAllAsync(int start, int count) =>
        await _table.Skip(start).Take(count).ToListAsync();

    public async Task<TEntity> CreateAsync(TEntity entity) {
        _table.Add(entity);
        await _testDbContext.SaveChangesAsync();
        return entity; // gibt die ID(Autoincremetn) vom Insert zurück für die Weiterverarbeitung
    }

    public async Task UpdateAsync(TEntity entity) {
        _table.Update(entity);
        await _testDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity) {
        _table.Remove(entity);
        await _testDbContext.SaveChangesAsync();
    }
}
