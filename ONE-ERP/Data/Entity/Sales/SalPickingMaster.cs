using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Sales
{
    public class SalPickingMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int pickingMasterId { get; set; }

        public string pickingNo { get; set; }

        public DateTime? pickingDate { get; set; }

        public int? isDispatch { get; set; }

        public int? stockMasterId { get; set; }

        public string remarks { get; set; }
    }
}
