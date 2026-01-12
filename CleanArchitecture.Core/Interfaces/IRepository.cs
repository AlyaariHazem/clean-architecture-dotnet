namespace CleanArchitecture.Infrastracture.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        ValueTask<TEntity> AddAsync(TEntity entity);
        ValueTask<TEntity> Get(string entityId);
        ValueTask<IEnumerable<TEntity>> GetAll();
        void UpdateAsynce(TEntity entity);
        ValueTask DeleteAsynce(string entityId);
        int SaveChanges();
    }
}
