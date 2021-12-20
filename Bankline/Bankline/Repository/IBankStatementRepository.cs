using Bankline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bankline.Repository
{
    public interface IBankStatementRepository : IDisposable
    {
        Task Adicionar(BankStatementModel bankStatement);
        Task<List<BankStatementModel>> ObterTodos();
        Task<int> SaveChagens();
    }
}
