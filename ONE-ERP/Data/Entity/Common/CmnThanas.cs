using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Common
{
    public class CmnThanas:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int thanasId { get; set; }
        [MaxLength(50)]
        public string thanaCode { get; set; }
        [MaxLength(250)]

        public string thanaName { get; set; }
        [MaxLength(100)]

        public string shortName { get; set; }

        public int? districtsId { get; set; }
        public CmnDistricts districts { get; set; }
    }
}
