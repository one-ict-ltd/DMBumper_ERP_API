using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Production.Models
{
    public class TransferNoteViewModel
    {
        public int productTransferId { get; set; }
        public string transferNoteNo { get; set; }
        public DateTime transferDate { get; set; }
        public int? productionPlanId { get; set; }
        public string batchNo { get; set; }
        public decimal? batchWeight { get; set; }
        public int? noOfBox { get; set; }
        public decimal? qtyPerShipper { get; set; }
        public decimal? equivalentWeight { get; set; }
        public int? weightUOMId { get; set; }
        public int? prdPlanProcessId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public decimal? transferQty { get; set; }
        public string remarks { get; set; }
        public string transferIssuedBy { get; set; }
        public DateTime manufacturingDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal? totalCommercialQty { get; set; }
        public decimal? transfered { get; set; }
        public decimal? remainQty { get; set; }
        public Boolean isComplete { get; set; }
        public string batchTypeName { get; set; }
        public int? SecndproductWiseSpecificationId { get; set; }
        public int? sQtyPerPack { get; set; }
        public decimal? sWeightPerPack { get; set; }
        public decimal? MRP { get; set; }
        public string ReleaseStatus { get; set; }
        public string ReleaseRemarks { get; set; }
    }
}
