using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.MasterData.Models
{
    public class HelpMasterViewModel
    {
        

        public int? helpId { get; set; }
        public string text { get; set; }
        public int? dropDownId { get; set; }
        public DateTime? date { get; set; }
        public string popUp { get; set; }
        public bool? checkbox { get; set; }
        public string textArea { get; set; }
        public int? radio { get; set; }
        public int? isActive { get; set; }
        public int? isDelete { get; set; }
        public List<HelpDetailViewModel> lstdetailmodel { get; set; }
        public List<HelpMultiViewModel> lstmultimodel { get; set; }
        public List<HelpImageViewModel> lstimagemodel { get; set; }

    }
}
