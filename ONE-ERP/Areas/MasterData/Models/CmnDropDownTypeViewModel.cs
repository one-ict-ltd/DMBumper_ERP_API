using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.MasterData.Models
{
    public class CmnDropDownTypeViewModel
    {
        public int? dropDownTypeId { get; set; }       
        public string dropDownType { get; set; }
        public bool? isActive { get; set; }
    }
}
