
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Common
{
    public class CmnAutoStockInOutSetting : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string manuName { get; set; }
        public bool isAutoStock { get; set; }
    }
}
