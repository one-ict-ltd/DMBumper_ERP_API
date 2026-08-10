using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ONEERP.Data.Entity.HrmMaster;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmEmployeeFamilyInfo:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int familyInfoId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public int? relationId { get; set; }
        public HrmRelation relation { get; set; }
        public DateTime? dob { get; set; }
        public string name { get; set; }
        public string occupation { get; set; }
        public string mobile { get; set; }
        public string NID { get; set; }
        public string passport { get; set; }
        public string email { get; set; }
        public string remarks { get; set; }
    }
}
