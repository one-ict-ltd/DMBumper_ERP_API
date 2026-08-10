namespace ONEERP.Areas.Sales.Models
{
    public class SalesRemittanceSlipViewModel
    {
        public int remittanceSlipId { get; set; }
        public string resourceUrl { get; set; }
        public int? remittanceId { get; set; }
        public string fileString { get; set; }
        public string fileName { get; set; }
        public string ext { get; set; }
    }
}