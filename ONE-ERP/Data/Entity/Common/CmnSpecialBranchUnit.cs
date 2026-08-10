using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Common
{
    public class CmnSpecialBranchUnit : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int sbuId { get; set; }
        [MaxLength(250)]
        public string sbuName { get; set; }
        [MaxLength(50)]
        public string aliasName { get; set; }
        [MaxLength(50)]
        public string sbuCode { get; set; }
        public int? shortOrder { get; set; }
        [DefaultValue(0)]
        public bool? isDefault { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
        [MaxLength(150)]
        public string branchBankAccName { get; set; }
        [MaxLength(150)]
        public string branchBankAccNo { get; set; }
        public string branchAddress { get; set; }
        public int? sbuTypeId { get; set; }
        public CmnStoreType sbuType { get; set; }
    }
}