using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmEmployeeNominee:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int nomineeId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        [MaxLength(250)]
        public string name { get; set; }
        [MaxLength(300)]
        public string address { get; set; }
        [MaxLength(250)]
        public string contact { get; set; }
        [MaxLength(250)]
        public string NID { get; set; }
        [MaxLength(250)]
        public string BRN { get; set; }
        [MaxLength(400)]
        public string imageUrl { get; set; }
        [MaxLength(250)]
        public string guardianName { get; set; }
        [MaxLength(250)]
        public string witnessName { get; set; }
        public DateTime? nomineeDate { get; set; }
        public string relationId { get; set; }
    }
}
