using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmTravellerInfo:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int trainingResourcePersonId { get; set; }
        public int? travelMasterId { get; set; }
        public int? employeeID { get; set; }
    }
}
