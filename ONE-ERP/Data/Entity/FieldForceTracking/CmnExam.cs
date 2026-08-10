using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnExam:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CmnExamID { get; set; }
        public string name { get; set; }
        public DateTime? date { get; set; }
        public DateTime? lastSubmitDate { get; set; }
        public int? time { get; set; }
        public int? CmnExamContentId { get; set; }
        public CmnExamContent CmnExamContent { get; set; }
        public int? totalMarks { get; set; }
    }
}
