namespace ONEERP.Areas.Accounting.Models
{
    public class AutoVoucherDetailViewModel
    {
        public int? autoVoucherDetailId { get; set; }
        public int? autoVoucherMasterId { get; set; }
        public int? transactionModeId { get; set; }
        public int? ledgerId { get; set; }
        public bool? isActive { get; set; }     
    }
}
