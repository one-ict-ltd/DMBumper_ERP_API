using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Common
{
    public class CmnHelpMaster:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int helpId { get; set; }
        [MaxLength(50)]
        public string text { get; set; }
        public int? dropDownId { get; set; }
        public CmnDropDown dropDown { get; set; }
        public DateTime? date { get; set; }
        [MaxLength(100)]
        public string popUp { get; set; }
        [DefaultValue(0)]
        public bool checkbox { get; set; }
        [MaxLength(100)]
        public string textArea { get; set; }
        public int? radio { get; set; }
    }
}
