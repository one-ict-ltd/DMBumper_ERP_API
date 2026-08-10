using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Common
{
    public class CmnUserLoginInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userLoginInfoId { get;set;}
        [MaxLength(50)]
        public string userName { get;set;}
        public DateTime? date { get;set; }
        public int? islogin { get;set; }
        public string EmpCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Address { get; set; }
        public DateTime? DateTime { get; set; }
        
    }
}
