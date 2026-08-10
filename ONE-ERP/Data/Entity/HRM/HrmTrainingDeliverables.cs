using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmTrainingDeliverables:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int trainingDeliverablesId { get; set; }
        public int? trainingInfoId { get; set; }
        public string deliverable { get; set; }
        public string details { get; set; }
        public DateTime? dueDate { get; set; }
    }
}
