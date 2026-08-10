using ONEERP.Areas.Production.Models;
using ONEERP.Areas.Purchase.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Production.Interfaces
{
    public interface IReagentReceiveService
    {
        Task<JsonViewModel> GetMaxReagentReceiveNumber(DateTime receiveDate);
        Task<JsonViewModel> GetReagentIssueNumberForReceive(int userId);
        Task<JsonViewModel> GetReagentIssueDetailsByMasterIdForReceive(int? issueId);
        Task<int> SaveReagentReceiveMaster(int userId, ReagentReceiveViewModel model);
        Task<int> SaveReagentReceiveDetails(string Id, List<ReagentReceiveDetailViewModel> model, int receiveId);      
        Task<JsonViewModel> GetReagentReceiveListByDate(int? userId, DateTime fromDate, DateTime toDate, int? receiveId);
        Task<JsonViewModel> GetReagentReceiveDetailsByMasterId(int? receiveId);
        Task<string> DeleteReagentReceiveById(string Id, int receiveId);
    }
}
