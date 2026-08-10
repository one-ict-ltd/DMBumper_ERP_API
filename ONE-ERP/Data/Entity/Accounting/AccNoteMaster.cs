using ONEERP.Data.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccNoteMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int noteMasterId { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
        public int? sbuId { get; set; }
        public CmnSpecialBranchUnit sbu { get; set; }
        public int? noteParentId { get; set; }
        public AccNoteParent noteParent { get; set; }
        //[MaxLength(250)]
        //public string noteType { get; set; }
        [MaxLength(350)]
        public string noteName { get; set; }
        [MaxLength(250)]
        public string noteNo { get; set; }
        public int? sortOrder { get; set; }
        

    }
}
