using ONEERP.Data.Entity.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccNoteParent : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int noteParentId { get; set; }        
        [MaxLength(50)]
        public string noteType { get; set; }
        [MaxLength(200)]
        public string parentNoteName { get; set; }
        [MaxLength(50)]
        public string shortParentName { get; set; }
        public int? sortOrder { get; set; }
        

    }
}
