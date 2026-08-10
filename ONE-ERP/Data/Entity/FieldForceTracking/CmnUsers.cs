using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnUsers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        public string CustomCode { get; set;}
        public int UserTypeID { get; set;}
        public int DesignationID { get; set;}
        public string UserFullName { get; set; }
        public int GenderID { get; set; }
        public string NID { get; set; }
        public string EmailID { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string PHint { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public string SpouseNane { get; set; }
        public string Address { get; set; }
        public int? intUpozilaID { get; set; }
        public string PhoneNo { get; set; }
        public string ImageUrl { get; set; }
        public int? IsActive { get; set; }
        public int? CompanyID { get; set; }

        public int? CreateBy { get; set; }
        public DateTime? CreateOn { get; set; }
        public string CreatePc { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateOn { get; set; }
        public string UpdatePc { get; set; }
        public int? IsDeleted { get; set; }
        public int? DeleteBy { get; set; }
        public int? DeleteOn { get; set; }
        public string DeletePc { get; set; }

    }
}
