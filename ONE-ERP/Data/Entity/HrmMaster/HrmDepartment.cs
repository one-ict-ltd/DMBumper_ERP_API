using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HrmMaster
{
    public class HrmDepartment:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int departmentId { get; set; }
        [MaxLength(50)]
        public string deptCode { get; set; }
        [MaxLength(250)]
        public string deptName { get; set; }
        [MaxLength(250)]
        public string shortName { get; set; }
        public DateTime? startDate { get; set; }
    }
}
