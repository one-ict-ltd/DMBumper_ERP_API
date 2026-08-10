using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmEmployeeLunchHistory:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeeLunchHistoryId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public DateTime? LunchDate { get; set; }
        public int? guestNo { get; set; }
        public int? status { get; set; }
        public string remarks { get; set; }
    }
}
