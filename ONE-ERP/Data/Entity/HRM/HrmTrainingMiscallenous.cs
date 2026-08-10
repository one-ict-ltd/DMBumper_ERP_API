using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmTrainingMiscallenous:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int trainingMiscallenousId { get; set; }
        public int? trainingInfoId { get; set; }
        public string activityHead { get; set; }
        public string activityDetails { get; set; }
        public DateTime? startTimestamp { get; set; }
        public DateTime? endTimestamp { get; set; }
        public string costOfAction { get; set; }
    }
}
