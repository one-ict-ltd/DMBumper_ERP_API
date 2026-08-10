using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class ClearanceViewModel
    {
        public int? ImpClearenceInfoId { get; set; }
        public int? lcMasterId { get; set; }
        public int? remainderDays { get; set; }
        public string type { get; set; }
        public string cnfAgent { get; set; }
        public string remarks { get; set; }
        public DateTime DocRecvDate { get; set; }
        public DateTime actBankClrDt { get; set; }
        public DateTime expCustomeClrDt { get; set; }

        //global TAX
        public decimal? gtaxFP { get; set; }
        public decimal? gtaxCV { get; set; }
        public decimal? gtaxSCV { get; set; }
        public decimal? gtaxDF { get; set; }
        public decimal? gtaxITC { get; set; }
        public decimal? gtaxDFV { get; set; }
        public decimal? gtaxCSF { get; set; }


        //ITEM TAX
        public decimal? itaxCD { get; set; }
        public decimal? itaxRD { get; set; }
        public decimal? itaxSD { get; set; }
        public decimal? itaxVAT { get; set; }
        public decimal? itaxAIT { get; set; }
        public decimal? itaxAT { get; set; }
        public decimal? itaxATV { get; set; }
    }
}
     