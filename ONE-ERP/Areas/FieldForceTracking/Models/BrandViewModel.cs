using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEICT.Areas.Schedule.Models
{
    public class BrandListViewModel
    {
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public string Code { get; set; }
        public string ModelName { get; set; }
        public string ModelNumber { get; set; }
        public string ColorCode { get; set; }
        public int IsActive { get; set; }
    }
}
