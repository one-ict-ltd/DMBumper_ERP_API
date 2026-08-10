using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Production
{
    public class PrdReagentIssueMaster:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int reagentIssueMasterId { get; set; }
        public string issueNo { get; set; }
        public DateTime issueDate { get; set; }
        public string typeofIssue { get; set; }
        public int? reagentReqMasterId { get; set; }
        public PrdReagentReqMaster reagentReqMaster { get; set; }
        public decimal? issueQty { get; set; }
        public int? status { get; set; }
        public string remarks { get; set; }
        public int? bomForId { get; set; }
    }
}
