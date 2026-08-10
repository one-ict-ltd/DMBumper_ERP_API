using ONEERP.Data.Entity.Common;
using ONEERP.Data.Entity.HRM;
using ONEERP.Data.Entity.Salary.MasterData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Salary.SalaryProcess
{
    public class SalaryEmployeeProcessMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeeProcessMasterId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public int? salaryPeriodId { get; set; }
        public SalaryPeriod salaryPeriod { get; set; }
        public decimal? netPayable { get; set; }
        public decimal? walletPayable { get; set; }
        public decimal? bankPayable { get; set; }
        public decimal? cashPayable { get; set; }
        [MaxLength(200)]
        public string bankName { get; set; }
        [MaxLength(50)]
        public string bankAccountNo { get; set; }
        [MaxLength(150)]
        public string walletName { get; set; }
        [MaxLength(50)]
        public string walletNo { get; set; }
        [MaxLength(250)]
        public string division { get; set; }
        [MaxLength(250)]
        public string department { get; set; }
        [MaxLength(250)]
        public string designation { get; set; }
        [MaxLength(50)]
        public string companyBankAccNo { get; set; }
        [MaxLength(200)]
        public string companyBankName { get; set; }
        [MaxLength(150)]
        public string companyBankBranchName { get; set; }
        [MaxLength(500)]
        public string companyBankAddress { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
        public int? sbuId { get; set; }
        public CmnSpecialBranchUnit sbu { get; set; }
    }
}
