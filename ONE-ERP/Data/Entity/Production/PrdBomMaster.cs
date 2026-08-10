using ONEERP.Data.Entity.Common;
using ONEERP.Data.Entity.Inventory;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ONEERP.Data.Entity.Production
{
    public class PrdBomMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int bomId { get; set; }
        [MaxLength(20)]
        public string bomNo { get; set; }
        public int pendingbomId { get; set; }
        public int? companyId { get; set; }
        public CmnCompany cmnCompany { get; set; }
        public int? sbuId { get; set; }
        public CmnSpecialBranchUnit cmnSpecialBranchUnit { get; set; }
        [MaxLength(256)]
        public string bomName { get; set; }
        [MaxLength(256)]
        public string bomDescription { get; set; }
        //[ForeignKey("productWiseSpecificationId")]
        public int?  bomProductWiseSpecificationId { get; set; }
        public InvProductWiseSpecification bomProductWiseSpecification { get; set; }
        public decimal? bomQty { get; set; }
        public decimal? bomTotalCost { get; set; }
        public DateTime? bomDate { get; set; }
        public string bomType { get; set; }
        public string materialType { get; set; }
        public decimal? weightPerPack { get; set; }
        public int? WeightPerPackUOM { get; set; }   
        public decimal batchWeight { get; set; }
        public int? batchWeightUOMId { get; set; }
        public int? phGroupMasterId { get; set; }
        public int  shelfLife { get; set; }
        public decimal? packSizeForPM { get; set; }
    }
}
