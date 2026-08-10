using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ONEERP.Data.Entity.HrmMaster
{
    public class HrmTrainingType:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int trainingTypeId { get; set; }
        [MaxLength(250)]
        public string name { get; set; }
        [MaxLength(250)]
        public string shortName { get; set; }
        [MaxLength(20)]
        public string nameBn { get; set; }
    }
}
