using ONEERP.Data.Entity.Inventory;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ONEERP.Data.Entity.Production
{
    public class PrdBomDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int bomDetailsId { get; set; }
        public int pendingbomDetailsId { get; set; }
        public int bomId { get; set; }
        public PrdBomMaster prdBomMaster { get; set; }
        public int bomDetailsProductWiseSpecificationId { get; set; }
        public InvProductWiseSpecification bomDetailsProductWiseSpecification { get; set; }
        public decimal qty { get; set; }
        public decimal price { get; set; }
        public decimal wastage { get; set; }
        public decimal totalQty { get; set; }
        public decimal totalPrice { get; set; }
        public string assay { get; set; }
        public int potencyEffect { get; set; }
        public int? bomForId { get; set; }
    }
}
