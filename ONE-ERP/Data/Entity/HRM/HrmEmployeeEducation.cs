using ONEERP.Data.Entity.HrmMaster;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmEmployeeEducation : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int educationalQualificationId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public string institution { get; set; }
        [MaxLength(250)]
        public string majorGroup { get; set; }
        [MaxLength(250)]
        public string grade { get; set; }
        public int? passingYear { get; set; }
        public string certificateUrl { get; set; }

        public int? resultId { get; set; }
        public HrmResult result{get;set;}
        public int? degreeId { get; set; }
        public HrmDegree degree { get; set; }
        public int? educationOrganizationId { get; set; }
       public HrmEducationOrganization educationOrganization { get; set; }
        public int? degreesubjectId { get; set; }
        public HrmDegreeSubject degreeSubject { get; set; }
    }
}
