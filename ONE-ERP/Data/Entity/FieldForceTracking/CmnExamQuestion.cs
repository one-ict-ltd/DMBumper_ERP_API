using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnExamQuestion:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CmnExamQuestionID { get; set; }
        public string question { get; set; }
        public int? marks { get; set; }
        public int? CmnExamId { get; set; }
        public CmnExam CmnExam { get; set; }
    }
}
