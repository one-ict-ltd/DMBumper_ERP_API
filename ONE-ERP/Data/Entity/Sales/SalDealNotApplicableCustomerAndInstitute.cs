using ONEERP.Data.Entity.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ONEERP.Data.Entity.Sales
{
    public class SalDealNotApplicableCustomerAndInstitute : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int dealNotApplicableCustomerAndInstituteId { get; set; }
        public int partyId { get; set; }
        public string bonusType { get; set; }
        public string customerType { get; set; }

    }
}
