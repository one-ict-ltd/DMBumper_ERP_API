using ONEERP.Areas.Sales.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Sales.Interfaces
{
    public interface ISalesDistributionService
    {
        #region SalesDistribution Master

        Task<bool> DeleteSalesDistributionById(string id, int distributionMasterId);
        Task<int> SaveSalesDistribution(string id, SalesDistributionMasterViewModel model);
        Task<JsonViewModel> GetSalesDistributionById(int? distributionMasterId);
        Task<JsonViewModel> GetMaxSalesDistributionNumber(DateTime datetime);
        Task<JsonViewModel> GetDepoWiseSalesInvoiceList(int? depoId);
        
        #endregion

        #region SalesDistribution Details

        Task<JsonViewModel> GetSalesDistributionDetailsByInvoiceId(int? salesInvoiceId);
        Task<JsonViewModel> GetSalesDistributionDetailsByMasterId(int? distributionMasterId);
        Task<bool> DeleteSalesDistributionDetailsById(string id, int distributionDetailId);
        Task<int> SaveSalesDistributionDetails(string id, List<SalesDistributionDetailsViewModel> Model, int distributionMasterId);

        #endregion

        #region Approval

        Task<JsonViewModel> GetSalesDistributionApprovedList(string userId, int? distributionMasterId);
        Task<JsonViewModel> GetSalesDistributionListForApproval(string userId, int? distributionMasterId);
        Task<int> ApproveSalesDistribution(string userId, List<SalesDistributionMasterViewModel> models, string approvalStatus);
        
        #endregion

        #region Reports

        Task<JsonViewModel> GetSalesDistributionReportDataById(int? distributionMasterId);
        Task<JsonViewModel> GetDestructionReportById(int? userId, int? masterId, string rType, string depotCode, DateTime fDate, DateTime tDate);

        #endregion

        #region  miscellaneous item for factory

        Task<int> SaveMiscellaneousItem(int? id, MiscellaneousItemViewModel model);
        Task<JsonViewModel> GetMiscellaneousItemById(int? id, int? miscellaneousItemId);
        Task<int> SaveMiscellaneousItemDetails(int? id, List<MiscellaneousItemDetailsViewModel> models, int miscellaneousItemId);
        Task<JsonViewModel> GetMiscellaneousItemDetailsByMasterId(int? id, int? miscellaneousItemId);
        Task<int> DeleteMiscellaneousItem(int? id, int miscellaneousItemId);
        Task<int> DeleteMiscellaneousItemDetails(int? id, int miscellaneousItemId);
        Task<JsonViewModel> GetMaxMiscellaneousNumber(int? userId, DateTime datetime);

        #endregion  miscellaneous item for factory 
        

        #region  miscellaneous item for Depot

        Task<int> SaveMiscellaneousItemDepot(int? id, MiscellaneousItemViewModel model);
        Task<JsonViewModel> GetMiscellaneousItemDepotById(int? id, int? miscellaneousItemId);
        Task<int> SaveMiscellaneousItemFileDepot(int? id, List<MiscellaneousItemFileViewModel> models, int miscellaneousItemId);
        Task<int> SaveMiscellaneousItemDetailsDepot(int? id, List<MiscellaneousItemDetailsViewModel> models, int miscellaneousItemId);
        Task<JsonViewModel> GetMiscellaneousItemDetailsDepotByMasterId(int? id, int? miscellaneousItemId);
        Task<int> DeleteMiscellaneousItemDepot(int? id, int miscellaneousItemId);
        Task<int> DeleteMiscellaneousItemDetailsDepot(int? id, int miscellaneousItemId);
        Task<JsonViewModel> GetMaxMiscellaneousNumberDepot(int? userId, DateTime datetime);
        Task<JsonViewModel> GetAllMiscellaneousType(int? userId, string param);

        #endregion  miscellaneous item for Depot 

        #region  miscellaneous item  for depot(Approval)
        Task<JsonViewModel> GetALLMiscellaneousItemDepotByApproval(int? id, int? isApproved);
        Task<int> SaveMiscellaneousItemForDepotApproval(int? userId, MiscellaneousItemApprovalViewModel model);
        #endregion miscellaneous item  for depot(Approval)

        #region  Deal Not Applicable
        Task<bool> SaveDealNotApplicableCustomerAndInstitute(string id, SalDealNotApplicableCustomerAndInstituteViewModel model);
        Task<JsonViewModel> getDealNotApplicableCustomerAndInstituteList(string userId, int dealNotApplicableCustomerAndInstituteId);
        #endregion
    }
}
