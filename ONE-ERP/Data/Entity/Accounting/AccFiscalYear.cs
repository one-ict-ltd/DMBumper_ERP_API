using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccFiscalYear:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fiscalYearId { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
        public int? sbuId { get; set; }
        public CmnSpecialBranchUnit sbu { get; set; }
        [MaxLength(50)]
        public string yearName { get; set; }
        [MaxLength(50)]
        public string financialYearName { get; set; }
        public DateTime? yearStartDate { get; set; }
        public DateTime? yearEndDate { get; set; }
        public DateTime? lockDate { get; set; }
        public bool? islocked { get; set; }
    }
}
