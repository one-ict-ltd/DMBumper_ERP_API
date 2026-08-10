using ONEERP.Data.Entity.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Sales
{
    public class SalCollectionMaster:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int collectionMasterId { get; set; }
        [MaxLength(250)]
        public string collectionNumber { get; set; }
        public string moneyReceiptNo { get; set; }
        public int? moneyReceiptId { get; set; }
        public MoneyReceiptNote moneyReceipt { get; set; }
        public int? partyId { get; set; }
        public AccParty party { get; set; }
        public int? salesInvoiceId { get; set; }
        public SalSalesInvoice salSalesInvoice { get; set; }
        public DateTime? collectionDate { get; set; }
        public decimal? collectionAmount { get; set; }
        public string remarks { get; set; }
        public int? paymentModeId { get; set; }
        public string chequeNo { get; set; }
        public DateTime? chequeDate { get; set; }
        public string trxNo { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
        public bool? hasRemittance { get; set; }
    }
}
