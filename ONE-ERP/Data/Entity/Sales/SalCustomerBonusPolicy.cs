using ONEERP.Data.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Sales
{
    public class SalGeneralCustomerBonusPolicy : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int generalPolicyId { get; set; }
        public int fromDays { get; set; }
        public int toDays { get; set; }
        public int maxDays { get; set; }
        public decimal percentValue { get; set; }
        public int? cmnCompanyId { get; set; }
        public CmnCompany cmnCompany { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? FromEffectiveDate { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? ToEffectiveDate { get; set; }
        public decimal collectionValue { get; set; }
    }
    public class SalMangoCustomerBonusPolicy : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int mangoPolicyId { get; set; }
        //[Column(TypeName = "Date")]
        public int fromMonth { get; set; }
        //[Column(TypeName = "Date")]
        public int toMonth { get; set; }
        [Column(TypeName = "Date")]
        public DateTime paymentDate { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? FromInvoiceDate { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? ToInvoiceDate { get; set; }
        public decimal percentValue { get; set; }
        public int? cmnCompanyId { get; set; }
        public CmnCompany cmnCompany { get; set; }
    }
}
