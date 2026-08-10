using ONEERP.Data.Entity.Accounting;
using ONEERP.Data.Entity.Inventory;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ONEERP.Data.Entity.Sales
{
    public class SalSalesProductExpireReturn : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productExpireReturnId { get; set; }
        public string expireReturnNumber { get; set; }
        public DateTime? returnDate { get; set; }
        public int? partyId { get; set; }
        public AccParty party { get; set; }
        public int? productExpireReturnMasterId { get; set; }
        public SalSalesProductExpireReturnMaster productExpireReturnMaster { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
        public int? salesInvoiceId { get; set; }
        public SalSalesInvoice salesInvoice { get; set; }
        public decimal? qty { get; set; }
        public decimal? amount { get; set; }
    }
    public class SalSalesProductExpireReturnDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int expireReturnDetailsId { get; set; }
        public int? productExpireReturnId { get; set; }
        public SalSalesProductExpireReturn productExpireReturn { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public InvProductWiseSpecification productWiseSpecification { get; set; }
        public decimal? qty { get; set; }
        public decimal? price { get; set; }
        public decimal? amount { get; set; }
        public string batchNo { get; set; }
        public DateTime? mgfDate { get; set; }
        public DateTime? expireDate { get; set; }



    }
}
