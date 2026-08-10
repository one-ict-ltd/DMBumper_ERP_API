using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Production.Models
{
    public class BomDetailsViewModelForApproval
    {
        public int bomDetailsId { get; set; }
        public int bomId { get; set; }
        public int pendingbomDetailsId { get; set; }
        //public int pendingbomId { get; set; }
        public int bomDetailsProductWiseSpecificationId { get; set; }
        public decimal qty { get; set; }
        public decimal price { get; set; }
        public decimal wastage { get; set; }
        public decimal totalQty { get; set; }
        public decimal totalPrice { get; set; }
        public bool isActive { get; set; }
        public bool isSelect { get; set; }
        public string assay { get; set; }
        public int potencyEffect { get; set; }
        public int? bomForId { get; set; }
    }
}
