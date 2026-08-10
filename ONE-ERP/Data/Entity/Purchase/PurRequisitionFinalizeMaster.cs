using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurRequisitionFinalizeMaster:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int requisitionFinalizeMasterId { get; set; }

        public string requisitionFinalizeNo { get; set; }
        public DateTime? requisitionFinalizeDate { get; set; }

        public string remarks { get; set; }

        public int? status { get; set; }

    }
}
