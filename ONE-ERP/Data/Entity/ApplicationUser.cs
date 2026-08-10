using Microsoft.AspNetCore.Identity;


using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity
{
    public class ApplicationUser: IdentityUser
    {
        public int? userTypeId { get; set; }


       // public int userId { get; set; }

        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        //public CmnCompany company { get; set; }
        
 

        public int? isActive { get; set; }
        public int? isDelete { get; set; }
    
        public DateTime? createdAt { get; set; }
        [MaxLength(120)]
        public string createdBy { get; set; }

        public DateTime? updatedAt { get; set; }
        [MaxLength(120)]
        public string updatedBy { get; set; }

 

        //newly added for BnB Apps login
    
        public string imagePath { get; set; }
        public string token { get; set; }
        public string header { get; set; }
        public int? isLogedIn { get; set; }
        public int? employeeId { get; set; }
        public int? TerritoryID { get; set; }
        public DateTime? PassExpiredAt { get; set; }
    }
}
