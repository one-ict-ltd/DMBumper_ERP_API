using ONEERP.Data.Entity.Common;
using ONEERP.Data.Entity.Inventory;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ONEERP.Data.Entity.Production
{
    public class PrdProcessHead : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int processHeadId { get; set; }
        public string name{ get; set; }
        public string shortName{ get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public int? isQA { get; set; }
        public int? shortOrder { get; set; }
        //public int? companyId { get; set; }
        //public CmnCompany company { get; set; }

    }
}
