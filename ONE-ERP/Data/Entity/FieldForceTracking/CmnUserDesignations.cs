using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnUserDesignations
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DesignationID { get; set; }
        [MaxLength(50)]
        public string DesignationCode { get; set; }
        public string Designation { get; set; }
        public int? CompanyID { get; set; }

        public int? IsActive { get; set; }
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
