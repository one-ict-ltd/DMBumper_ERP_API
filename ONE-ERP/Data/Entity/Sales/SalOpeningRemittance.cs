using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Sales
{
    public class SalOpeningRemittance : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int openingRemittanceId { get; set; }
        public string depotCode { get; set; }
        public DateTime? uptoDate { get; set; }
        public decimal amount { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
    }
}
