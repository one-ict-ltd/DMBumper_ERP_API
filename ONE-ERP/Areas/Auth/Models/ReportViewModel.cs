using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ONEERP.Areas.Auth.Models
{
    public class ReportViewModel
    {        
        public int? reportId { get; set; }
        public int? reportTypeId { get; set; }
        public int? moduleId { get; set; }
        public string reportName { get; set; }
        public bool? isActive { get; set; }
        

    }
}
