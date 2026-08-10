using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurGrnLogtbl : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int grnLogMasterId { get; set; }
        public int grnDetailsId { get; set; }
        public DateTime? RetestDate { get; set; }
        public DateTime? prevRetestDate { get; set; }
        public decimal? TestReqQty { get; set; }
        public decimal? NoOfPackForRetest { get; set; }
        public string LocalOrImport { get; set; }
        //public int? TestTypeId { get; set; }
        public int? testApprovalStatus { get; set; }
    }
}
