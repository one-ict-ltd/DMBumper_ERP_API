using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnCheckInOuts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CheckInOutId { get; set; } 
        public string userId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime? DateTime { get; set; }
        public int? Flag { get; set; }
        public string Address { get; set; }
        public string Opinion { get; set; }
        [MaxLength(50)]
        public string Time { get; set; }
        public string PunchTime { get; set; }
        public bool? isHQ { get; set; }
        public bool? isEHQ { get; set; }
        public bool? isOS { get; set; }
        public bool? isOther { get; set; }
    }
}
