using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Production
{
    public class PrdBomFor : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int bomForId { get; set; }
        [MaxLength(256)]
        public string bomForType { get; set; }
        [MaxLength(256)]
        public string bomForName { get; set; }
    }
}
