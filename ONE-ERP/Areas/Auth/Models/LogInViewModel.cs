using System;
using System.ComponentModel.DataAnnotations;

namespace ONEERP.Areas.Auth.Models
{
    public class LogInViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} at most {1} characters long.")]
        [Display(Name = "Name")]
        public string Name { get; set; }    
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }


        //addition
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Address { get; set; }
        public string deviceNo { get; set; }
    }
}
