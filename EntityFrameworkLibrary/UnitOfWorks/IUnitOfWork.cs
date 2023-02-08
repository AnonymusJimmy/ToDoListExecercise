using EntityFrameworkLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkLibrary.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IToDoListRepository ItemRepository { get; }
        Task SaveChangesAsync();
        void Dispose();
    }
}
