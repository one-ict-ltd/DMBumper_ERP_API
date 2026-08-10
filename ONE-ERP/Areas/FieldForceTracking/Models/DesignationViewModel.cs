//using UMBRELLA.Areas.MasterData.Models.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class DesignationViewModel
    {
        public int designationId { get; set; }

        [Required]
        [Display(Name = "Designation Code")]
        public string designationCode { get; set; }

        [Required]
        [Display(Name = "Designation Name")]
        public string designationName { get; set; }

        public string designationNameBn { get; set; }

        [Required]
        [Display(Name = "Short Name")]
        public string shortName { get; set; }

        //public DesignationLn fLang { get; set; }

    

    }
}
