using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Auth.Models
{
    public class ChangePsswordViewModel
    {
        public string OldPassword { get; set; }
        public string Name { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
