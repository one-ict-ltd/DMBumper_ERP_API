using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Models
{
    public class ExamContentViewModel
    {
        public int CmnExamContentID { get; set; }
        public string fileName { get; set; }
        public string description { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? endDate { get; set; }
        public int? isActive { get; set; }
    }

    public class CmnExamPerformViewModel
    {
        public int CmnExamPerformID { get; set; }
        public int? HrmEmployeeId { get; set; }
        public int? CmnExamQuestionId { get; set; }
        public int? CmnExamQuestionOptionId { get; set; }
        public int? marks { get; set; }
    }
    public class CmnExamPerformListViewModel
    {
        public List<CmnExamPerformViewModel> cmnExamPerformViewModels { get; set; }
    }

    public class CmnExamQuestionViewModel
    {
        public int productId { get; set; }
        public string productCode { get; set; }
        public string productName { get; set; }
        public int? productTypeId { get; set; }
        public int? width { get; set; }
        public DateTime? expiryDate { get; set; }
        public DateTime? lastSubmitDate { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public bool? isActive { get; set; }
        public List<ExamQuestionListViewModel> Specificationdetail { get; set; }
    }

    public class ExamQuestionListViewModel
    {
        public string skuName { get; set; }
        public string skuNumber { get; set; }
        public string specificationType { get; set; }
        public int? rowMerge { get; set; }
        public int? isHide { get; set; }
        public bool? value { get; set; }
    }

}
