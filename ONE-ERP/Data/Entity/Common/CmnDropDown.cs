using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Common
{
    public class CmnDropDown : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int dropDownId { get; set; }
        public int? dropDownTypeId { get; set; }
        public CmnDropDownType dropDownType { get; set; }
        [MaxLength(300)]
        public string dropDownValue { get; set; }
        [MaxLength(300)]
        public string dropDownText { get; set; }
        public int? sortOrder { get; set; }
    }
}
