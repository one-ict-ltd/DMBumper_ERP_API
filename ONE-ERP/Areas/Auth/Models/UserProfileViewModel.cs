using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Auth.Models
{
    public class UserProfileViewModel
    {
      public string empName { get; set; }
      public string empDesignation { get; set; }
      public string empDepartment { get; set; }
      public string empMobile { get; set; }
      public string empAddress { get; set; }
      public string empAreaCode { get; set; }
      public string empTerritorycodeCode { get; set; }
      public string empArea { get; set; }
      public string empImage { get; set; }

    }
}
