using ONEERP.Data.Entity.Inventory;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Sales
{
    public class SalSalesReturnDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int salesReturnDetailId { get; set; }
        public int? salesReturnMasterId { get; set; }
        public SalSalesReturnMaster salesReturnMaster { get; set; }
        public int? salesInvDetailsId { get; set; }
        public SalSalesInvoiceDetails salesInvoiceDetails { get; set; }
        public decimal? CntQty { get; set; }
        public decimal? looseQty { get; set; }
        public int? toUomId { get; set; }

        public decimal? returnQty { get; set; }
        public decimal? unitPrice { get; set; }
        public decimal? vatPercent { get; set; }
        public decimal? aitPercent { get; set; }
        public decimal? discountPercent { get; set; }
        public decimal? totalAmount { get; set; }        
    }
}
