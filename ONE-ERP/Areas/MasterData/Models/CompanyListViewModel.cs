using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.MasterData.Models
{
    public class CompanyListViewModel
    {
    
        public int? companyId { get; set; }
        public string companyName { get; set; }
        public string aliasName { get; set; }
        public string ownerName { get; set; }
        public string managerName { get; set; }
        public string tradeLicense { get; set; }
        public string businessNature { get; set; }
        public string officeTelephone { get; set; }
        public string vatNo { get; set; }
        public string tinNo { get; set; }
        public DateTime? dateOfEstablishment { get; set; }
        public int? permanentEmployee { get; set; }
        public string companyEmail { get; set; }
        public string alternetEmail { get; set; }
        public decimal? liquidityRatio { get; set; }
        public string filePath { get; set; }
        public string addressLine { get; set; }
        public string filePathTwo { get; set; }
        public string filePathThree { get; set; }
        public bool? isActive { get; set; }
    }
}
