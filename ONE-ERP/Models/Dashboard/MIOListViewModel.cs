using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Models.Dashboard
{
    public class MIOListViewModel
    {
       public string EMP_ID { get; set; }
        public string  EMPLOYEE_NAME { get; set; }
        public string POSTING_LOCATION { get; set; }
        public string DEPOT_CODE { get; set; }
        public string  ZONE_CODE { get; set; }
        public string REGION_CODE { get; set; }
        public string AREA_CODE { get; set; }
        public string TERRITORY_CODE { get; set; }
        public int companyId { get; set; }
    }
}
