using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class SalesCollectionDetailsViewModel
    {
        public int? collectionDetailId { get; set; }
        public int? collectionMasterId { get; set; }
        public int? paymentModeId { get; set; }
        public decimal? collectionAmount { get; set; }
        public string bankName { get; set; }
        public string chequeNo { get; set; }
        public string trxNo { get; set; }
        public bool isActive { get; set; }
    }
}
