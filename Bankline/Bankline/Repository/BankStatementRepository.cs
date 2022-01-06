using Bankline.Data;
using Bankline.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bankline.Repository
{
    public class BankStatementRepository : Repository<BankStatementModel>, IBankStatementRepository
    {
        public BankStatementRepository(BankDbContext context) : base(context)
        {

        }
    }
}
