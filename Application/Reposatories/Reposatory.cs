using Application.Contracts;
using InfraStructure.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public async Task<T> Update(T item)
    {
       _dbContext.Set<T>().Update(item);
       await _dbContext.SaveChangesAsync();
        return item;
    }
}
