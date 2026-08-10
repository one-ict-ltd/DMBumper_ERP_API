using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnRxUpload
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int rxUploadID { get; set; }

        public int? CmnRxUploadMasterId { get; set; }
        public CmnRxUploadMaster CmnRxUploadMaster { get; set; }

        public DateTime? date { get; set; }

        public string imageUrl { get; set; }
    }
}
