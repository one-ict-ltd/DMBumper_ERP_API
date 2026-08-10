using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class SalesProductExpireReturnMasterViewModel
    {
        public int productExpireReturnMasterId { get; set; }
        public int productExpireReturnId { get; set; }
        public int partyId { get; set; }
        public string expireReturnNumber { get; set; }
        public DateTime? returnDate { get; set; }
        public decimal? grandTotal { get; set; }
        public List<SalesProductExpireReturnViewModel> lstDetailsViewModel { get; set; }
    }

    public class SalesProductExpireReturnViewModel
    {
        public int? expireReturnDetailsId { get; set; }
        public int? productExpireReturnId { get; set; }
        public string expireReturnNumber { get; set; }
        public DateTime? returnDate { get; set; }
        //public int? productExpireReturnMasterId { get; set; }
        public int? salesInvoiceId { get; set; }
        public int? partyId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public decimal? returnQty { get; set; }
        public decimal? amount { get; set; }
        public decimal? returnPrice { get; set; }
        public string batchNo { get; set; }
        public DateTime? mgfDate { get; set; }
        public DateTime? expireDate { get; set; }
    }
}
