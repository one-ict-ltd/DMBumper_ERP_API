using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Common
{
    public class CmnTransactionType:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int transactionTypeId { get; set; }
        
        [MaxLength(250)]
        public string transactionTypeName { get; set; }
        [MaxLength(50)]
        public string trTypeShortName { get; set; }
    }
}
