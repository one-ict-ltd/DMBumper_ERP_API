using ONEERP.Data.Entity.HRM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Common
{
    public class CmnReportPermission:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int reportPermissionId { get; set; }
        public int? reportId { get; set; }
        public CmnReport report { get; set; }        
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        
       
    }
}
