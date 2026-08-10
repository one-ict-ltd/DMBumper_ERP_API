using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Common
{
    public class CmnMenus:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int menuId { get; set; }
        [MaxLength(100)]
        public string menuName { get; set; }
        [MaxLength(100)]
        public string menuShortName { get; set; }
        public int? menuTypeId { get; set; }
        public CmnMenuTypes menuTypes { get; set; }
        public int? moduleId { get; set; }
        public CmnModule module { get; set; }
        [MaxLength(300)]
        public string menuPath { get; set; }
        [MaxLength(100)]
        public string reportName { get; set; }
        [MaxLength(100)]
        public string reportPath { get; set; }
        [MaxLength(50)]
        public bool? isParent { get; set; }
        public int? parentId { get; set; }
        public int? sequence { get; set; }
        [MaxLength(50)]
        public string menuIcon { get; set; }
    }
}
