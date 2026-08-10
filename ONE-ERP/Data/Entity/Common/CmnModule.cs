using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Common
{
    public class CmnModule:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int moduleId { get; set; }
        [MaxLength(150)]
        public string moduleName { get; set; }
        [MaxLength(250)]
        public string description { get; set; }
        [MaxLength(300)]
        public string imageURl { get; set; }
        [MaxLength(300)]
        public string modulePath { get; set; }
        public int? sequence { get; set; }
    }
}
