using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Production
{
    public class PrdBatchType:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int batchTypeId { get; set; }
        public string batchTypeName { get; set; }
        public string batchTypeCode { get; set; }
        public int? sortOrder { get; set; } 
    }
}
