using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvCurrentStock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int? storeId { get; set; }
        public int? ProductWiseSpecificationId { get; set; }
        public decimal? CurrentStock { get; set; }
        public string batchNo { get; set; }
        public DateTime? mgfDate { get; set; }
        public DateTime? expireDate { get; set; }
    }
}
