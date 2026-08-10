using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Models
{
    public class SalesBatchsViewModel
    {
        public int Id { get; set; }
        public string batchNo { get; set; }        
        public decimal currentStock { get; set; }
        public int? isProcess { get; set; }
        
    }
}
