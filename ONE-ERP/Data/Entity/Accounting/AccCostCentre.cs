using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccCostCentre:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? costCentreId { get; set; }
        [MaxLength(350)]
        public string costCentreName { get; set; }
        [MaxLength(100)]
        public string aliasName { get; set; }

        public int? AccCostCenterCategoryId { get; set; }
        public AccCostCenterCategory AccCostCenterCategory { get; set; }

        public int? AccCostCenterLocationId { get; set; }
        public AccCostCenterLocation AccCostCenterLocation { get; set; }
    }
}
