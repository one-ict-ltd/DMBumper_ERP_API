using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Production.Models
{
    public class BomMasterViewModelForApproval
    {
        public int bomId { get; set; }
        public int pendingbomId { get; set; }
        public string bomNo { get; set; }
        public string bomName { get; set; }
        public DateTime bomDate { get; set; }
        public string bomDescription { get; set; }
        public int bomProductWiseSpecificationId { get; set; }
        public decimal bomQty { get; set; }
        public decimal bomTotalCost { get; set; }
        public bool isActive { get; set; }
        public string materialsType { get; set; }
        public List<BomDetailsViewModelForApproval> pendinglstDetailsViewModel { get; set; }
        public string bomType { get; set; }
        public decimal? weightPerPack { get; set; }
        public int? WeightPerPackUOM { get; set; }
        public decimal batchWeight { get; set; }
        public int? batchWeightUOMId { get; set; }
        public int? phGroupMasterId { get; set; }
        public int shelfLife { get; set; }

        public decimal? packSizeForPM { get; set; }
    }
}
