using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class ThanasViewModel
    {
        public int? thanasId { get; set; }
        public string thanaCode { get; set; }
        public string thanaName { get; set; }
        public string shortName { get; set; }
        public int? districtsId { get; set; }
        public bool? isActive { get; set; }
    }
}
