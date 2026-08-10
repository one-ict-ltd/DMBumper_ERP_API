using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Production.Models
{
    public class BatchReleaseViewModel
    {
        public List<BatchReleaseListViewModel> TransferDetailsList { get; set; }
    }
    public class BatchReleaseListViewModel
    {
        public int? productTransferId { get; set; }
        public string ReleaseStatus { get; set; }
        public string ReleaseRemarks { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? ReleaseBy { get; set; }
        public bool? isActive { get; set; }
    }

}
