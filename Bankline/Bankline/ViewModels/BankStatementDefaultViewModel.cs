using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bankline.Models
{
    public class BankStatementDefaultViewModel
    {
        public List<TransactionDefaultViewModel> Transacoes { get; set; }
        public BankStatementDefaultViewModel()
        {
            Transacoes = new List<TransactionDefaultViewModel>();
        }
    }
}
