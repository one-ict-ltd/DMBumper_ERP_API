using ONEERP.Data.Entity.HrmMaster;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmFinalSettlementSignatory : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int signatoryId { get; set; }
        public string signatoryType { get; set; }
        public int sortOrder { get; set; }
        public int finalSettlementHeadId { get; set; }
        public int employeeId { get; set; }
        public int status { get; set; }
        public int finalSettlementMasterId { get; set; }
        public string remarks { get; set; }
        public int? isApprove { get; set; }
    }
}
