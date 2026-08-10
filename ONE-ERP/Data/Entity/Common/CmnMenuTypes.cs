using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Common
{
    public class CmnMenuTypes:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int menuTypeId { get; set; }
        [MaxLength(250)]
        public string menuTypeName { get; set; }
        [MaxLength(300)]
        public string description { get; set; }
    }
}
