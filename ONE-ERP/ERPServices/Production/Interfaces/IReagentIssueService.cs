using ONEERP.Areas.Production.Models;
using ONEERP.Areas.Purchase.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Production.Interfaces
{
    public interface IReagentIssueService
    {
        Task<JsonViewModel> GetMaxReagentIssueNumber(DateTime reagentIssueDate);
        Task<JsonViewModel> GetReagentRequisitionNumberforIssue(int userId);
        Task<JsonViewModel> GetReagentRequisitionByIdToIssue(int userId, int reagentReqId);
        Task<int> SaveReagentIssueMaster(int userId, ReagentIssueViewModel model);
        Task<int> SaveReagentIssueDetails(int userId, List<ProductionIssueDetailViewModel> lstDetailsViewModel, int issueId);
        Task<JsonViewModel> GetReagentIssueListByDate(int? userId, DateTime fromDate, DateTime toDate, int? issueId, string typeOfIssue);
        Task<JsonViewModel> GetReagentIssueDetailsByMasterId(int? issueId);
        Task<string> DeleteReagentIssueById(string Id, int issueId);
        //Task<JsonViewModel> GetAllProductForReagentReq(int productId, int employeeId);
        //Task<int> SaveReagentReq(string id, ReagentRequisitionViewModel ReqViewModel);
        //Task<int> SaveReagentReqDetails(string id, List<ReagentReqDetailsViewModel> purReagentReqDetailsViewModels, int reagentReqId);
        //Task<JsonViewModel> GetReagentReqById(int? userId, int? reagentReqId);
        //Task<bool> DeleteReagentReqById(int userId, int reagentReqId);
        //Task<JsonViewModel> GetReagentReqDetailsById(int? reagentReqId);
    }
}
