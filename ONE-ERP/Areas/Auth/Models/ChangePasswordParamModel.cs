using System;
using System.ComponentModel.DataAnnotations;

namespace ONEERP.Areas.Auth.Models
{
    public class ChangePasswordParamModel
    {       
        public string previousPassword { get; set; }  
        public string newPassword { get; set; }
    }
}
