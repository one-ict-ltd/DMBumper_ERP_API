using ONEERP.Areas.Production.Models;
using ONEERP.Areas.Purchase.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Production.Interfaces
{
    public interface IReagentReqService
    {
        Task<JsonViewModel> GetMaxReagentReqNumber(DateTime reagentReqDate);
        Task<JsonViewModel> GetAllProductForReagentReq(int productId, int employeeId);
        Task<int> SaveReagentReq(string id, ReagentRequisitionViewModel ReqViewModel);
        Task<int> SaveReagentReqDetails(string id, List<ReagentReqDetailsViewModel> purReagentReqDetailsViewModels, int reagentReqId);
        Task<JsonViewModel> GetReagentReqById(int? userId, int? reagentReqId);
        Task<bool> DeleteReagentReqById(int userId, int reagentReqId);
        Task<JsonViewModel> GetReagentReqDetailsById(int? reagentReqId);
    }
}
