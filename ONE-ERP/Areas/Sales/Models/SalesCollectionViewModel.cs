using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class SalesCollectionViewModel
    {
        public int? collectionMasterId { get; set; }
        public string collectionNumber { get; set; }
        //public DateTime? collectionDate { get; set; }
        //public DateTime? paymentDate { get; set; }
        public int? partyId { get; set; }
        public int? salesInvoiceId { get; set; }
        public DateTime? collectionDate { get; set; }
       
        public decimal? collectionAmount { get; set; }
        
        public string remarks { get; set; }
        public bool? isActive { get; set; }
        public List<SalesCollectionDetailsViewModel> lstDetailsViewModel { get; set; }
        
    }
    public class SalesCollectionFromDispatchViewModel
    {
        public int? distributionMasterId { get; set; }
        public DateTime? startDate { get; set; }
        public string number { get; set; }
        public int? paymentModeId { get; set; }
        public string chequeNo { get; set; }
        public DateTime? chequeDate { get; set; }
        public List<SalesCollectionDetailsFromDispatchViewModel> lstMasterViewModel { get; set; }
        public bool? isActive { get; set; }
    }
    public class SalesCollectionDetailsFromDispatchViewModel
    {
        public int collectionDetailId { get; set; }
        public int? salesInvoiceId { get; set; }
        public decimal? grandTotal { get; set; }
        public decimal? collectionAmount { get; set; }
        public decimal? dueAmount { get; set; }
        public decimal? bonusDiscount { get; set; }
        public bool? isSelect { get; set; }
        
    }



    public class SalesCollectionFromDispatchViewModel_v2
    {
        public int? distributionMasterId { get; set; }
        public int? collectionMasterId { get; set; }
        public DateTime? collectionDate { get; set; }
        public DateTime? startDate { get; set; }
        public string collectionNumber { get; set; }//collectionNumber
        public string moneyReceiptNo { get; set; }
        public int? moneyReceiptId { get; set; }
        public decimal? ttlCollectionAmount { get; set; }
        public decimal? collectionAmount { get; set; }
        public int? paymentModeId { get; set; }
        public int? partyId { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
        public string chequeNo { get; set; }
        public DateTime? chequeDate { get; set; }
        public string remarks { get; set; }
        public bool? isActive { get; set; }
        public List<SalesCollectionDetailsFromDispatchViewModel_v2> lstDetailsViewModel { get; set; }
    }
    public class SalesCollectionDetailsFromDispatchViewModel_v2
    {
        public int collectionDetailId { get; set; }
        public int? salesInvoiceId { get; set; }
        public decimal? grandTotal { get; set; }
        public decimal? collectionAmount { get; set; }
        public decimal? dueAmount { get; set; }
        public decimal? bonusDiscount { get; set; }
        public decimal? incentiveAmount { get; set; }
        public decimal? vatAdjustment { get; set; }
        public decimal? percentValue { get; set; }
        public string productDiscountPercent { get; set; }
        public bool? isSelect { get; set; }

    }
}
