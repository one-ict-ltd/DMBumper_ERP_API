using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.MasterData.Models
{
    public class MenuWiseTransactionDateUnlockViewModel
    {
        public int? unlockId { get; set; }
        public int? employeeId { get; set; }
        public string menuName { get; set; }
        public int? backDays { get; set; }
        public int? forwardDays { get; set; }
        public string uptoDate { get; set; }
        public List<MenuWiseTransactionDateUnlockViewModel> lstDetailsData { get; set; }
    }
}
