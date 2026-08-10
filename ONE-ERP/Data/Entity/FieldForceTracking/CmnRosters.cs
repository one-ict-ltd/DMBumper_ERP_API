    using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnRosters
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RosterID { get; set; }
        public string Name { get; set; }
        public int CompanyID { get; set; }
        public int? IsActive { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateOn { get; set; }
        public string CreatePc { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateOn { get; set; }
        public string UpdatePc { get; set; }
        public int? IsDeleted { get; set; }
        public int? DeleteBy { get; set; }
        public DateTime? DeleteOn { get; set; }
        public string DeletePc { get; set; }

    }
}
