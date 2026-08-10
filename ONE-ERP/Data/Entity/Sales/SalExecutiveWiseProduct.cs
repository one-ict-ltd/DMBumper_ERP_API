using ONEERP.Data.Entity.HRM;
using ONEERP.Data.Entity.Inventory;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ONEERP.Data.Entity.Sales
{
    public class SalExecutiveWiseProduct : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int executiveWiseProductId { get; set; }
        public int employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public int productId { get; set; }
        public InvProduct product { get; set; }
        public DateTime? effectiveFromDate { get; set; }
        public DateTime? effectiveToDate { get; set; }
    }
}
