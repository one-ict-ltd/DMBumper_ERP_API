using ONEERP.Data.Entity.HRM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnMessageInfo:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int messageInfoID { get; set; }
        public int? fromEmployeeId { get; set; }
        public HrmEmployee fromEmployee { get; set; }
        public int? toEmployeeId { get; set; }
        public HrmEmployee toEmployee { get; set; }
        public string msgTitle { get; set; }
        public string message { get; set; }
        public DateTime? date { get; set; }
        public int? isRead { get; set; }
    }
}
