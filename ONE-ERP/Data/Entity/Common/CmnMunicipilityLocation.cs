using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HrmMaster
{
    public class CmnMunicipilityLocation : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MunicipilityLocationId { get; set; }
        [MaxLength(250)]
        public string locationName { get; set; }
        [MaxLength(50)]
        public string shortName { get; set; }
    }
}
