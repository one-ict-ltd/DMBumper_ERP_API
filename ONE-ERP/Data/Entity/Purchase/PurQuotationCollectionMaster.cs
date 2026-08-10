using ONEERP.Data.Entity.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurQuotationCollectionMaster:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int quotationCollectionMasterId { get; set; }

        public string quotationCollectionMasterNo { get; set; }
        public DateTime? quotationCollectionMasterDate { get; set; }

        public int? PurRequisitionFinalizeDetailId { get; set; }
        public PurRequisitionFinalizeDetail PurRequisitionFinalizeDetail { get; set; }

        public int? quotationTypeId { get; set; } // For Local Puorchase or Import 

        public int? status { get; set; }

        public string remarks { get; set; }


    }
}
