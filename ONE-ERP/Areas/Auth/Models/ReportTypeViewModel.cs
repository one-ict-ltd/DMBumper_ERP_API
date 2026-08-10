using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ONEERP.Areas.Auth.Models
{
    public class ReportTypeViewModel
    {
        public int? reportTypeId { get; set; }        
        public string reportTypeName { get; set; }
        public bool? isActive { get; set; }
        

    }
}
