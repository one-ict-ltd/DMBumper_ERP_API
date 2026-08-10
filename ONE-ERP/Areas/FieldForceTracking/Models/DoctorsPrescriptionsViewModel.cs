using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class DoctorsPrescriptionsViewModel
    {
        public int? PrescriptioID { get; set; }
        public int? DoctorID { get; set; }
        public DateTime Date { get; set; }
        public string ImagePath { get; set; }
        public string Remarks { get; set; }
        public int IsActive { get; set; } = 1;
        public IFormFile ImageUrl { get; set; }
    }
}
