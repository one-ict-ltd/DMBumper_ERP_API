using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccGroupNature:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int groupNatureId { get; set; }
        public AccGroupNature groupNature { get; set; }
        [MaxLength(250)]
        public string natureName { get; set; }
        public int? printOrder { get; set; }
    }
}
