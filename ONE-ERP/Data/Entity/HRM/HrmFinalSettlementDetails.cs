using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmFinalSettlementDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int finalSettlementDetailsId { get; set; }
        public int finalSettlementMasterId { get; set; }
        public int finalSettlementHeadId { get; set; }
        public string monthOrParticulars { get; set; }
        public string days { get; set; }
        public decimal amount { get; set; }
    }

}
