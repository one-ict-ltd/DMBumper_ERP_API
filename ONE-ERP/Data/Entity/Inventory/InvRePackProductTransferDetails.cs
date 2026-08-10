using ONEERP.Data.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvRePackProductTransferDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RePackProductTransferDetailId { get; set; }
        public int? RePackProductTransferId { get; set; }
        public int destructionNoteRecvDetailId { get; set; }     
        public int? productSpecificationId { get; set; }
        public InvProductWiseSpecification productSpecification { get; set; }
        public string batchNo { get; set; }
        public decimal? transferQty { get; set; }
    }
}
