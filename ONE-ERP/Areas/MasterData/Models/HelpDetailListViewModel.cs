using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.MasterData.Models
{
    public class HelpDetailListViewModel
    {
        public int? helpDetailId { get; set; }
        public int? helpId { get; set; }
        public string dtext { get; set; }
        public int? ddropDownId { get; set; }
        public DateTime? ddate { get; set; }
        public string dpopUp { get; set; }
        public string dcheckbox { get; set; }

        public string dradio { get; set; }
        public string dImage { get; set; }
        public int? isActive { get; set; }
    }
}
