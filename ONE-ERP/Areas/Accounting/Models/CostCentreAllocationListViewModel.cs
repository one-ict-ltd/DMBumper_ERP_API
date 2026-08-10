namespace ONEERP.Areas.Accounting.Models
{
    public class CostCentreAllocationListViewModel
    {
        public int? costCentreAllocationId { get; set; }
        public int? costCentreId { get; set; }
        public int? voucherMasterId { get; set; }
        public int? voucherDetailId { get; set; }
        public string costCentreName { get; set; }
        public decimal amount { get; set; }
        public int? isActive { get; set; }

    }
}
