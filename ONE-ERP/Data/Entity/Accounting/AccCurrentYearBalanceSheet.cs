using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccCurrentYearBalanceSheet:Base
    {
        [MaxLength(100)]
        public string natureName { get; set; }
        [MaxLength(300)]
        public string groupName { get; set; }
        public int? ledgerId { get; set; }
        [MaxLength(400)]
        public string accountName { get; set; }        
        public decimal? currentBSheetAmount { get; set; }
        public decimal? previousBSheetAmount { get; set; }
    }
}
