using ONEERP.Data.Entity.Accounting;
using ONEERP.Data.Entity.Inventory;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ONEERP.Data.Entity.Sales
{
    public class SalSalesProductExpireReturnMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productExpireReturnMasterId { get; set; }
        public string expireReturnNo { get; set; }
        public DateTime? returnDate { get; set; }

    }
}
