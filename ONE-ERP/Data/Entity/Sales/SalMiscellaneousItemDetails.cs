using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Sales
{
    public class SalMiscellaneousItemDetails : NewBase // Factory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? miscellaneousItemDetailsId { get; set; }


        public int? miscellaneousItemId { get; set; }
        public SalMiscellaneousItem miscellaneousItem { get; set; }

        public int? productSpecificationId { get; set; }
        public decimal? ctnQty { get; set; }
        public decimal? looseQty { get; set; }
        public decimal? price { get; set; }


        [MaxLength(500)]
        public string remarks { get; set; }
        [MaxLength(50)]
        public string batchNo { get; set; }
        public DateTime? mgfDate { get; set; }
        public DateTime? expireDate { get; set; }
    }
    public class SalMiscellaneousItemFile : NewBase//Factory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? miscellaneousItemFileId { get; set; }
        public int? miscellaneousItemId { get; set; }
        public SalMiscellaneousItem miscellaneousItem { get; set; }
        [MaxLength(300)]
        public string docInfo { get; set; }
        [MaxLength(300)]
        public string filePath { get; set; }
    }
    public class SalMiscellaneousItemDetailsDepot : NewBase//Depot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? miscellaneousItemDetailsId { get; set; }


        public int? miscellaneousItemId { get; set; }
        public SalMiscellaneousItemDepot miscellaneousItem { get; set; }

        public int? productSpecificationId { get; set; }
        public decimal? ctnQty { get; set; }
        public decimal? looseQty { get; set; }
        public decimal? price { get; set; }

        [MaxLength(500)]
        public string remarks { get; set; }
        [MaxLength(50)]
        public string batchNo { get; set; }
        public DateTime? mgfDate { get; set; }
        public DateTime? expireDate { get; set; }
    }
    public class SalMiscellaneousItemFileDepot : NewBase//Depot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? miscellaneousItemFileId { get; set; }
        public int? miscellaneousItemId { get; set; }
        public SalMiscellaneousItemDepot miscellaneousItem { get; set; }
        [MaxLength(300)]
        public string docInfo { get; set; }
        [MaxLength(300)]
        public string filePath { get; set; }
    }
}