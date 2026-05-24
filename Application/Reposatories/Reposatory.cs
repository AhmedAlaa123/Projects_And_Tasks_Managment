using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Exceptions;
using InfraStructure.context;
using Microsoft.EntityFrameworkCore;

namespace Application.Reposatories;

public class Reposatory<T> : IReposatory<T> where T : class
{
    private readonly ApplicationDbContext _dbContext;

    public Reposatory(ApplicationDbContext dbContext) => _dbContext = dbContext;


    public async Task<T> Add(T item)
    {
       _dbContext.Set<T>().Add(item);
        await  _dbContext.SaveChangesAsync();
        return item;
    }

    public async Task<T> Delete(T item)
    {
        _dbContext.Set<T>().Remove(item);
        await _dbContext.SaveChangesAsync() ;
        return item;
    }

   

    public IQueryable<T> GetAll()
    {
       var data=_dbContext.Set<T>().AsQueryable();
        return data;
    }

    public T GetById<Td>(Td id)
    {
        var entity = _dbContext.Set<T>().Find(id);
        return entity ?? throw new NotFoundException($"{typeof(T).Name} with id {id} not found");
    }

    public async Task<T> Update(T item)
    {
       _dbContext.Set<T>().Update(item);
       await _dbContext.SaveChangesAsync();
        return item;
    }
    public async Task<int> TotalCount()
    {
      return await _dbContext.Set<T>().CountAsync();
    }
}
