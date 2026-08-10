using System;
namespace ONEERP.Areas.Hrm.Models
{
    public class PromotionLogViewModel
    {
        public string promotionId { get; set; }
        public string employeeID { get; set; }
        public string designation { get; set; }
        public int? designationNewId { get; set; }
        public int? designationOldId { get; set; }
        public DateTime date { get; set; }
        public string payScale { get; set; }
        public string nature { get; set; }
        public string basic { get; set; }
        public string rank { get; set; }
        public string remark { get; set; }
        public string employeeNameCode { get; set; }
        public string goNumber { get; set; }
        public DateTime? goDate { get; set; }
    }
}
