using System;
using System.ComponentModel.DataAnnotations;

namespace ONEERP.Data.Entity.Auth
{
    public class UserAccessPage : Base
    {
        public int? navbarId { get; set; }
        public Navbar navbar { get; set; }

        public int? isAccess { get; set; }

        public string applicationRoleId { get; set; }
        public ApplicationRole applicationRole { get; set; }
    }

    public class CmnUserDesignation
    {

        [Key]
        public int DesignationID { get; set; }
        public string Designation { get; set; }
        public int? IsActive { get; set; }
        public int CompanyID { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateOn { get; set; }
        public string CreatePc { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateOn { get; set; }
        public string UpdatePc { get; set; }
        public int IsDeleted { get; set; }
        public int? DeleteBy { get; set; }
        public DateTime? DeleteOn { get; set; }
        public string DeletePc { get; set; }
    }


}
