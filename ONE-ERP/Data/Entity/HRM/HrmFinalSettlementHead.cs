using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmFinalSettlementHead : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int finalSettlementHeadId { get; set; }
        public string finalSettlementHeadName { get; set; }
        public int sortOrder { get; set; }
        public string finalSettlementHeadType { get; set; }
    }
}
