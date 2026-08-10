using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.PromoInventory
{
    public class PromoTerritoryDistributionMaster : NewBase
    {
        public int territoryDistributionMasterId { get; set; }
        public DateTime? Date { get; set; }
    }
}
