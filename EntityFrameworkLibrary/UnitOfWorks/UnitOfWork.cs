using EntityFrameworkLibrary.Context;
using EntityFrameworkLibrary.Repositories;

namespace EntityFrameworkLibrary.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ToDoListDbContext _dbContext;
        private IToDoListRepository _toDoListRepository;

        public UnitOfWork(ToDoListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IToDoListRepository ItemRepository
        {
            get
            {
                if(_toDoListRepository == null)
                {
                    _toDoListRepository = new ToDoListRepository(_dbContext);
                }
                return _toDoListRepository;
            }
        }

        public Task SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await _dbContext.DisposeAsync();
        }

    }
}
