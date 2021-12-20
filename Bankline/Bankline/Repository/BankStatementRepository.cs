using Bankline.Data;
using Bankline.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bankline.Repository
{
    public class BankStatementRepository : IBankStatementRepository
    {
        private readonly BankDbContext _bankDbContext;
        protected readonly DbSet<BankStatementModel> DbSet;
        public BankStatementRepository(BankDbContext db)
        {
            _bankDbContext = db;
            DbSet = db.Set<BankStatementModel>();
        }
        public virtual async Task Adicionar(BankStatementModel bankStatement)
        {
            _bankDbContext.Add(bankStatement);
            await SaveChagens();
        }

        public virtual async Task<List<BankStatementModel>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<int> SaveChagens()
        {
            return await _bankDbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _bankDbContext?.Dispose();
        }
    }
}
