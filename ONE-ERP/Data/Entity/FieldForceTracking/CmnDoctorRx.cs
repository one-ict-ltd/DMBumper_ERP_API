using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnDoctorRx:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int doctorRxID { get; set; }
        public int? productId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public int? quantity { get; set; }
        public int DoctorID { get; set; }
    }
}
