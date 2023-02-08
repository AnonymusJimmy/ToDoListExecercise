using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkLibrary.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }



        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }


        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }


        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }


        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }


        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }
    }
}
