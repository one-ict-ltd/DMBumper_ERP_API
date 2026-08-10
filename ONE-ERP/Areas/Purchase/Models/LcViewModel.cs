using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class LcViewModel
    {
        public int ImpLCInfoMasterId { get; set; }
        public int ImpPreLCInfoMasterId { get; set; }
        public int MyProperty { get; set; }
        public string lcStatus { get; set; }
        public DateTime sortedDate { get; set; }
        public DateTime typedDate { get; set; }
        public DateTime appliedOnDate { get; set; }
        public DateTime signDate { get; set; }
        public DateTime amndCopyDate { get; set; }
        public DateTime faxedOnDate { get; set; }
        public DateTime mailReqRcvDate { get; set; }
        public DateTime lcOpenDate { get; set; }
        public DateTime bankSubDate { get; set; }
        public DateTime validityDate { get; set; }
        public DateTime exshiptDate { get; set; }
        public DateTime expireDate { get; set; }
        public DateTime remindDate { get; set; }
        public string lcNo { get; set; }
        public string lcaNo { get; set; }
        public int? bankId { get; set; }
        public int? adviceBankId { get; set; }
        public int? loadingPortId { get; set; }
        public int? destinatinPortId { get; set; }
        public int? totalLcAmount { get; set; }
        public int? frightAmount { get; set; }
        public int? countryOriginId { get; set; }
        public string shiptDay { get; set; }
        public string remarks { get; set; }
        public string lcNegotiation { get; set; }
       


    }
}
