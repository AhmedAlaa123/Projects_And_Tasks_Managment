namespace Application.Contracts;

public interface IReposatory<T> where T : class
{
   Task<T> Add(T item);
    Task<T> Update(T item);
    Task<T> Delete(T item);

    IQueryable<T> GetAll();

}
