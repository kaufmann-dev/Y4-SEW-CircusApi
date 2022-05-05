using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Repositories.Interfaces; 

public interface IRepository<TEntity> where TEntity : class {

    Task<TEntity> ReadAsync(int id);

    Task<List<TEntity>> ReadAsync(Expression<Func<TEntity,bool>> filter);

    Task<List<TEntity>> ReadAllAsync();

    Task<List<TEntity>> ReadAllAsync(int start, int count);

    Task<TEntity> CreateAsync(TEntity entity);

    Task UpdateAsync(TEntity entity);

    Task DeleteAsync(TEntity entity);

}