using ONEERP.Data.Entity.Inventory;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Production
{
    public class PrdProductionQaMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productionQaId { get; set; }
        public int? productionPlanId { get; set; }
        public DateTime QCDate { get; set; }
        public int? prdPlanProcessId { get; set; }
        public string remarks { get; set; }
        public string approvalStatus { get; set; }
    }
}
