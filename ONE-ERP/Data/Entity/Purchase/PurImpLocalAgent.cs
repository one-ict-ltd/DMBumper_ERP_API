using ONEERP.Data.Entity.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurImpLocalAgent:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImpLocalAgentId { get; set; }
        public string localAgentName { get; set; }
        public string localAgentCode { get; set; }
        public string localAgentShortName { get; set; }
        public int? sortOrder { get; set; }
    }
}
