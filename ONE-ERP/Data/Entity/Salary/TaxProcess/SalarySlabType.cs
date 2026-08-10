using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Salary.TaxProcess
{
    public class SalarySlabType : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int slabTypeId { get; set; }
        [MaxLength(100)]
        public string slabTypeName { get; set; }    
    }
}
