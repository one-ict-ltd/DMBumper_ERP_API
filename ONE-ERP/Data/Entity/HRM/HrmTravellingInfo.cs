using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmTravellingInfo:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int travellingInfoId { get; set; }
        public int? travelMasterId { get; set; }
        public string travellingFrom { get; set; }
        public string travellingTo { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? arrivalDate { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? arrivalTime { get; set; }
        public int? travelVehicleTypeId { get; set; }
        public string vehicleNumber { get; set; }
        public string driverName { get; set; }
        public string driverContactNumber { get; set; }
        public string accommodationDaaress { get; set; }
        public int? bookingRequird { get; set; }

    }
}
