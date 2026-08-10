using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnDoctorChemistDeleteHistory:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoctorDeleteHistoryID { get; set; }

        public int? type { get; set; } // 1 for doctor 2 chemist 

        public string doctorCode { get; set; }
        public string chemistCode { get; set; }
        public int? status { get; set; } //for approval 1= entry 2 = recomandaded 3 = approved 4 = rejected 
    }
}
