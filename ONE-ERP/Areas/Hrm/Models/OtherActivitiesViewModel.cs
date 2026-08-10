

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Models
{
    public class OtherActivitiesViewModel
    {
        public int? employeeID { get; set; }
        public int? activityType { get; set; }
        public int? activityID { get; set; }
        public int? activityName { get; set; }
        public string description { get; set; }
        //public DateTime? startDate { get; set; }
        //public DateTime? endDate { get; set; }

        public string employeeNameCode { get; set; }

        //public SupervisorLn fLang { get; set; }
        //public IEnumerable<Supervisor> supervisors { get; set; }

        //public Photograph photograph { get; set; }
        //public EmployeeInfo employeeInfo { get; set; }

        //public IEnumerable<HRPMSActivityType> hRPMSActivityTypes { get; set; }
        //public IEnumerable<ActivityName> activityNames { get; set; }
        //public IEnumerable<OtherActivity> otherActivities { get; set; }
    }
}
