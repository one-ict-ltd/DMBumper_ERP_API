using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Common
{
    public class CmnUserGroup : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userGroupId { get;set;}
        [MaxLength(200)]
        public string groupName { get;set; }
        [MaxLength(100)]
        public string shortName { get; set; }
    }
}
