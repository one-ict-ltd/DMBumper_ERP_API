
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class ComTypeViewModel
    {
        public int comTypeId { get; set; }
        [Required]
        public string communicationTypeName { get; set; }      
    }
}
