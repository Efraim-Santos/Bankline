using Bankline.Models;
using System;
using System.Collections.Generic;

namespace Bankline.Models
{
    public class AllStatementBankSavedViewModel
    {
        public int Id { get; set; }
        public List<BankStatementModel> ListBankStatements {get; set;}
        public AllStatementBankSavedViewModel()
        {
            ListBankStatements = new List<BankStatementModel>();
        }
    }
}
