using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmTravelRoute:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int travelRouteId { get; set; }
        public int? travelMasterId { get; set; }
        public int? employeeId { get; set; }
        public int? routeOrder { get; set; }
    }
}
