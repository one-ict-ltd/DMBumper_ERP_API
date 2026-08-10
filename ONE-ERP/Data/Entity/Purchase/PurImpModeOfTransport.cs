using ONEERP.Data.Entity.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurImpModeOfTransport:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImpModeOfTransportId { get; set; }
        public string modeOfTransportName { get; set; }
        public string modeOfTransportCode { get; set; }
        public string modeOfTransportShortName { get; set; }
        public int? sortOrder { get; set; }
    }
}
