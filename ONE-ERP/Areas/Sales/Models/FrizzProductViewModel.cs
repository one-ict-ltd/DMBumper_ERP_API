using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class FrizzProductViewModel
    {
        public int? productWiseSpecificationId { get; set; }
        public string batchNumbers { get; set; }
        public bool isSelect { get; set; }
    }
}
