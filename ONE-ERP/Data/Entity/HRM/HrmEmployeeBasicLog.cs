using ONEERP.Data.Entity.Common;
using ONEERP.Data.Entity.HrmMaster;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ONEERP.Data.Entity.HRM
{
    public class HrmEmployeeBasicLog : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeBasicLogId { get; set; }
        [MaxLength(30)]
        public int employeeId { get; set; }
        [MaxLength(250)]
        public string message { get; set; }
        public string status { get; set; }
    }
}
