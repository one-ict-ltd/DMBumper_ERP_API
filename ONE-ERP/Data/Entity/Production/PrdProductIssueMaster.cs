using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Production
{
    public class PrdProductIssueMaster:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productIssueMasterId { get; set; }

        public string IssueNo { get; set; }
        public DateTime IssueDate { get; set; }

        public string TypeofIssue { get; set; }

        public int? PrdRequisitionMasterId { get; set; }
        public PrdRequisitionMaster PrdRequisitionMaster { get; set; }

        public decimal? IssueQty { get; set; }

        public int? status { get; set; }
        public string remarks { get; set; }
        public int? bomForId { get; set; }
    }
}
