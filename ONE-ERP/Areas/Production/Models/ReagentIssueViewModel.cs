using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Production.Models
{
    public class ReagentIssueViewModel
    {
        public int reagentIssueMasterId { get; set; }
        public string issueNo { get; set; }
        public DateTime issueDate { get; set; }
        public string typeOfIssue { get; set; }
        public int? requisitionId { get; set; }
        public int? storeId { get; set; }
        public decimal? issueQty { get; set; }
        public int? issueStatus { get; set; }
        public string issueRemarks { get; set; }
        public int? bomForId { get; set; }
        public List<ProductionIssueDetailViewModel> lstDetailsViewModel { get; set; }
    }
}
