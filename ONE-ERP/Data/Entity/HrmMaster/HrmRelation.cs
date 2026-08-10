using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HrmMaster
{
    public class HrmRelation:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int relationId { get; set; }
        [MaxLength(250)]
        public string relationName { get; set; }
        [MaxLength(250)]
        public string relationShortName { get; set; }
    }
}
