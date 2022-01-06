using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bankline.Models
{
    public class TransactionModel : Entity
    {
        public string Type { get; set; }
       
        public DateTime Date { get; set; }
       
        public decimal Value { get; set; }

        public string Description { get; set; }
    }
}
