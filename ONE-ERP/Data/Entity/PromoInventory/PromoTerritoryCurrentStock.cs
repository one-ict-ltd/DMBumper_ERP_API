using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.PromoInventory
{
    public class PromoTerritoryCurrentStock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; } 
        public string territoryCode { get; set; }
        public int? ProductWiseSpecificationId { get; set; }
        public decimal? CurrentStock { get; set; } 
        
       
    }
}
