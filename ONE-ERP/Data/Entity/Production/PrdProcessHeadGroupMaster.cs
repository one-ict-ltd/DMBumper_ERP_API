using ONEERP.Data.Entity.Common;
using ONEERP.Data.Entity.Inventory;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ONEERP.Data.Entity.Production
{
    public class PrdProcessHeadGroupMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int phGroupMasterId { get; set; }
        public int? productionTypeId { get; set; } // 1= Menufacturing,2= Packing // Goes to PrdProcessHeadGroupDetails table
        public string groupName{ get; set; }
        public string shortName{ get; set; }
        public string remarks { get; set; }
        public int? shortOrder { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
    }
}
