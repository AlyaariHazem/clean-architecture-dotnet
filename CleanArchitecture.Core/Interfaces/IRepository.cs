namespace CleanArchitecture.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        ValueTask<TEntity> AddAsync(TEntity entity);
        ValueTask<TEntity?> GetByIdAsync(int id);
        ValueTask<IEnumerable<TEntity>> GetAllAsync();
        void Update(TEntity entity);
        void Delete(TEntity entity);
        ValueTask<int> SaveChangesAsync();
        int SaveChanges();
    }
}
