using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Common
{
    public class CmnDivisions:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int divisionsId { get; set; }
        [MaxLength(50)]
        public string divisionCode { get; set; }
        [MaxLength(250)]
        public string divisionName { get; set; }
        [MaxLength(100)]
        public string shortName { get; set; }
        public int? countryId { get; set; }
        public CmnOriginCountry originCountry { get; set; }

    }
}
