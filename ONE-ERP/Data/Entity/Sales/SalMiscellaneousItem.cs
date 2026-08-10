using ONEERP.Data.Entity.Common;
using ONEERP.Data.Entity.Inventory;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Sales
{
    public class SalMiscellaneousItem : NewBase//Factory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int miscellaneousItemId { get; set; }

        public DateTime? itemDate { get; set; }
        public string miscellaneousNo { get; set; }
        public int? miscellaneousTypeId { get; set; }

        public int? fromSbuId { get; set; }
        public CmnSpecialBranchUnit fromSbu { get; set; }

        public int? sbuId { get; set; }
        public CmnSpecialBranchUnit sbu { get; set; }

        [MaxLength(500)]
        public string remarks { get; set; }
    }

    public class SalMiscellaneousItemDepot : NewBase//Depot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int miscellaneousItemId { get; set; }

        public DateTime? itemDate { get; set; }
        public string miscellaneousNo { get; set; }
        public int? miscellaneousTypeId { get; set; }

        public int? fromSbuId { get; set; }
        public CmnSpecialBranchUnit fromSbu { get; set; }

        public int? sbuId { get; set; }
        public CmnSpecialBranchUnit sbu { get; set; }

        [MaxLength(500)]
        public string remarks { get; set; }

        public int? RePackProductTransferId { get; set; }
        public int? isApproved { set;get; }
        public InvRePackProductTransferMaster rePackProductTransferMaster { get; set; }
    }
}