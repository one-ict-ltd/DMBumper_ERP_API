using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Production.Models
{
    public class ProductionReturnViewModel
    {
        public int productReturnMasterId { get; set; }
        public string returnNo { get; set; }
        public DateTime returnDate { get; set; }
        public string TypeofReturn { get; set; }
        public int? productIssueMasterId { get; set; }
        public int? Status { get; set; }
        public string remarks { get; set; }
        public int? bomForId { get; set; }

        public List<ProductionReturnDetailViewModel> lstDetailsViewModel { get; set; }
    }
    public class ProductionReturnDetailViewModel
    {
        public int? productReturnDetailId { get; set; }
        public int? productIssueDetailId { get; set; }
        public decimal? returnQty { get; set; }
        public decimal? potency { get; set; }
        public string grnNo { get; set; }
        public int? grnDetailsId { get; set; }
    }
}
