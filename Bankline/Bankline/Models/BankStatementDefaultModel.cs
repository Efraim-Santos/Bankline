using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bankline.Models
{
    public class BankStatementDefaultModel
    {
        public List<TransactionDefaultModel> Transacoes { get; set; }
        public BankStatementDefaultModel()
        {
            Transacoes = new List<TransactionDefaultModel>();
        }
    }
}
