using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Common
{
    public class CmnReport: NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int reportId { get; set; }
        public int? reportTypeId { get; set; }
        public CmnReportType reportType { get; set; }
        public int? moduleId { get; set; }
        public CmnModule module { get; set;}
        [MaxLength(250)]
        public string reportName { get; set; }        
    }
}
