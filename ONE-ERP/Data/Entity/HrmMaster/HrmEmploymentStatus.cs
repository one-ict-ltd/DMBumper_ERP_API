using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HrmMaster
{
    public class HrmEmploymentStatus : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employmentStatusId { get; set; }
        [MaxLength(100)]
        public string employmentStatus { get; set; }
        [MaxLength(256)]
        public string EmploymentStatusBn { get; set; }
        [MaxLength(30)]
        public string shortName { get; set; }
    }
}
