using System.Collections.Generic;
namespace ONEERP.Areas.Hrm.Models
{
    public class PromotionViewModel
    {
        public int? ID { get; set; }
        public int employeeId { get; set; }
        public string employeeName { get; set; }
        public string promotionType { get; set; }
        public int designationId { get; set; }
        public string designationName { get; set; }
        public string promotionDate { get; set; }
        public int salaryGradeId { get; set; }
        public string gradeName { get; set; }
        public string gradeAliasName { get; set; }
        public decimal? Basic { get; set; }
        public string Remarks { get; set; }
    }
}
