using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnTADAForEmployee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CmnTADAForEmployeeID { get; set; }

        public DateTime? date { get; set; }

        public string employeeCode { get; set; }

        public decimal? amount { get; set; }
        public decimal? taAmount { get; set; }

        public int? status { get; set; }

        public string remarks { get; set; }
    }

    //public class CmnTADAForEmployeeRemarksHistory
    //{
    //    [Key]
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int CmnTADAForEmployeeRemarksHistoryID { get; set; }

    //    public int? CmnTADAForEmployeeId { get; set; }
    //    public CmnTADAForEmployee CmnTADAForEmployee { get; set; } 
    //    public DateTime? date { get; set; }
    //    public string employeeCode { get; set; }
    //    public string UpdatedBy { get; set; }
    //    public decimal? amount { get; set; }
    //    public decimal? taAmount { get; set; }
    //    public string remarks { get; set; }
    //}
}
