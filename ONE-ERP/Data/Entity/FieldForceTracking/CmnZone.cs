using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnZone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ZoneID { get; set; }
        [MaxLength(50)]
        public string ZoneCode { get; set; }
        [MaxLength(50)]
        public string ZoneName { get; set; }
        public int? CompanyId { get; set; }
        public bool? IsActive { get; set; }
        public int? sortOrder { get; set; }
        [MaxLength(50)]
        public string SquareRefCode { get; set; }
    }
}
