using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Auth.Models
{
    public class UserLoginViewModel
    {
        public string MobileNo { get; set; }
        public string Teritory { get; set; }

        public string UserFullName { get; set; }

        public string EmailID { get; set; }
    }
}
