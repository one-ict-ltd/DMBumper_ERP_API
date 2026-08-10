using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.Common
{
    public class CmnCompany : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int companyId { get; set; }
        public int? companyCategoryId { get; set; }
        public CmnCompanyCategory companyCategory { get; set; }
        [MaxLength(250)]
        public string companyName { get; set; }
        [MaxLength(150)]
        public string aliasName { get; set; }
        [MaxLength(250)]
        public string ownerName { get; set; }
        [MaxLength(250)]
        public string managerName { get; set; }
        [MaxLength(150)]
        public string tradeLicense { get; set; }
        [MaxLength(250)]
        public string businessNature { get; set; }
        [MaxLength(250)]
        public string alternateTelephone { get; set; }
        [MaxLength(250)]
        public string website { get; set; }
        [MaxLength(50)]
        public string officeTelephone { get; set; }
        [MaxLength(50)]
        public string vatNo { get; set; }
        [MaxLength(50)]
        public string tinNo { get; set; }
        public DateTime? dateOfEstablishment { get; set; }
        public int? permanentEmployee { get; set; }
        [MaxLength(250)]
        public string companyEmail { get; set; }
        [MaxLength(250)]
        public string alternetEmail { get; set; }
        public decimal? liquidityRatio { get; set; }
        [MaxLength(150)]
        public string filePath { get; set; }
        [MaxLength(150)]
        public string filePathTwo { get; set; }
        [MaxLength(150)]
        public string filePathThree { get; set; }
        public string addressLine { get; set; }
        [MaxLength(150)]
        public string companyBankAccName { get; set; }
        [MaxLength(50)]
        public string companyBankAccNo { get; set; }
        [MaxLength(10)]
        public string imageHeight { get; set; }
        [MaxLength(10)]
        public string imageWidth { get; set; }
    }
}