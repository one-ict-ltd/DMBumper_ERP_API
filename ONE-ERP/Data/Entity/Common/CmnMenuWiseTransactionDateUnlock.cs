using ONEERP.Data.Entity.HRM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Common
{
    public class CmnMenuWiseTransactionDateUnlock : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int unlockId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public string menuName { get; set; }
        public int? backDays { get; set; }
        public int? forwardDays { get; set; }
        public DateTime? uptoDate { get; set; }
    }
}
