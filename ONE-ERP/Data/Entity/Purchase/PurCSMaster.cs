using ONEERP.Data.Entity.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurCSMaster:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int csMasterId { get; set; }

        public string csMasterNo { get; set; }
        public DateTime? csDate { get; set; }

        public int? PurQuotationCollectionMasterId { get; set; }
        public PurQuotationCollectionMaster PurQuotationCollectionMaster { get; set; }

        public int? status { get; set; }

        public string remarks { get; set; }
    }
}
