using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnExamContent:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CmnExamContentID { get; set; }
        public string fileName { get; set; }
        public string description { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? endDate { get; set; }
    }
}
