using System;
using System.ComponentModel.DataAnnotations;

namespace ONEERP.Areas.Auth.Models
{
    public class LogOutViewModel
    {
      
        public string Name { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Address { get; set; }
    }
}
