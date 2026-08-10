using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class SalRemittanceViewModel
    {
        public int remittanceId { get; set; }

        public DateTime? remittanceDate { get; set; }
        public int remittanceNo { get; set; }
        public int? remittanceTypeId { get; set; }

       
        public string oplTranNo { get; set; }


        //public string depotCode { get; set; }

        //public int? companyId { get; set; }
       
        //public DateTime? depositDate { get; set; }
        
        //public int? bankBranchId { get; set; }
       

       
        //public string depositRefNo { get; set; }

        //public decimal? depositAmount { get; set; }

       
    }
}
