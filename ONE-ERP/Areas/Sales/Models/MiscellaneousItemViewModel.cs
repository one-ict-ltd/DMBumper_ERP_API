using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Sales.Models
{
    public class MiscellaneousItemViewModel
    {
        public int miscellaneousItemId { get; set; }

        public DateTime? itemDate { get; set; }
        public string miscellaneousNo { get; set; }
        public int? miscellaneousTypeId { get; set; }
        public int? miscellaneousTypeName { get; set; }
        public int? sbuId { get; set; }
        public string remarks { get; set; }
        public int? RePackProductTransferId { get; set; }
        public List<MiscellaneousItemFileViewModel> lstFileAttachment { get; set; }
        public List<MiscellaneousItemDetailsViewModel> lstMiscellaneousDetailsViewModel { get; set; }
    }

    public class MiscellaneousItemApprovalViewModel
    {
        public int? miscellaneousItemId { get; set; }
        public bool? isSelect { get; set; }
        public int? approvalStatusValue { get; set; }
        public List<MiscellaneousItemApprovalViewModel> lstMasterViewModel { get; set; }
    }
}
