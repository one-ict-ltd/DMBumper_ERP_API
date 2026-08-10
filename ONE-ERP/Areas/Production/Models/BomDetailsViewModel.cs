namespace ONEERP.Areas.Production.Models
{
    public class BomDetailsViewModel
    {
        public int bomDetailsId { get; set; }
        public int bomId { get; set; }
        public int bomDetailsProductWiseSpecificationId { get; set; }
        public decimal qty { get; set; }
        public decimal price { get; set; }
        public decimal wastage { get; set; }
        public decimal totalQty { get; set; }
        public decimal totalPrice { get; set; }
        public bool isActive { get; set; }
        public bool isSelect { get; set; }
    }
}
