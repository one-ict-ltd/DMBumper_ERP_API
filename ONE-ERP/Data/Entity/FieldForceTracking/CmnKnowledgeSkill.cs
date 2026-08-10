using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.FieldForceTracking
{
    public class CmnKnowledgeSkill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CmnKnowledgeSkillID { get; set; } 
        public string imageUrl { get; set; }
        public int? type { get; set; } //1 =image 2 = video
        public string videoUrl { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public bool? isActive { get; set; }
    }
}
