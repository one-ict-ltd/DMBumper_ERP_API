using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class DailyPlanDocViewModel
    {
        public List<DailyPlanDocViewModelDetails> dailyPlanDocViewModelDetails { get; set; }
    }

    public class DailyPlanDocViewModelDetails
    {
        public string EmpCode { get; set; }
        public string DoctorCode { get; set; }
        public string TerritoryCode { get; set; }
        public string day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Remarks { get; set; }
    }

    public class UpdateDailyPlanDocViewModelDetails
    {
        public List<int> DailyPlanDocs { get; set; }
        public int status { get; set; }
    }

    public class UpdatePartyObserbationViewModel
    {
        public List<UpdatePartyObserbationViewModelDetails> PartyList { get; set; }
        public int status { get; set; }
    }
    public class UpdatePartyObserbationViewModelDetails
    {
        public int PartyId { get; set; }
        public decimal? creditLimit { get; set; }
    }

    public class EmployeeTADAObject
    {
        public int EmployeeTADAId { get; set; }
        public decimal? amount { get; set; }
        public int status { get; set; }
        public String remarks { get; set; }
    }

    public class UpdateEmployeeTADAViewModelDetails
    {
        public List<EmployeeTADAObject> DailyPlanDocs { get; set; }
    }
    public class EmployeeMonthlyPromoItem
    {
        public int TerritoryWiseMonthlyPromoItemID { get; set; }
        public decimal? amount { get; set; }
        public int monthno { get; set; }
    }

    public class UpdateEmployeeMonthlyPromoItem
    {
        public List<EmployeeMonthlyPromoItem> employeeMonthlyPromoItems { get; set; }
    }
}
