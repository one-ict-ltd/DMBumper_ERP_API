using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Common
{
    public class CmnProbationPeriod : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int probationPeriodId { get; set; }
        public string probationPeriodName { get; set; }
    }
}
