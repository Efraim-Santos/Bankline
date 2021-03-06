using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bankline.Models
{
    public class BankStatementModel : Entity
    {
        public ICollection<TransactionModel> Transaction { get; set; }
        public string StatementPeriod { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
