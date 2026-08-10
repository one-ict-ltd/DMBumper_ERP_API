using System;
using System.Collections.Generic;


namespace ONEERP.Areas.Production.Models
{
    public class BomActiveInActiveViewModel
    {
        public List<BomlstMasterViewModel> lstMasterViewModel { get; set; }
    }
    public class BomlstMasterViewModel
    {
        public int bomId { get; set; }
        public bool isSelect { get; set; }
    }
}
