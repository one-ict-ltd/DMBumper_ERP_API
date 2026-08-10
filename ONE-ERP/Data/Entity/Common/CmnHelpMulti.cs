using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Common
{
    public class CmnHelpMulti:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int multiId { get; set; }
        public int? helpId { get; set; }
        public CmnHelpMaster helpMaster { get; set; }
        public int? selectedId { get; set; }
    }
}
