using Bankline.Data;
using Bankline.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bankline.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {

        protected readonly BankDbContext _bankDbContext;
        protected readonly DbSet<TEntity> DbSet;
        public Repository(BankDbContext db)
        {
            _bankDbContext = db;
            DbSet = db.Set<TEntity>();
        }
        public virtual async Task AddNew(TEntity entity)
        {
            var entityDate = entity.GetType().GetProperty("RegisterDate");

            if (entityDate != null)
                entityDate.SetValue(entity, Convert.ChangeType(DateTime.Now, entityDate.PropertyType));

            _bankDbContext.Add(entity);

            await SaveChagens();
        }

        public virtual async Task<List<TEntity>> GetAll()
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
