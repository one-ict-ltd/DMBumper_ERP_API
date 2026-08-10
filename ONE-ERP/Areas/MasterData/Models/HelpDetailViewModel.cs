using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.MasterData.Models
{
    public class HelpDetailViewModel
    {

        
        public int? helpDetailId { get; set; }
        public int? helpId { get; set; }
        public string dtext { get; set; }
        public int? ddropdownId { get; set; }
        public DateTime? ddate { get; set; }
        public string dpopup { get; set; }
        public bool? dcheckbox { get; set; }
        public int? dradio { get; set; }
        public string dImage { get; set; }
        public string dtextArea { get; set; }
        public int? isActive { get; set; }
        public int? isDelete { get; set; }

    }
}
