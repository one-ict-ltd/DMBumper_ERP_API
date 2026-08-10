using ONEERP.Data.Entity.Accounting;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Sales
{
    public class SalDMSReportDetail : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int dmsReportDetailId { get; set; }       
        public int? dmsReportMasterId { get; set; }
        public SalDMSReportMaster dmsReportMaster { get; set; }
        public string reportName { get; set; }
        public string reportValue { get; set; }
        public int? sortOrder { get; set; }
        public string reportType { get; set; }

    }
}
