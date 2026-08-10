using ONEERP.Data.Entity.Inventory;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccFormulaType:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int formulaTypeId { get; set; }             
        [MaxLength(100)]
        public string formulaName { get; set; }
        [MaxLength(50)]
        public string formulaShortName { get; set; }
    }
}
