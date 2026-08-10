using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.MasterData.Models
{
    public class AutoStockInOutSettingViewModel
    {
        public int id { get; set; }
        public string manuName { get; set; }
        public bool isAutoStock { get; set; }
    }
}
