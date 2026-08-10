using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnExamQuestionOption : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CmnExamQuestionOptionID { get; set; }
        public int? CmnExamQuestionId { get; set; }
        public CmnExamQuestion CmnExamQuestion { get; set; }
        public string option { get; set; }
        public bool? iscorrect { get; set; }
    }
}
