using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.MasterData.Models
{
    public class HelpMasterListViewModel
    {
        public int? helpId { get; set; }
        public string text { get; set; }
        public int? dropDownId { get; set; }
        public DateTime? date { get; set; }
        public string popUp { get; set; }
        public string checkbox { get; set; }
        public string textArea { get; set; }
        public string radio { get; set; }
        public int? isActive { get; set; }
    }
}
