using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccChequeBookDetails:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int chequeBookDetailsId { get; set; }
        public int? chequeBookMasterId { get; set; }
        public AccChequeBookMaster chequeBookMaster { get; set; }
        public int? voucherDetailsId { get; set; }
        public AccVoucherDetails voucherDetails { get; set; }
    }
}
