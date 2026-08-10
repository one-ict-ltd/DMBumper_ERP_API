using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Salary.TaxProcess
{
    public class SalaryRebateSlabType : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int rebateSlabTypeId { get; set; }
        [MaxLength(400)]
        public string rebateSlabTypeName { get; set; }    
        public decimal? minValue { get; set; }
        public decimal? maxValue { get; set; }
    }
}
