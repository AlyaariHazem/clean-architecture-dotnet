namespace CleanArchitecture.Infrastracture.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        ValueTask<TEntity> AddAsync(TEntity entity);
        ValueTask<TEntity> Get(string EntityId);
        ValueTask<IEnumerable<TEntity>> GetAll();
        ValueTask<TEntity> UpdateAsynce(TEntity entity);
        ValueTask DeleteAsynce(TEntity entity);
    }
}
