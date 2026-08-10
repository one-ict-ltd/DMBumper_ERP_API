using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmLoanInterestType : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int interestTypeId { get; set; }
        public string interestName { get; set; }
        public decimal? interestRate { get; set; }
        public int? shortOrder { get; set; }
    }
}
