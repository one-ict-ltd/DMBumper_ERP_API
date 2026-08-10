using System;
namespace ONEERP.Areas.Hrm.Models
{
    public class TrainingViewModel
    {
        public int? trainingId { get; set; }
        public int employeeID { get; set; }
        public int? trainingTypeId { get; set; }
        public string trainingTitle { get; set; }
        //public string category { get; set; }
        public string institute { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public string remarks { get; set; }
        public string country { get; set; }
        public bool isActive { get; set; }


        //public int employeeID { get; set; }
        //public string trainingCategoryId { get; set; }
        //public string trainingTitle { get; set; }
        //public string institute { get; set; }
        //public DateTime? fromDate { get; set; }
        //public DateTime? toDate { get; set; }
        //public string remarks { get; set; }
        //public string country { get; set; }

        //public int trainingLogID { get; set; }
        //public string sponsoringAgency { get; set; }
        //public string employeeNameCode { get; set; }

        //public Photograph photograph { get; set; }
        //public EmployeeInfo employeeInfo { get; set; }

        //public TrainingLn fLang { get; set; }

        //public IEnumerable<Country> countries { get; set; }

        //public IEnumerable<TrainingCategory> trainingCategories { get; set; }

        //public IEnumerable<TrainingInstitute> trainingInstitutes { get; set; }

        //public IEnumerable<TraningLog> traningLogs { get; set; }
    }
}
