using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Inventory
{
    public class InvProductGrade:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int gradeId { get; set; }
        [MaxLength(250)]
        public string gradeName { get; set; }
        [MaxLength(50)]
        public string gradeCode { get; set; }
        [MaxLength(10)]
        public string aliesName { get; set; }
    }
}
