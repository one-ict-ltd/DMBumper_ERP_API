using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnCalender
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? Day { get; set; }
        public DateTime? Date { get; set; }
        [MaxLength(50)]
        public string DayName { get; set; }
        public int? MonthNo { get; set; }
        public int? Year { get; set; }
        public int? IsHoliDay { get; set; }
        public int? CompanyID { get; set; }
        public int? IsActive { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateOn { get; set; }
        public string CreatePc { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateOn { get; set; }
        public string UpdatePc { get; set; }
        public int? IsDeleted { get; set; }
        public int? DeleteBy { get; set; }
        public DateTime? DeleteOn {get;set;}
        public string DeletePc { get; set; } 
    }
}
