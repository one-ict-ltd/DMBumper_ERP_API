using ONEERP.Areas.Auth.Models;
using ONEERP.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class MIOVisitReportViewModel
    {
        public int?[] selectIds { get; set; }
        public int?[] WeekendIds { get; set; }
        public int?[] day { get; set; }
        public string[] dayName { get; set; }
        public DateTime?[] date { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public IEnumerable<AspNetUsersViewModel> aspNetUsersViewModels { get; set; }
        public IEnumerable<VisitReportDoctorViewModel> visitReportDoctorViewModels { get; set; }
        public IEnumerable<VisitReportChemistViewModel> visitReportChemistViewModels { get; set; }
        public IEnumerable<UserInfoViewModel> userInfoViewModels { get; set; }
        public IEnumerable<ZoneListViewModel> zoneListViewModels { get; set; }
        public IEnumerable<DepoListViewModel> depoListViewModels { get; set; }
        public IEnumerable<TeritoryListViewModel> teritoryListViewModels { get; set; }
        public IEnumerable<MIOListViewModel> mIOListViewModels { get; set; }
    }
}
