using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmLoanLogHistory : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int loanLogHistoryId { get; set; }
        public int? loanEntryId { get; set; }
        public HrmLoanEntry loanEntry { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public DateTime? PayDate { get; set; }
        public DateTime? AdjustmentDate { get; set; }
        public int? isPaid { get; set; }
        public decimal? OpeningAmount { get; set; }
        public decimal? PrincipalAmount { get; set; }
        public decimal? interestAmount { get; set; }
        public decimal? Amounttobepaid { get; set; }
        public decimal? cumulativePrincipal { get; set; }
        public decimal? cumulativeInterest { get; set; }
        public decimal? principalBalance { get; set; }
    }
}
