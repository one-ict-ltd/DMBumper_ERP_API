using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Common
{
    public class CmnHelpDetail : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int helpDetailId { get; set; }
        public int helpId { get; set; }
        public CmnHelpMaster helpMaster { get; set; }
        public string dtext { get; set; }
        [MaxLength(50)]
        public int ddropDownId { get; set; }
        public DateTime ddate { get; set; }
        public string dpopUp { get; set; }
        [DefaultValue(0)]
        public bool dcheckbox { get; set; }
        public int? dradio{ get; set; }
        public string dImage { get; set; }
        public string dtextArea { get; set; }
    }
}
