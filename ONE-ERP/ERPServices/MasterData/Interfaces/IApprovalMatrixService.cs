using ONEERP.Areas.MasterData.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.MasterData.Interfaces
{
    public interface IApprovalMatrixService
    {
        #region Approval Type        
        Task<JsonViewModel> GetApprovalTypeById(int approvalTypeId);
        Task<int> SaveApprovalType(string Id, ApprovalTypeViewModel approvalTypeViewModel);
        Task<bool> DeleteApprovalTypeByTypeId(string id, int approvalTypeId);

        #endregion

        #region Approver Type        
        Task<JsonViewModel> GetApproverTypeById(int approverTypeId, int approvalTypeId);
        Task<int> SaveApproverType(string Id, ApproverTypeViewModel approveTypeViewModel);
        Task<JsonViewModel> GetApproverType(int approverTypeId);
        Task<bool> DeleteApproverTypeId(string id, int approverTypeId);
        #endregion

        #region Approval Matrix
        Task<int> SaveApprovalMatrix(string id, List<ApprovalMatrixViewModel> model, int approvalTypeId);
        Task<JsonViewModel> GetApprovalMatrix(int approvalTypeId);
        Task<JsonViewModel> GetApprovalMatrixByTypeId(int approvalTypeId);
        Task<bool> DeleteApprovalMatrixByTypeId(string id, int approvalTypeId);

        #endregion
    }
}
