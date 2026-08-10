using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Common
{
    public class CmnDistricts:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int districtsId { get; set; }
        [MaxLength(50)]
        public string districtCode { get; set; }
        [MaxLength(250)]
        public string districtName { get; set; }
        [MaxLength(100)]
        public string shortName { get; set; }
        public int? divisionsId { get; set; }
        public CmnDivisions divisions { get; set; }
    }
}
