using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmTravelPurpose:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int travelPurposeId { get; set; }
        public string purposeName { get; set; }
        public string purposeNameBn { get; set; }
        public string purposeShortName { get; set; }
    }
}
