using ONEERP.Data.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Accounting
{
    public class AccLedgers:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ledgerId { get; set; }
        public int? accountNatureId { get; set; }
        public AccGroupNature accountNature { get; set; }
        public int? accountGroupId { get; set; }
        public AccAccountGroup accountGroup { get; set; }
        public int? currencyId { get; set; }
        public AccCurrency currency { get; set; }
        public int? companyId { get; set; }
        public CmnCompany company { get; set; }
        public int? sbuId { get; set; }
        public CmnSpecialBranchUnit sbu { get; set; }
        public int? ledgerTypeId { get; set; }
        public AccLedgerType ledgerType { get; set; }
        [MaxLength(100)]
        public string accountCode { get; set; }
        [MaxLength(250)]
        public string accountName { get; set; }
        [MaxLength(100)]
        public string aliasName { get; set; }
        public int? haveSubledger { get; set; }        
        public int? parentId { get; set; }
        public int? haveCostCentre { get; set; }
        [MaxLength(1)]
        public string ledgerPrefix { get; set; }

        public int? partyId { get; set; }
        public AccParty party { get; set; }
    }
}
