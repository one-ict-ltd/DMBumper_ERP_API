using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Data.Entity.HRM
{
    public class HrmTrainingInfoNew:NewBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int trainingInfoNewId { get; set; }
        public string year { get; set; }
        public string course { get; set; }
        public string budget { get; set; }
        public int? noOfParticipants { get; set; }
        public int? noOfParticipantsActual { get; set; }
        public int? employeeTypeId { get; set; }
        public int? countryId { get; set; }
        public int? trainingCategoryId { get; set; }
        public int? trainingInstituteId { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public DateTime? startDateActua { get; set; }
        public DateTime? endDateActual { get; set; }
        public string amount { get; set; }
        public string amountActual { get; set; }
        public string location { get; set; }
        public string courseObjective { get; set; }
        public string remarks { get; set; }
        public int? status { get; set; }
        public int? trainingType { get; set; }
        public string orgType { get; set; }
        public string employeeTypes { get; set; }
        public string employeeTypeNames { get; set; }

    }
}
