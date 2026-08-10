using ONEERP.Data.Entity.Inventory;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ONEERP.Data.Entity.Production
{
    public class PrdProcessHeadGroupDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int phGroupDetailId { get; set; }

        public int? phGroupMasterId { get; set; }
        public PrdProcessHeadGroupMaster phGroupMaster { get; set; }
        public int? processHeadId { get; set; }
        public PrdProcessHead processHead { get; set; }
        public int? headOrder { get; set; }
        public int? isQA { get; set; }
        public int? hasQC { get; set; }
        public string remarks { get; set; }
        public int? productionTypeId { get; set; } // 1= Menufacturing,2= Packaging

    }
}
