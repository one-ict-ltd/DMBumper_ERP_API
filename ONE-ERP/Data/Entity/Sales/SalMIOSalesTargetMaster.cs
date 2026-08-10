using ONEERP.Data.Entity.HRM;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace ONEERP.Data.Entity.Sales
{
    public class SalMIOSalesTargetMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int targetMasterId { get; set; }
        public string depotCode { get; set; }
        public string territoryCode { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public int? month { get; set; }
        public int? year { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
    }

    public class SalMIOSalesTargetMasterYearly : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int targetMasterYearlyId { get; set; }
        public string depotCode { get; set; }
        public string territoryCode { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public int? year { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
    }
    public class SalMonthWiseBudgetPercent : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int monthWiseBudgetPercentId { get; set; }
        public int? monthNo { get; set; }
        public int? companyId { get; set; }
        public int? year { get; set; }
        public decimal? rate { get; set; }
    }
}
