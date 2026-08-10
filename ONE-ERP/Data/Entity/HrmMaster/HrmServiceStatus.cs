using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HrmMaster
{
    public class HrmServiceStatus:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int serviceStatusId { get; set; }
        [MaxLength(250)]
        public string statusName { get; set; }
        [MaxLength(250)]
        public string statusShortName { get; set; }
    }
}
