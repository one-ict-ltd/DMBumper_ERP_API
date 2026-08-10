using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Common
{
    public class CmnOriginCountry:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int countryId { get; set; }
        [MaxLength(50)]
        public string countryNo { get; set; }
        [MaxLength(250)]
        public string countryName { get; set; }
        [MaxLength(50)]
        public string countryShortName { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
    }
}
