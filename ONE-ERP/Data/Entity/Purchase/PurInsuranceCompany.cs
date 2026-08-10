using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Purchase
{
    public class PurInsuranceCompany : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int insuranceCompanyId { get; set; }
        public string insuranceCompanyName { get; set; }
        public string insuranceCompanyCode { get; set; }
        public string remarks { get; set; }
        public int? sortOrder { get; set; }
    }
}
