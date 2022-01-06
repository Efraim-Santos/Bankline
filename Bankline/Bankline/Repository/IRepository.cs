using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bankline.Repository
{
    public interface IRepository<TEntity> : IDisposable
    {
        Task AddNew(TEntity bankStatement);
        Task<List<TEntity>> GetAll();
        Task<int> SaveChagens();
    }
}
