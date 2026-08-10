using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Attendance.Models
{
    public class AttandaceClarivicationViewModel
    {
        public int? attandanceClarificationId { get; set; }
        public DateTime? attandanceClarificationDate { get; set; }
        public string attandanceClarificationTime { get; set; }
        public string narration { get; set; }
        public int? attandanceClarificationTypeId { get; set; }
        public bool? isApproved { get; set; }
    }
}
