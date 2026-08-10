using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmTrainingInfoDetails:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int trainingInfoDetailsId { get; set; }
        public int? trainingInfoId { get; set; }
        public string name { get; set; }
        public string startTimestamp { get; set; }
        public string endTimestamp { get; set; }
        public int? trainerId { get; set; }
        public string content { get; set; }
    }
}
