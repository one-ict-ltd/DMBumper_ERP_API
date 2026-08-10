using ONEERP.Data.Entity.HRM;
using ONEERP.Data.Entity.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurUserWiseProductCategory: NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userProductTypeId { get; set; }
        public int productTypeId { get; set; }
        public InvProductType productType { get; set; }
        public int employeeId { get; set; }
        public HrmEmployee employee { get; set; }
    }
}
