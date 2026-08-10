using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmLeaveYear:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int leaveYearId { get; set; }
        [MaxLength(250)]
        public string yearName { get; set; }
        [MaxLength(100)]
        public string aliasName { get; set; }
    }
}
