using ONEERP.Data.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class AppsVersion:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppsVersionID { get; set; }
        public int? version { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
    }
}
