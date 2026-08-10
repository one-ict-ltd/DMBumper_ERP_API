using ONEERP.Data.Entity.HrmMaster;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmEmployeeTraining : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int trainingId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public int? trainingTypeId { get; set; }
        [MaxLength(250)]
        public string trainingTitle { get; set; }
        public string institute { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        [MaxLength(250)]
        public string remarks { get; set; }
        [MaxLength(50)]
        public string country { get; set; }
    }
}
