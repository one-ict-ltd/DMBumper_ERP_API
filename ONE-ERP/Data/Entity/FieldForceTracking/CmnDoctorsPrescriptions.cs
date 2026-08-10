using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnDoctorsPrescriptions : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PrescriptioID { get; set; }
        public int DoctorID { get; set; }
        public CmnDoctor Doctor { get; set; }
        //public IFormFile ImageUrl { get; set; }
        public string ImagePath { get; set; }
        public string Remarks { get; set; }
    }
}
