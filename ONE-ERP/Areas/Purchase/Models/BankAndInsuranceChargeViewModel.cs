using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class BankAndInsuranceChargeViewModel
    {
        public int ImpBankInsuranceChargeMasterId { get; set; }
        public int ImpLCInfoMasterId { get; set; }
        public int insuranceAmount { get; set; }
        public int chargeTypeId { get; set; }
        public string chargeType { get; set; }
        public string documentNo { get; set; }
        public DateTime bankChargeDate { get; set; }
        public DateTime insuranceDate { get; set; }
        public string remarks { get; set; }
        public string insuranceCompany { get; set; }
        public string insuranceBranch { get; set; }
        public string insuranceNo { get; set; }
        public List<BankInsuranceChargeDetailsViewModel> lstReqDetailsViewModel { get; set; }
    }
}


//ImpBankInsuranceChargeMasterId: number;
//ImpLCInfoMasterId: number;
//chargeTypeId: number;
//chargeFlag: boolean;
//lstReqDetailsViewModel: any[];
//referenceSelected: { };
//preLcId: number;


//documentNo: string;
//bankChargeDate: Date;
//lcOpenDate: Date;
//remarks: string;
//lcAmount: number;
//insuranceNo: string,
//: Date;
//: number;

//: string;
//openBankName: string;