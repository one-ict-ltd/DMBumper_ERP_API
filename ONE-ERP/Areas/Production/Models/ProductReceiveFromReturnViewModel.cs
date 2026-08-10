using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Production.Models
{
    public class ProductReceiveFromReturnViewModel
    {
        public int ProductReceiveFromReturnMasterId { get; set; }
        public DateTime ProductReceiveFromReturnDate { get; set; }
        public string TypeofReceive { get; set; }
        public int? ProductReturnMasterId { get; set; }
        public int? status { get; set; }
        public string remarks { get; set; }
        public int? bomForId { get; set; }

        public List<ProductReceiveFromReturnDetailViewModel> lstDetailsViewModel { get; set; }
    }
    public class ProductReceiveFromReturnDetailViewModel
    {
        public int ProductReceiveFromReturnDetailId { get; set; }
        public int productReturnDetailId { get; set; }
        public int? ProductReceiveFromReturnMasterId { get; set; }
        public int? ProductIssueDetailId { get; set; }
        public decimal? potency { get; set; }
        public string grnNo { get; set; }
        public decimal? receivedQty { get; set; }
        public int? grnDetailsId { get; set; }
    }
}
