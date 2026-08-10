using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.PromoInventory
{
    public class PromoRequisitionUploadMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int promoRequisitionId { get; set; }
        public DateTime? promoRequisitionDate { get; set; }
        public string promoRequisitionNo { get; set; }
        public string  refNo { get; set; }
        public string  remarks { get; set; }
        public string programName { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
        public string allocationType { get; set; }

    }
}
