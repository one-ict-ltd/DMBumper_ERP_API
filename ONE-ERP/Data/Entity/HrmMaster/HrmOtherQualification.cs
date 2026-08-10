using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HrmMaster
{
    public class HrmOtherQualification:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int otherQualificationId { get; set; }
        public int? employeeID { get; set; }
        public int? otherQualificationHeadId { get; set; }
        [MaxLength(250)]
        public string subject { get; set; }
        public int? resultId { get; set; }
        [MaxLength(250)]
        public string instituteName { get; set; }
        [MaxLength(250)]
        public string passingYear { get; set; }
        [MaxLength(250)]
        public string markGrade { get; set; }

    }
}
