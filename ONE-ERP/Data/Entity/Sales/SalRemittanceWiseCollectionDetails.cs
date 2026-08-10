using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Sales
{
    public class SalRemittanceWiseCollection: NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int remittanceCollectionId { get; set; }
        public int? remittanceId { get; set; }
        public SalRemittance remittance { get; set; }

        public int? collectionMasterId { get; set; }
        public SalCollectionMaster collectionMaster { get; set; }

        public int? remittanceMasterId { get; set; }
        public SalRemittanceMaster remittanceMaster { get; set; }
    }
}
