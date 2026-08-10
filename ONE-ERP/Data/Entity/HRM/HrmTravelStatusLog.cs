using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmTravelStatusLog:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int travelStatusLogId { get; set; }
        public int? travelMasterId { get; set; }
        public int? employeeId { get; set; }
        public DateTime? date { get; set; }
        public string remarks { get; set; }
        public int? Status { get; set; }
    }
}
