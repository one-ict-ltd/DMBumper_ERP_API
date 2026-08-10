using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmTravelMaster:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int travelMasterId { get; set; }
        public int? employeeID { get; set; }
        public string travelNumber { get; set; }
        public DateTime date { get; set; }
        public string accountNumber { get; set; }
        public int? travelProjectId { get; set; }
        public int? hRActivityId { get; set; }
        public int? hRDonerId { get; set; }
        public string purpose { get; set; }
        public int? status { get; set; }
    }
}
