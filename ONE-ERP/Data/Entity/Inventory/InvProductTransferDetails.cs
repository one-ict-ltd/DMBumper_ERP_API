using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvProductTransferDetails:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productTrnfrDetailsId { get; set; }
        public int? prodTrnfrId { get; set; }
        public InvProductTransfer productTransfer { get; set; }
        public int? productReqDetailsId { get; set; }
        public int? fromStoreId { get; set; }
        public int? productId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public decimal? transferQty { get; set; }
        public decimal? price { get; set; }
        public decimal? CntQty { get; set; }
        public decimal? looseQty { get; set; }
        public int? toUomId { get; set; }
        public string batchNo { get; set; }
    }
}
