using ONEERP.Data.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnDepot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepotID { get; set; }
        [MaxLength(50)]
        public string ZoneCode { get; set; }
        [MaxLength(50)]
        public string RegionCode { get; set; }
        [MaxLength(50)]
        public string DepotCode { get; set; }
        [MaxLength(50)]
        public string DepotName { get; set; }
        public string DepotAddress { get; set; }
        public int? CompanyId { get; set; }
        public bool? IsActive { get; set; }
        public int? sortOrder { get; set; }
        public int? depotTypeId { get; set; }
        public CmnStoreType depotType { get; set; }
    }
}
