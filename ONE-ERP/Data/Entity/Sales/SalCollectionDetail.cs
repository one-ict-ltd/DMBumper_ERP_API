using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Sales
{
    public class SalCollectionDetail : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int collectionDetailId { get; set; }
        public int? collectionMasterId { get; set; } 
        public SalCollectionMaster collectionMaster { get; set; }
        public int? paymentModeId { get; set; }
        public SalPaymentMode paymentMode{get;set; }
        public decimal? collectionAmount { get; set; }
        public decimal? bonusAmount { get; set; }//Collection Discount Amt.
        public decimal? bonusPercent { get; set; }//Collection Discount %
        public decimal? incentiveAmount { get; set; }//Product Discount Amt.

        [MaxLength(250)]
        public string productDiscountPercent { get; set; }//Prod. Disc. %
        public decimal? vatAdjustment { get; set; }
        public int? salesInvoiceId { get; set; }
        [MaxLength(250)]
        public string bankName { get; set; }
        [MaxLength(250)]
        public string chequeNo { get; set; }
        [MaxLength(250)]
        public string trxNo { get; set; }
    }
}
