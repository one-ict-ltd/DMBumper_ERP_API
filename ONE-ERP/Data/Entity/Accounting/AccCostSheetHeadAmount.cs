using ONEERP.Data.Entity.Inventory;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccCostSheetHeadAmount:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int costSheetHeadAmountId { get; set; }
        public int? costSheetHeadId { get; set; }
        public AccCostSheetHead costSheetHead { get; set; }
        public int? formulaTypeId { get; set; }
        public AccFormulaType formulaType { get; set; }
        public int? ledgerId { get; set; }
        public AccLedgers ledger { get; set; }        
               
        
    }
}
