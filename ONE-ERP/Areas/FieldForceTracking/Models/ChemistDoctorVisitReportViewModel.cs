
using ONEERP.Data.Entity.FieldForceTracking;
using ONEERP.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class ChemistDoctorVisitReportViewModel
    {
        public IEnumerable<CmnDoctor> GetCmnDoctors { get; set; }
        public IEnumerable<CmnChemist> GetCmnChemists { get; set; }
        public IEnumerable<ChemistWiseVisitReportViewModel> chemistWiseVisitReportViewModels { get; set; }
        public IEnumerable<DoctorWiseVisitReportViewModel> doctorWiseVisitReportViewModels { get; set; }
        public IEnumerable<ChemistDataViewModel> chemistDataViewModels { get; set; }
        public IEnumerable<ZoneListViewModel> zoneListViewModels { get; set; }
        public IEnumerable<MIOCurrentLocationViewModel> mIOCurrentLocationViewModels { get; set; }
        public IEnumerable<VisitReportEmployeeViewModel> visitReportEmployeeViewModels { get; set; }
    }
}
