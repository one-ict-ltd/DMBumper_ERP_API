using ONEERP.Areas.Production.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Production.Interfaces
{
    public interface IProductionIssueAndReceived
    {

        Task<int> SaveIssueMaster(string Id, ProductionIssueViewModel model);
        Task<string> DeleteIssueById(string Id, int issueId);
        Task<JsonViewModel> GetIssueById(int? issueId, string typeOfIssue);
        Task<JsonViewModel> GetIssueByIdDate(int? userId, DateTime fromDate, DateTime toDate, int? issueId, string typeOfIssue);
        Task<int> SaveIssueDetails(string Id, List<ProductionIssueDetailViewModel> model, int issueId);
        Task<JsonViewModel> GetIssueDetailsByMasterId(int? issueId);
        Task<bool> DeleteIssueDetailsById(string userId, int issueDetailsId);


        #region ProductReceived
        Task<JsonViewModel> GetIssueNoForReceive(int type, int? userId);
        Task<JsonViewModel> GetIssueDataById(int? issueId);
       
        Task<JsonViewModel> GetIssueDetailsByMasterIdForReceive(int? issueId);


        Task<JsonViewModel> GetMaxReceiveMasterNumber(DateTime date, int type);
        Task<int> SaveReceiveMaster(string Id, ProductionReceiveViewModel model);
        Task<string> DeleteReceiveById(string Id, int receiveId);
        Task<JsonViewModel> GetReceiveById(int? receiveId);
        Task<JsonViewModel> GetReceiveByIdDate(int? userId, DateTime fromDate, DateTime toDate, int? receiveId);
        Task<int> SaveReceiveDetails(string Id, List<ProductionReceiveDetailViewModel> model, int receiveId);
        Task<JsonViewModel> GetReceiveDetailsByMasterId(int? receiveId);
        Task<bool> DeleteReceiveDetailsById(string userId, int receiveDetailsId);
        #endregion

        Task<JsonViewModel> GetMaxReturnMasterNumber(DateTime ReturnDate, int type);
        Task<JsonViewModel> GetRequisitionNumberforReturn(int type, int? userId);
        Task<JsonViewModel> GetRMPMReturnDetailsByReqMasterId(int? requisitionId);
        Task<int> SaveProductReturn(string Id, ProductionReturnViewModel model);
        Task<int> SaveProductReturnDetails(string Id, List<ProductionReturnDetailViewModel> model, int returnId);
        Task<JsonViewModel> GetReturnByIdDate(DateTime fromDate, DateTime toDate, int? returnId,int? userId);
        Task<string> DeleteReturnMasterById(string Id, int ReturnMasterId);
        Task<JsonViewModel> GetReturnDetailsByReturnMasterId(int? ProductReturnMasterId);

        Task<int> SaveProductReceiveFromReturn(string Id, ProductReceiveFromReturnViewModel model);
        Task<int> SaveProductReceiveFromReturnDetails(string Id, List<ProductReceiveFromReturnDetailViewModel> model, int returnId);
        Task<JsonViewModel> GetReturnFromReceiveByIdDate(DateTime fromDate, DateTime toDate, int? ProductReceiveFromReturnMasterId, int? userId);
        Task<string> DeleteProductReceiveFromReturnById(string Id, int ProductReceiveFromReturnMasterId);
        Task<JsonViewModel> GetProductReceiveFromReturnDetails(int? ProductReceiveFromReturnMasterId);
    }
}
