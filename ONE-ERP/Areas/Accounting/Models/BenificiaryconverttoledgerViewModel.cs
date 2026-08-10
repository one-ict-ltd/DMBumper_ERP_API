using System;
using System.Collections.Generic;

namespace ONEERP.Areas.Accounting.Models
{
    public class BenificiaryconverttoledgerViewModel
    {
        public int? partyId { get; set; }
        public string partyCode { get; set; }
        public string partyName { get; set; }
        public string tradeLicense { get; set; }
        public string drugLicense { get; set; }
        public string aliasName { get; set; }
        public string addressLine { get; set; }
        public string contactNumber { get; set; }
        public string contactPerson { get; set; }
        public string email { get; set; }
        public string officeName { get; set; }
        public string ownerName { get; set; }
        public string fatherName { get; set; }
        public string motherName { get; set; }
        public string territoryId { get; set; }
        public DateTime? birthdate { get; set; }
        public string nid { get; set; }
        public string gender { get; set; }
        public int? visaPartyId { get; set; }
        public int? partyTypeId { get; set; }
        public int? companyId { get; set; }
        public int? sbuId { get; set; }
        public bool? isActive { get; set; }
        public DateTime? businessStartDate { get; set; }
        public decimal? creditLimit { get; set; }
        public decimal? creditDays { get; set; }
        public string creditLimitWord { get; set; }
        public int? companyCategoryId { get; set; }
        public bool? isApproved { get; set; }
        public bool? isHold { get; set; }
        public bool? isSelect { get; set; }

        public List<BenificiaryconverttoledgerViewModel> lstMasterViewModel { get; set; }
       
    }
}
