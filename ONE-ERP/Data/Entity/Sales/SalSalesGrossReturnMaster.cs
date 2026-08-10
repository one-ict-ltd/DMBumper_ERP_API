using ONEERP.Data.Entity.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Sales
{
    public class SalSalesGrossReturnMaster:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int salesGrossRetunMasterId { get; set; }
        public string grossReturnNumber { get; set; }
        public DateTime? grossReturnDate { get; set; }
        public int? partyId { get; set; }
        public AccParty party { get; set; }
        public decimal? totalAmount { get; set; } 
    }
}
