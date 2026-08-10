using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HrmMaster
{
    public class HrmNomineeDetail:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int nomineeDetailId { get; set; }
        public int? nomineeFundId { get; set; }
        public int? nomineeId { get; set; }
        public decimal? percentence { get; set; }
    }
}
