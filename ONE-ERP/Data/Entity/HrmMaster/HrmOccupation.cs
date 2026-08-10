using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HrmMaster
{
    public class HrmOccupation:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int hrmOccupationId { get; set; }
        [MaxLength(250)]
        public string occupationName { get; set; }
        [MaxLength(50)]
        public string occupationShortName { get; set; }
    }
}
