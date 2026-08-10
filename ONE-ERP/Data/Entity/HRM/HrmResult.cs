using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmResult:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int resultId { get; set; }
        public string resultName { get; set; }
        public string resultNameBn { get; set; }
        public string resultShortName { get; set; }
        public decimal resultMaxValue { get; set; }
    }
}
