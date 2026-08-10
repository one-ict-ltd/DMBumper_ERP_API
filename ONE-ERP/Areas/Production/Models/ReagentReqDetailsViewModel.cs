namespace ONEERP.Areas.Production.Models
{
    public class ReagentReqDetailsViewModel
    {
        public int? prodTrnfrDetailsId { get; set; }
        public int? prodTrnfrId { get; set; }
        public decimal? transferQty { get; set; }

        public int? reagentReqDetailsId { get; set; }
        public int? reagentReqId { get; set; }
        public int? productId { get; set; }
        public int? fromStoreId { get; set; }
        public int? productWiseSpecificationId { get; set; }
        public decimal? reqQty { get; set; }
        public decimal? price { get; set; }
        public bool? isActive { get; set; }
        public bool? isSelect { get; set; }
        public string batchNo { get; set; }
    }
}
