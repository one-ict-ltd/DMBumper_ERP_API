using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class QuotationCollectionViewModel
    {
        public int? quotationCollectionMasterId { get; set; }
        public DateTime? quotationCollectionMasterDate { get; set; }
        public int? PurRequisitionFinalizeDetailId { get; set; }
        public int? status { get; set; }
        public string remarks { get; set; }
        public bool? isActive { get; set; }
        public int? quotationTypeId { get; set; } // For Local Puorchase or Import 
        public List<QuotationCollectionDetailsViewModel> lstQuoDetailsViewModel { get; set; }
    }

    public class QuotationCollectionDetailsViewModel
    {
        public int? quotationCollectionDetailId { get; set; }
        public int? supplierId { get; set; }
        public string supplierName { get; set; }
        public decimal? qty { get; set; }
        public decimal? rate { get; set; }  // SIGHT Rate - Cash
        public decimal? deferredRate { get; set; } //  Deferred Rate -Credit 
        public decimal? amount { get; set; }
        public decimal? deferredAmount { get; set; }
        public int? PurQuotationCollectionMasterId { get; set; }
        public bool? isActive { get; set; }
        public string manufactureOrigin { get; set; }
        public int? PurRequisitionFinalizeDetailId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public decimal? VatPercent { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? TotalRate { get; set; }
        public int? BudgetCreateId { get; set; }
        public decimal? Discount { get; set; }
    }
}
