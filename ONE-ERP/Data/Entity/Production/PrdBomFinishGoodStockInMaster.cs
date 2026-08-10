using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Production
{
    public class PrdBomFinishGoodStockInMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int bomStockInId { get; set; }
        public string stockInNo { get; set; }
        public int? companyId { get; set; }
        public CmnCompany cmnCompany { get; set; }
        public int? sbuId { get; set; }
        public CmnSpecialBranchUnit sbu { get; set; }
        public int? storeId { get; set; }
        public CmnStore store { get; set; }
        public DateTime? stockInDate { get; set; }
    }
}
