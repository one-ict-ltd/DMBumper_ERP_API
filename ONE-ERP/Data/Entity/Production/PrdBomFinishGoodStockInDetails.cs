using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Production
{
    public class PrdBomFinishGoodStockInDetails : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int bomStockInDetailsId { get; set; }
        public int? bomStockInId { get; set; }
        public PrdBomFinishGoodStockInMaster bomStockIn { get; set; }
        public int? bomId { get; set; }
        public PrdBomMaster bom { get; set; }
        public decimal? qty { get; set; }
    }
}
