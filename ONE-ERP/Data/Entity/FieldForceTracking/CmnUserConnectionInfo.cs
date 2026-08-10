using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnUserConnectionInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        public string EmpCode { get; set; }
        public DateTime Date { get; set; }
        [MaxLength(50)]
        public string Time { get; set; }
        public bool? Islocation { get; set; }
        public bool? IsDataConnected { get; set; }
    }
}
