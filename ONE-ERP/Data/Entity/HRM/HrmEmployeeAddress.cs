using ONEERP.Data.Entity.Common;
using ONEERP.Data.Entity.HrmMaster;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmEmployeeAddress : NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeeAddressId { get; set; }
        public int? employeeId { get; set; }
        public HrmEmployee employee { get; set; }
        public int? addressTypeId { get; set; }
        public HrmAddressType addressType { get; set; }
        [MaxLength(600)]
        public string address { get; set; }
        public int? countryId { get; set; }
        public CmnOriginCountry originCountry { get; set; }
        public int? divisionId { get; set; }
        public CmnDivisions division { get; set; }
        public int? districtId { get; set; }
        public CmnDistricts district { get; set; }
        public int? thanaId { get; set; }
        public CmnThanas thana { get; set; }
    }
}
