using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnTAReceipt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TAReceiptID { get; set; }
        public string imageUrl { get; set; }
        public DateTime? date { get; set; }
        public int? CmnTADAForEmployeeId { get; set; }
        public CmnTADAForEmployee CmnTADAForEmployee { get; set; }
        public bool? isActive { get; set; }
        public int? CompanyId { get; set; }
    }
}
