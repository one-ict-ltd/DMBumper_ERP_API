using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.HrmMaster
{
    public class HrmDegree:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int degreeId { get; set; }
        [MaxLength(250)]
        public string name { get; set; }
        [MaxLength(250)]
        public string shortName { get; set; }
        public int? levelOfEducationId { get; set; }
        public HrmLevelofEducation levelOfEducation{get;set;}
    }
}
