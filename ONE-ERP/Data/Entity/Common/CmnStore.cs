using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Common
{
    public class CmnStore:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int storeId { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { set; get; }
        public int? sbuId { get; set; }
        public CmnSpecialBranchUnit specialBranchUnit { get; set; }
        [MaxLength(250)]
        public string storeName { get; set; }
        [MaxLength(50)]
        public string storeCode { get; set; }
        [MaxLength(50)]
        public string depotCode { get; set; }
        public int? storeTypeId { get; set; }
        public CmnStoreType storeType { get; set; }
    }

    public class CmnStoreType:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int storeTypeId { get; set; }
        [MaxLength(50)]
        public string storeTypeName { get; set; }
    }
}
