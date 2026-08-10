using ONEERP.Data.Entity.Inventory;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccCostSheetHead:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int costSheetHeadId { get; set; }
        public int? parentHeadId { get; set; }
        public AccCostSheetParentHead parentHead { get; set; }        
        [MaxLength(250)]
        public string costHeadName { get; set; }
        public string description { get; set; }
        public int? sortOrder { get; set; }
    }
}
