using ONEERP.Data.Entity.HRM;
using ONEERP.Data.Entity.Salary.MasterData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Salary.SalaryProcess
{    
    public class SalaryEmployeeCashSetup : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeeCashSetupId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }   
        public decimal? bankAmount { get; set; }
        public decimal? walletAmount { get; set; }
        public decimal? cashAmount { get; set; }
        [MaxLength(10)]
        public string defaultAccount { get; set; }
    }
}
