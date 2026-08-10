using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmTrainingExam:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int trainingExamId { get; set; }
        public string name { get; set; }
        public int? trainingInfoId { get; set; }
        public int? trainerId { get; set; }
        public DateTime? startTimestamp { get; set; }
        public DateTime? endTimestamp { get; set; }
        public string modeOfExam { get; set; }
        public string syllabus { get; set; }
        public string maxValue { get; set; }
        public string minValue { get; set; }
        public string unit { get; set; }
    }
}
