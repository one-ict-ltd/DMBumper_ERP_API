using ONEERP.Data.Entity.HrmMaster;
using ONEERP.Data.Entity.Salary.MasterData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.HRM
{
    public class SalarySlabDesignation : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int slabDesignationId { get; set; }

        public int? salarySlabId { get; set; }

        public SalarySlab salarySlab { get; set; }

        public int? hrmDesignationId { get; set; }

        public HrmDesignation hrmDesignation { get; set; }

    }
}
