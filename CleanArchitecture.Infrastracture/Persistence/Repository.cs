using CleanArchitecture.Infrastracture.Interfaces;
using CleanArchitecture.Infrastracture.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastracture.Persistence
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private ApplicationDbContext _context = null;
        private DbSet<TEntity> _entity;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entity = _context.Set<TEntity>();
        }
        public async ValueTask<TEntity> AddAsync(TEntity entity)
        {
            await _entity.AddAsync(entity);
            return entity;
        }

        public async ValueTask DeleteAsynce(string entityId)
        {
            var oDate = await _entity.FindAsync(entityId);
            _entity.Remove(oDate);

        }

        public async ValueTask<TEntity> Get(string entityId) =>
            await _entity.FindAsync(entityId);


        public async ValueTask<IEnumerable<TEntity>> GetAll() =>
            await _entity.ToListAsync();

        public void UpdateAsynce(TEntity entity) =>
            _entity.Attach(entity);

        public int SaveChanges() =>
            _context.SaveChanges();
    }
}
