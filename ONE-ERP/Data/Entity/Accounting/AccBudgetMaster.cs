using ONEERP.Data.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccBudgetMaster:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? budgetMasterId { get; set; }
        public int? fiscalYearId { get; set; }
        public AccFiscalYear fiscalYear { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
        public int? sbuId { get; set; }
        public CmnSpecialBranchUnit sbu { get; set; }
        [MaxLength(250)]
        public string budgetNo { get; set; }
        public DateTime budgetDate { get; set; }
        public decimal? grandTotal { get; set; }
        public int? status { get; set; }
    }
}
