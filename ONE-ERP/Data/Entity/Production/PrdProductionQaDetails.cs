using ONEERP.Data.Entity.Inventory;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Production
{
    public class PrdProductionQaDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productionQaDetailsId { get; set; }
        public int productionQaId { get; set; }
        public int? TestParameterId { get; set; }
        public string testName { get; set; }
        public decimal? value { get; set; }
        //public string uom { get; set; }
        public string result { get; set; }
        public string description { get; set; }
    }
}
