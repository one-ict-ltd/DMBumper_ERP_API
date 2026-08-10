using ONEERP.Data.Entity.Accounting;
using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Sales
{

    public class SalRemittanceMaster : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int remittanceMasterId { get; set; }

        public DateTime? remittanceDate { get; set; }
        public decimal? ttlRemittanceAmnt { get; set; }
    }

    public class SalRemittance : NewBase    // Details
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int remittanceId { get; set; }

        public DateTime? remittanceDate { get; set; }
        public int remittanceNo { get; set; }
        public int? remittanceTypeId { get; set; }

        [MaxLength(5000)]
        public string oplTranNo { get; set; }

        [MaxLength(50)]
        public string depotCode { get; set; }

        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
        public DateTime? depositDate { get; set; }
        //public int? bankId { get; set; }
        //public CmnBank bank { get; set; }
        public int? bankBranchId { get; set; }
        public CmnBankBranch bankBranch { get; set; }

        [MaxLength(100)]
        public string depositRefNo { get; set; }

        public decimal? depositAmount { get; set; }

        [MaxLength(500)]
        public string remarks { get; set; }
        public int? remittanceMasterId { get; set; }
        public SalRemittanceMaster remittanceMaster { get; set; }
        public int? voucherMasterId { get; set; }

        public AccVoucherMasters voucherMaster { get; set; }
    }
}