using ONEERP.Areas.FieldForceTracking.Models;
using ONEERP.Areas.Schedule.Models;
using ONEERP.Data.Entity.FieldForceTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Models.Dashboard
{
    public class HomeViewModel
    {
        public string logoPath { get; set; }
        public string fileName { get; set; }

        public IEnumerable<CmnDoctor> cmnDoctors { get; set; }
        public IEnumerable<CmnChemist> cmnChemists { get; set; }
        public IEnumerable<MIOCurrentLocationViewModel> mIOCurrentLocationViewModels { get; set; }
        public IEnumerable<ChemistWiseVisitReportViewModel> chemistWiseVisitReportViewModels { get; set; }
        public IEnumerable<DoctorWiseVisitReportViewModel> doctorWiseVisitReportViewModels { get; set; }
        public IEnumerable<ZoneListViewModel> zoneListViewModels { get; set; }
        public IEnumerable<DepoListViewModel> depoListViewModels { get; set; }
        public IEnumerable<TeritoryListViewModel> teritoryListViewModels { get; set; }
        public IEnumerable<MIOListViewModel> mIOListViewModels { get; set; }
        public IEnumerable<ChemistDataChartViewModel> chemistDataChartViewModels { get; set; }
        public IEnumerable<LoginInfoDataViewModel> loginInfoDataViewModels { get; set; }
    }
}
