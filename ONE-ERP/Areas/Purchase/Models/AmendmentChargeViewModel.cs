using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class AmendmentChargeViewModel
    {
        public int ImpLCAmendmentChargeId { get; set; }
        public int ImpLCAmendmentId { get; set; }
        public int amendmentAmount { get; set; }
        public DateTime amendmentChargeDate { get; set; }
        public string remarks { get; set; }

    }
}

//ImpLCAmendmentId: number;
//ImpLCAmendmentChargeId: number;
//amendmentAmount: number;
//amendmentChargeDate: Date;
//remarks: string;
