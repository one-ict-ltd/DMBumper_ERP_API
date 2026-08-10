using ONEERP.Data.Entity.HRM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnExamPerform : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CmnExamPerformID { get; set; }
        public int? HrmEmployeeId { get; set; }
        public HrmEmployee HrmEmployee { get; set; }
        public int? CmnExamQuestionId { get; set; }
        public CmnExamQuestion CmnExamQuestion { get; set; }
        public int? CmnExamQuestionOptionId { get; set; }
        public CmnExamQuestionOption CmnExamQuestionOption { get; set; }
        public int? marks { get; set; }

    }
}
