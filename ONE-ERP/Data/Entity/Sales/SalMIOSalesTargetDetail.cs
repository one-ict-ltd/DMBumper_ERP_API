using ONEERP.Data.Entity.Inventory;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ONEERP.Data.Entity.Sales
{
    public class SalMIOSalesTargetDetail : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int targetdetailId { get; set; }

        public int? salMIOSalesTargetMasterId { get; set; }
        public SalMIOSalesTargetMaster salMIOSalesTargetMaster { get; set; }

        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
        public decimal? targetQty { get; set; }
        public decimal? targetvalue { get; set; }
        public decimal? looseQty { get; set; }
        public decimal? CtnQty { get; set; }
    }
    public class SalMIOSalesTargetDetailYearly : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int targetdetailYearlyId { get; set; }
        public int? mioSalesTargetMasterYearlyId { get; set; }
        public SalMIOSalesTargetMasterYearly mioSalesTargetMasterYearly { get; set; }

        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
        public decimal? targetQty { get; set; }
        public decimal? targetvalue { get; set; }
        public decimal? looseQty { get; set; }
        public decimal? CtnQty { get; set; }
    }
}
