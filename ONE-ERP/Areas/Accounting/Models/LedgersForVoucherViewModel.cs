namespace ONEERP.Areas.Accounting.Models
{
    public class LedgersForVoucherViewModel
    {
        public string accountCode { get; set; }
        public string accountName { get; set; }
        public int? ledgerTypeId { get; set; }
        public int? haveSubledger { get; set; }
        public int? ledgerId { get; set; }   
        public decimal? amount { get; set; }
    }
}
