using ONEERP.Data.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvStockMaster:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int stockMasterId { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
        public int? sbuId { get; set; }
        public CmnSpecialBranchUnit sbu { get; set; }
        public int? storeId { get; set; }
        public CmnStore store { get; set; }
        public int? stockCategoryId { get; set; }
        public InvStockCategory stockCategory { get; set; }
        [MaxLength(100)]
        public string stockNo { get; set; }
        public DateTime? stockDate { get; set; }
        public int? stockTypeId { get; set; }
        public InvStockType stockType { get; set; }
        public string remarks { get; set; }
        public int? transactionMasterId { get; set; }
        
    }
}
