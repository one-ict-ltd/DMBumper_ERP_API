using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnRxUploadMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int rxUploadMasterID { get; set; }

        public string userID { get; set; }

        public int? doctorId { get; set; }
        public CmnDoctor doctor { get; set; }

        public DateTime? date { get; set; }
    }
}
