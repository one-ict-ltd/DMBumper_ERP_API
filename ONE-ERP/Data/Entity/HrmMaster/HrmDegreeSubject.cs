using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HrmMaster
{
    public class HrmDegreeSubject : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int degreeSubjectId { get; set; }
        public int? degreeId { get; set; }
        public HrmDegree degree { get; set; }
        public int? subjectId { get; set; }
        public HrmEducationalSubject subject { get; set; }

        //[MaxLength(250)]
        //public string Name { get; set; }
        //[MaxLength(250)]
        //public string ShortName { get; set; }
    }
}
