namespace ONEERP.Areas.MasterData.Models
{
    public class StoreViewModel
    {
        public int? storeId { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        public string storeName { get; set; }
        public string storeCode { get; set; }        
        public bool? isActive { get; set; }

    }
}
