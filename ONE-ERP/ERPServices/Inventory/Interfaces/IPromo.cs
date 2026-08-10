using ONEERP.Areas.Inventory.Models;
using ONEERP.Areas.Sales.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Inventory.Interfaces
{
    public interface IPromo
    {
        Task<JsonViewModel> GetPromoRequisitionMaster();
        Task<bool> DeletePromoRequisitionById(int? userId, int promoRequisitionId);
        Task<JsonViewModel> GetPromoReqDetails(string userId, int promoRequisitionId);
        Task<JsonViewModel> GetAllPacketBySbuId(int sbuId, int? userId);
        Task<JsonViewModel> TerritoryWisePromo(int userId, DateTime fDate, DateTime tDate, string territoryCode);
        Task<JsonViewModel> GetMaxPacketTransferNumberJson(DateTime Datetime);
        Task<JsonViewModel> GetMaxReceivedTransferNumberJson(DateTime Datetime);
        Task<JsonViewModel> GetMaxDistributeTransferNumberJson(DateTime Datetime);
        Task<JsonViewModel> GetMaxPacketingMasterNo(int? employeeId, DateTime dateTime);
        Task<JsonViewModel> getDistribution(int sbuId);
        Task<JsonViewModel> getReceived(int sbuId);
        Task<JsonViewModel> getRequisition(string userId);
        Task<JsonViewModel> GetTerritoryByRequisition(int requisitionId);
        Task<JsonViewModel> GetAreaManagerCodeByRequisition(int requisitionId);
        Task<JsonViewModel> GetRSMCodeByRequisition(int requisitionId);
        Task<JsonViewModel> GetProductReqDetails(int? userId,string territoryCode, int requisitionId, string allocationType);
        Task<JsonViewModel> GetAllPacketByDistribution(int distributionId);
        Task<JsonViewModel> GetAllPacketByReceived(int distributionId);
        Task<int> SavePromoTransfer(string id, PromoTransferViewModel model);
        Task<int> SavePromoReceive(string id, DepotPromoReceiveViewModel model);
        Task<int> SaveDepotPromoDistribution(string id, DepotPromoDistributionViewModel model);
        Task<int> SavePromoPacketMaster(string id, PromoPacketingVM model);
        Task<int> SaveBulkPromoPacketMaster(string id, PromoBulkPacketingVM model);
        Task<int> SavePromoTransferDetails(string id, List<PromoTransferDetailsViewModel> detailsModel, int purReqId, int? toSbuId);
        Task<int> SavePromoReceiveDetails(string id, List<DepotPromoReceiveDetailsViewModel> detailsModel, int purReqId, int packetDistributionId);
        Task<int> SaveDepotPromoDistributionDetails(string id, List<DepotPromoDistributionDetailsViewModel> models, int prodTrnfrId, int depotPromoReceiveMasterId);
        Task<int> SavePromoPacketDetails(string id, List<PromoPacketingDetailsVM> models, int masterId);
        Task<int> SavePromoPacketNo(string id, List<PromoPacketNoDetailsVM> models, int masterId);
        Task<JsonViewModel> GetPromoTransferById(int? userId, int? prodTrnfrId);
        Task<JsonViewModel> GetPromoReceivedById(int? userId, int? prodTrnfrId);
        Task<JsonViewModel> GetPromoDistributionById(int? userId, int? prodTrnfrId);
        Task<JsonViewModel> GetProductSubCategoryByCategoryId(int? userId, int? productCatId);
        Task<JsonViewModel> GetPromoPacketById(int? userId, int? packetingMasterId);
        Task<JsonViewModel> GetPromoTransferDetailsByMasterId(int? purReqDetailsId);
        Task<JsonViewModel> GetPromoReceiveDetailsByMasterId(int? purReqDetailsId);
        Task<JsonViewModel> GetDepotPromoDistributionDetailsByMasterId(int? prodTrnfrId);
        Task<JsonViewModel> GetPromoPacketDetailsByMasterId(int? packetingMasterId);
        Task<JsonViewModel> GetPromoPacketNoDetailsByMasterId(int? packetingMasterId);
        Task<bool> DeletePromoTransferById(string id, int promoTrnfrId);
        Task<bool> DeletePromoReceiveById(string id, int promoTrnfrId);
        Task<bool> DeleteDepotPromoDistributionById(string id, int promoTrnfrId);
        Task<bool> DeletePromoPacketById(string id, int promoTrnfrId);
        Task<JsonViewModel> GetDepotCodeByTerritoryCode(string territoryCode);
        Task<JsonViewModel> GetAllTerritoryCodes(int? userId);
        Task<JsonViewModel> GetAllAreaCodes(int? userId);
        Task<JsonViewModel> GetAllRSMCode(int? userId);
        Task<JsonViewModel> GetAllProductCodes(int? userId);
        Task<JsonViewModel> GetPromoDisburseSummary(int? userId, DateTime fDate, DateTime tDate, string depotCode, string territoryCode);
        Task<bool> SetPromoRequisitionUpload(int? userId, PromoRequisitionProductUploadViewModel program);
        Task<bool> SetPromoRequisitionUploadDetails(int? userId, string DepotCode, string territoryCode, string productCode, decimal? quantity, string UploadId, string areaManagerCode, string rsmCode);

        #region Promo MOBILE API
        Task<JsonViewModel> GetAllDistributionNoByMIO(int employeeId);
        Task<JsonViewModel> GetAllDistributionNoByAM(int employeeId);
        Task<JsonViewModel> GetPacketItemsByDistributionId(int distributionId, int employeeId);
        Task<JsonViewModel> GetPacketItemsByDistributionIdForAM(int distributionId, int employeeId);
        //Task<JsonViewModel> GetPromoTerritotiesForPacketing(int distributionId, int employeeId);


        Task<int> TerritoryReceivePromoItems(string id, TerritoryPromoStockMasterModel model);
        Task<int> TerritoryReceivePromoItemDetails(string id, List<TerritoryPromoStockDetailsModel> detailsModel, int promoItemMasterId);


        Task<int> PromoTerritoryDisburseItems(string id, TerritoryPromoStockMasterModel model);
        Task<int> TerritoryDisbursePromoItemDetails(string id, List<TerritoryPromoStockDetailsModel> detailsModel, int promoItemMasterId);

        #endregion
        #region Promo report
        Task<JsonViewModel> PromoDisburseDetailsReport(int userId, DateTime fDate, DateTime tDate, string depotCode, string territoryCode);
        Task<JsonViewModel> PromoStockReport(int userId, DateTime? fDate, DateTime? tDate,int? productWiseSpecificationId);
        #endregion

        #region Balk Packeting
        Task<JsonViewModel> GetPromoTerritotiesForBulkPacketing(int employeeId, int promoRequisitionMasterId);
        #endregion

    }
}
