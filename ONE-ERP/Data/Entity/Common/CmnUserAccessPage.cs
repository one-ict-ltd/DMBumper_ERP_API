using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Common
{
    public class CmnUserAccessPage : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userAccessPageId { get; set; }
        public int? navbarId { get; set; }
        public int? isAccess { get; set; }        
        public int? companyId { get; set; }
        public CmnCompany company {get;set;}
        public int? sbuId { get; set; }
        public CmnSpecialBranchUnit sbu { get; set; }
        [MaxLength(250)]
        public string applicationRoleId { get; set; }
    }
}
