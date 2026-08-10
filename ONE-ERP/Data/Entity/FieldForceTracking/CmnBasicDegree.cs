using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnBasicDegree:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BasicDegreeID { get; set; }
        public string BasicDegreeName { get; set; }
        public string BasicDegreeShotName { get; set; }
        public int? BasicDegreeSortOrder { get; set; }
    }
}
