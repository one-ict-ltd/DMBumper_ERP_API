using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnSuperStarItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SuperStarItemID { get; set; }

        public int? starItemSecId { get; set; }
        public InvProductWiseSpecification starItemSec { get; set; }
        public DateTime? effectiveFrom { get; set; }
        public DateTime? effectiveTo { get; set; }
        public bool? isActive { get; set; }
        [DefaultValue(0)]
        public bool? isDelete { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        [MaxLength(250)]
        public string createdBy { get; set; }
        [MaxLength(250)]
        public string updatedBy { get; set; }
    }
}
