using ONEERP.Areas.Production.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Production.Interfaces
{
    public interface IBomRequisitionService
    {
        Task<JsonViewModel> GetMaxRMRequisitionMasterNumber(DateTime date);
        Task<JsonViewModel> GetMaxIssueMasterNumber(DateTime date,int type);
        Task<JsonViewModel> GetMaxPMRequisitionMasterNumber(DateTime date);
        Task<JsonViewModel> GetProductSpecificatinDataByIdFromBomDetails(int? bomId,int? bomForId, int? userId);

        Task<int> SaveRMRequisitionMaster(string Id, RmRequisitionViewModel model);
        Task<string> DeleteRMRequisitionById(string Id, int requisitionId);
        Task<JsonViewModel> GetRMRequisitionById(int? requisitionId);
        Task<JsonViewModel> GetRMRequisitionByIdWithDate(DateTime fromDate, DateTime toDate, int? requisitionId, int? userId);
        #region RMRequisition Details

        Task<int> SaveRMRequisitionDetails(string Id, List<RMRequisitionDetailsViewModel> model, int requisitionId);
        Task<JsonViewModel> GetRMRequisitionDetailsByMasterId(int? requisitionId, int? userId);
        Task<bool> DeleteRMRequisitionDetailsById(string userId, int rmRequisitionDetailsId);

        #endregion


        #region Issue
        Task<JsonViewModel> GetRequisitionNoForIssue(int type,int userId);
        
        #endregion

    }
}
