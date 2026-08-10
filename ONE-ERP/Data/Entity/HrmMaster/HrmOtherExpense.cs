using ONEERP.Data.Entity.HRM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HrmMaster
{
    public class HrmOtherExpense:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int otherExpenseId { get; set; }

        public DateTime? EndDateOfMonth { get; set; }

        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }

        public decimal? amount { get; set; }

        public string remarks { get; set; }
    }
}
