using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnLocationWiseVehicleBill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CmnLocationWiseVehicleBillID { get; set; }

        public string postingLocation { get; set; }

        public decimal? vehicleBill { get; set; }
        public decimal? billWithoutvehicle { get; set; }
        public decimal? maintenanceCharge { get; set; }

        public bool? isActive { get; set; }
    }
}
