using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Purchase.Models
{
    public class SampleCollectionViewModel
    {
        public int scMasterId { get; set; }
        public string scMasterNo { get; set; }
        public DateTime? scDate { get; set; }
        public int? grnMasterId { get; set; }
        public int? sampleStatus { get; set; }
        public string remarks { get; set; }
        public List<SampleCollectionDetailsViewModel> lstDetailViewModel { get; set; }
    }
    

    public class SampleCollectionDetailsViewModel
    {
        public int scDetailsId { get; set; }
        public int? scMasterId { get; set; }
        public int? grnDetailsId { get; set; }
        public decimal? sampleQty { get; set; }
        public int? status { get; set; }
    }
}
