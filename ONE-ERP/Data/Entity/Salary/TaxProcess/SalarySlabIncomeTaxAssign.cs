using ONEERP.Data.Entity.HRM;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Salary.TaxProcess
{   
    public class SalarySlabIncomeTaxAssign : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int slabIncomeTaxAssignId { get; set; }
        public int? slabTypeId { get; set; }
        public SalarySlabType slabType { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
    }
}
