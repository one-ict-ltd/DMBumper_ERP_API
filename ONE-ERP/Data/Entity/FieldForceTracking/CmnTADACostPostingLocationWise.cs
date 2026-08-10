using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnTADACostPostingLocationWise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CmnTADACostPostingLocationWiseID { get; set; }

        public string postingLocation { get; set; }
        public string location { get; set; }

        public decimal? amount { get; set; }
    }
}
