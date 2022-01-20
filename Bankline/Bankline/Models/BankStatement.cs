using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bankline.Models
{
    public class BankStatement
    {
        public List<Transaction> AllTransaction { get; set; }
        public BankStatement()
        {
            AllTransaction = new List<Transaction>();
        }
    }
}
