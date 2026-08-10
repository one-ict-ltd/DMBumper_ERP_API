using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.MasterData.Models
{
    public class CmnDropDownViewModel
    {

        public int? dropDownId { get; set; }
        public int? dropDownTypeId { get; set; }
        public string dropDownValue { get; set; }
        public string dropDownText { get; set; }       
        public int? sortOrder { get; set; }        
        public bool? isActive { get; set; }

    }
}
