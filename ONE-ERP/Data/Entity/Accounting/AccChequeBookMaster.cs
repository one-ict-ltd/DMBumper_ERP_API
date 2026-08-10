using ONEERP.Data.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccChequeBookMaster:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int chequeBookMasterId { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
        public int? sbuId { get; set; }
        public CmnSpecialBranchUnit sbu { get; set; }

        [MaxLength(50)]
        public string chequeBookId { get; set; }
        [MaxLength(250)]
        public string bankName { get; set; }
        [MaxLength(250)]
        public string accountName { get; set; }
        [MaxLength(250)]
        public string accountNumber { get; set; }
        public int? chequeNumberCurrent { get; set; }
        public int? chequeNumberStarting { get; set; }
        public DateTime? chequeDate { get; set; }
        public decimal? chequeAmount { get; set; }
        public bool? isAccountPayee { get; set; }
        public bool? isBearer { get; set; }
        public bool? isNonNegotiable { get; set; }
        public bool? isPayableOndateOnly { get; set; }
        public bool? isVoid { get; set; }
        public bool? isPrinted { get; set; }
        public bool? isCleared { get; set; }
        public bool? isWithoutDate { get; set; }
        

    }
}
