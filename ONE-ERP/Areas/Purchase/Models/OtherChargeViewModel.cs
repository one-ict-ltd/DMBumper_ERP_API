using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class OtherChargeViewModel
    {

        public int ImpOtherChargeId { get; set; }
        public int ImpLCInfoMasterId { get; set; }
        public int CustomsDutyOthersCharge { get; set; }
        public int ClearingCNFCharge { get; set; }
        public int LoadingUnloadingCharge { get; set; }
        public int CarringCharge { get; set; }
        public int OthersCharge { get; set; }
        public int OthersCharge2 { get; set; }
        public DateTime CustomsDutyOthersChargeDate { get; set; }
        public DateTime ClearingCNFChargeDate { get; set; }
        public DateTime LoadingUnloadingChargeDate { get; set; }
        public DateTime CarringChargeDate { get; set; }
        public string remarks { get; set; }

        //    ImpOtherChargeId:0,
        //CustomsDutyOthersCharge:0,
        //CustomsDutyOthersChargeDate:new Date(),
        //ClearingCNFCharge:0,
        //ClearingCNFChargeDate:new Date(),
        //LoadingUnloadingCharge:0,
        //LoadingUnloadingChargeDate:new Date(),
        //CarringCharge:0,
        //CarringChargeDate:new Date(),
        //OthersCharge:0,	
        //OthersCharge2:0,
        //remarks:"",

    }
}
