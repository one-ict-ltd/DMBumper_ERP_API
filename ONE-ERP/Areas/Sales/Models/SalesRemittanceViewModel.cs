using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Sales.Models
{
    public class SalesRemittanceMasterViewModel
    {
        public int remittanceId { get; set; }
        public DateTime? remittanceDate { get; set; }
        public decimal selectedAmount { get; set; }
        public List<SalesRemittanceViewModel> salesRemittanceDetails { get; set; }
        public List<SalesRemittanceSlipViewModel> salesRemittanceSlips { get; set; }
    }
        public class SalesRemittanceViewModel
    {
        public int remittanceId { get; set; }
        public DateTime? remittanceDate { get; set; }
        public int remittanceNo { get; set; }
        public int? remittanceTypeId { get; set; }
        public string oplTranNo { get; set; }
        public string depotCode { get; set; }
        public int? companyId { get; set; }
        public DateTime? depositDate { get; set; }
        public int? bankBranchId { get; set; }
        public string depositRefNo { get; set; }
        public decimal? depositAmount { get; set; }
        public string remarks { get; set; }
        
        //public List<HasRemittanceOfCollectionMasterUpdateViewModel> collections { get; set; }

    }

    public class HasRemittanceOfCollectionMasterUpdateViewModel
    {
        public int? remittanceId { get; set; }
        public int collectionMasterId { get; set; }
        public string collectionNumber { get; set; }
        public bool isSelect { get; set; }
    }
 
}