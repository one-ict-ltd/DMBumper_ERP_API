using System;

namespace ONEERP.Areas.Accounting.Models
{
    public class FiscalYearViewModel
    {        
        public int? fiscalYearId { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        public string yearName { get; set; }
        public string financialYearName { get; set; }
        public DateTime yearStartDate { get; set; }
        public DateTime yearEndDate { get; set; }
        public DateTime lockDate { get; set; }        
        public bool? isActive { get; set; }
        public bool? islocked { get; set; }
    }
}
