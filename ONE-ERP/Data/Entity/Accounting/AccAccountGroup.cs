using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccAccountGroup:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int accountGroupId { get; set; }
        public int? groupNatureId { get; set; }
        public AccGroupNature groupNature { get; set; }
        public int? parentId { get; set; }
        [MaxLength(10)]
        public string groupCode { get; set; }
        [MaxLength(250)]
        public string groupName { get; set; }
        public int? sortOrder { get; set; }
    }
}
