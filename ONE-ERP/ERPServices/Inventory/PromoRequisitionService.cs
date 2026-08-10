using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Inventory.Models;
using ONEERP.Areas.Sales.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Inventory.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Inventory
{
    public class PromoRequisitionService : IPromo
    {
        private readonly ERPDbContext _context;
        public PromoRequisitionService(ERPDbContext contex)
        {
            _context = contex;
        }
        public async Task<JsonViewModel> GetPromoRequisitionMaster()
        {
            var result = await _context.jsonViewModels.FromSql($"GetPromoRequisitionJSON").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeletePromoRequisitionById(int? userId, int promoRequisitionId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SpDeletePromoRequisitionById {userId}, {promoRequisitionId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetPromoReqDetails(string userId, int promoRequisitionId)
        {
            var result = await _context.jsonViewModels.FromSql($"GetPromoRequisitionDetailsByIdJSON {userId}, {promoRequisitionId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAllPacketBySbuId(int sbuId, int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"GetPromoPacketDistributionJSON {sbuId}, {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> TerritoryWisePromo(int userId, DateTime fDate, DateTime tDate, string territoryCode)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"TerritoryWisePromoJson {userId}, {fDate}, {tDate}, {territoryCode}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        public async Task<JsonViewModel> GetMaxPacketTransferNumberJson(DateTime dateTime)
        {
            var result = await _context.jsonViewModels.FromSql($"SpGetMaxPacketTransferNumberJson {dateTime}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetMaxReceivedTransferNumberJson(DateTime dateTime)
        {
            var result = await _context.jsonViewModels.FromSql($"GetMaxReceivedTransferNumberJson {dateTime}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetMaxDistributeTransferNumberJson(DateTime dateTime)
        {
            var result = await _context.jsonViewModels.FromSql($"GetMaxDistributeTransferNumberJson {dateTime}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetMaxPacketingMasterNo(int? employeeId, DateTime dateTime)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"GetMaxPacketingMasterNo {dateTime},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public async Task<int> SavePromoTransfer(string id, PromoTransferViewModel model)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"SpSetPromoTransfer {id},{model.promoTrnfId}, {model.packetDistributionDate}, {model.packetDistributionNo},{model.fromSbuId},{model.toSbuId},{model.fromStoreId},{model.Purpose}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<int> SavePromoReceive(string id, DepotPromoReceiveViewModel model)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"SpSetPromoReceive {id},{model.depotPromoReceiveMasterId}, {model.packetDistributionDate}, {model.promoReceivedNo},{model.fromSbuId},{model.purpose},{model.packetDistributionId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<int> SaveDepotPromoDistribution(string id, DepotPromoDistributionViewModel model)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"SpSetDepotPromoDistribution {id},{model.promoDistributionMasterId}, {model.promoDistributionDate}, {model.promoDistributionNo},{model.Purpose},{model.prodTrnfrId},{model.fromSbuId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<int> SavePromoPacketMaster(string id, PromoPacketingVM model)
        {
            try
            {
                if (model.packetingFor == "A")
                {
                    string sql = $"SpSetPromoPacketMaster {id},{model.packetingMasterId}, {model.packetingMasterDate}, {model.packetingMasterNo},{model.promoRequisitionId},NULL,{model.totalPacket},{model.packetNames},{model.refNo},{model.remarks},{model.territoryCode},{model.packetingFor},NULL";

                    var result = await _context.saveUpdateValueViewModels.FromSql($"SpSetPromoPacketMaster {id},{model.packetingMasterId}, {model.packetingMasterDate}, {model.packetingMasterNo},{model.promoRequisitionId},NULL,{model.totalPacket},{model.packetNames},{model.refNo},{model.remarks},{model.territoryCode},{model.packetingFor},NULL").AsNoTracking().FirstOrDefaultAsync();
                    return result.isSuccess;
                }
                if (model.packetingFor == "R")
                {
                    string sql = $"SpSetPromoPacketMaster {id},{model.packetingMasterId}, {model.packetingMasterDate}, {model.packetingMasterNo},{model.promoRequisitionId},NULL,{model.totalPacket},{model.packetNames},{model.refNo},{model.remarks},NULL,{model.packetingFor},{model.territoryCode}";

                    var result = await _context.saveUpdateValueViewModels.FromSql($"SpSetPromoPacketMaster {id},{model.packetingMasterId}, {model.packetingMasterDate}, {model.packetingMasterNo},{model.promoRequisitionId},NULL,{model.totalPacket},{model.packetNames},{model.refNo},{model.remarks},NULL,{model.packetingFor},{model.territoryCode}").AsNoTracking().FirstOrDefaultAsync();
                    return result.isSuccess;
                }
                else
                {
                    string sql = $"SpSetPromoPacketMaster {id},{model.packetingMasterId}, {model.packetingMasterDate}, {model.packetingMasterNo},{model.promoRequisitionId},{model.territoryCode},{model.totalPacket},{model.packetNames},{model.refNo},{model.remarks},NULL,{model.packetingFor},NULL";

                    var result = await _context.saveUpdateValueViewModels.FromSql($"SpSetPromoPacketMaster {id},{model.packetingMasterId}, {model.packetingMasterDate}, {model.packetingMasterNo},{model.promoRequisitionId},{model.territoryCode},{model.totalPacket},{model.packetNames},{model.refNo},{model.remarks},NULL,{model.packetingFor},NULL").AsNoTracking().FirstOrDefaultAsync();
                    return result.isSuccess;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
            
            
        }
        public async Task<int> SaveBulkPromoPacketMaster(string id, PromoBulkPacketingVM model)
        {
            try
            {
                int masterSaveCounter = 0;
                Dictionary<int, string> locationCodes = new Dictionary<int, string>();
                HashSet<string> distinctLocationCodes = new HashSet<string>();

                int i = 0;
                foreach (var item in model.allPacketListModel)
                {
                    if(item.transferQty > 0)
                    {
                        if (distinctLocationCodes.Add(item.locationCode))
                        {
                            locationCodes.Add(i, item.locationCode);
                            i++;
                        }
                    }
                    
                }

                foreach (var locationCode in locationCodes)
                {
                    int totalRowItems = model.allPacketListModel.Where(c => c.locationCode == locationCode.Value).Count();
                    var currentPacketDetails = model.allPacketListModel.Where(c => c.locationCode == locationCode.Value);
                    string packetName = "1";
                    if (model.packetingFor == "A")
                    {
                        string sql = $"SpSetPromoPacketMaster {id},{model.packetingMasterId}, {model.packetingMasterDate}, {$"PMN-{DateTime.Now.Date.ToString("yyMMdd")}-{masterSaveCounter + 1}"},{model.promoRequisitionId},NULL,{totalRowItems},{packetName},{model.refNo},{model.remarks},{locationCode.Value},{model.packetingFor},NULL";

                        var result = await _context.saveUpdateValueViewModels.FromSql($"SpSetPromoPacketMaster {id},{model.packetingMasterId}, {model.packetingMasterDate}, {$"PMNA-{DateTime.Now.Date.ToString("yyMMdd")}-{masterSaveCounter + 1}"},{model.promoRequisitionId},NULL,{totalRowItems},{packetName},{model.refNo},{model.remarks},{locationCode.Value},{model.packetingFor},NULL").AsNoTracking().FirstOrDefaultAsync();
                        //return result.isSuccess;
                        if (result.isSuccess > 0)
                        {
                            masterSaveCounter++;
                            List<PromoPacketingDetailsVM> packetDetails = new List<PromoPacketingDetailsVM>();
                            foreach(var item in currentPacketDetails)
                            {
                                var detailItem = new PromoPacketingDetailsVM()
                                {
                                    PacketingDetailId = 0,
                                    packetingMasterId = result.isSuccess,
                                    productWiseSpecificationId = item.productWiseSpecificationId,
                                    requisitionDetailId = await GetRequisionDetailsId(model.promoRequisitionId, locationCode.Value, item.productWiseSpecificationId, "A"),
                                    requisitionQty = null,
                                    transferQty = item.transferQty
                                };
                                packetDetails.Add(detailItem);
                            }
                            int isDeatisInserted = await SavePromoPacketDetails(id, packetDetails, result.isSuccess);
                            if (isDeatisInserted > 0)
                            {
                                int packet = 0;
                                var packetItem = new PromoPacketNoDetailsVM()
                                {
                                    packetingMasterId = result.isSuccess,
                                    packetNo = "1",
                                    PacketNoDetailId = 0,
                                    refNo = ""
                                };
                                List<PromoPacketNoDetailsVM> packetingDetails = new List<PromoPacketNoDetailsVM>();
                                packetingDetails.Add(packetItem);

                                packet = await SavePromoPacketNo(id, packetingDetails, result.isSuccess);
                            }


                        }

                    }
                    if (model.packetingFor == "R")
                    {
                        string sql = $"SpSetPromoPacketMaster {id},{model.packetingMasterId}, {model.packetingMasterDate}, {$"PMN-{DateTime.Now.Date.ToString("yyMMdd")}-{masterSaveCounter + 1}"},{model.promoRequisitionId},NULL,{totalRowItems},{packetName},{model.refNo},{model.remarks},NULL,{model.packetingFor},{locationCode.Value}";

                        var result = await _context.saveUpdateValueViewModels.FromSql($"SpSetPromoPacketMaster {id},{model.packetingMasterId}, {model.packetingMasterDate}, {$"PMNR-{DateTime.Now.Date.ToString("yyMMdd")}-{masterSaveCounter + 1}"},{model.promoRequisitionId},NULL,{totalRowItems},{packetName},{model.refNo},{model.remarks},NULL,{model.packetingFor},{locationCode.Value}").AsNoTracking().FirstOrDefaultAsync();
                        // return result.isSuccess;
                        if (result.isSuccess > 0)
                        {
                            masterSaveCounter++;
                            List<PromoPacketingDetailsVM> packetDetails = new List<PromoPacketingDetailsVM>();
                            foreach (var item in currentPacketDetails)
                            {
                                var detailItem = new PromoPacketingDetailsVM()
                                {
                                    PacketingDetailId = 0,
                                    packetingMasterId = result.isSuccess,
                                    productWiseSpecificationId = item.productWiseSpecificationId,
                                    requisitionDetailId = await GetRequisionDetailsId(model.promoRequisitionId, locationCode.Value, item.productWiseSpecificationId, "R"),
                                    requisitionQty = null,
                                    transferQty = item.transferQty
                                };
                                packetDetails.Add(detailItem);
                            }
                            int isDeatisInserted = await SavePromoPacketDetails(id, packetDetails, result.isSuccess);
                            if (isDeatisInserted > 0)
                            {
                                int packet = 0;
                                var packetItem = new PromoPacketNoDetailsVM()
                                {
                                    packetingMasterId = result.isSuccess,
                                    packetNo = "1",
                                    PacketNoDetailId = 0,
                                    refNo = ""
                                };
                                List<PromoPacketNoDetailsVM> packetingDetails = new List<PromoPacketNoDetailsVM>();
                                packetingDetails.Add(packetItem);

                                packet = await SavePromoPacketNo(id, packetingDetails, result.isSuccess);
                               
                            }


                        }
                    }
                    else
                    {
                        string sql = $"SpSetPromoPacketMaster {id},{model.packetingMasterId}, {model.packetingMasterDate}, {model.packetingMasterNo},{model.promoRequisitionId},{locationCode.Value},{totalRowItems},{packetName},{model.refNo},{model.remarks},NULL,{model.packetingFor},NULL";

                        var result = await _context.saveUpdateValueViewModels.FromSql($"SpSetPromoPacketMaster {id},{model.packetingMasterId}, {model.packetingMasterDate}, {$"PMNT-{DateTime.Now.Date.ToString("yyMMdd")}-{masterSaveCounter+1}"},{model.promoRequisitionId},{locationCode.Value},{totalRowItems},{packetName},{model.refNo},{model.remarks},NULL,{model.packetingFor},NULL").AsNoTracking().FirstOrDefaultAsync();
                        // return result.isSuccess;
                        if (result.isSuccess > 0)
                        {
                            masterSaveCounter++;
                            List<PromoPacketingDetailsVM> packetDetails = new List<PromoPacketingDetailsVM>();
                            foreach (var item in currentPacketDetails)
                            {
                                var detailItem = new PromoPacketingDetailsVM()
                                {
                                    PacketingDetailId = 0,
                                    packetingMasterId = result.isSuccess,
                                    productWiseSpecificationId = item.productWiseSpecificationId,
                                    requisitionDetailId = await GetRequisionDetailsId(model.promoRequisitionId, locationCode.Value, item.productWiseSpecificationId, "T"),
                                    requisitionQty = null,
                                    transferQty = item.transferQty
                                };
                                packetDetails.Add(detailItem);
                            }
                            int isDeatisInserted = await SavePromoPacketDetails(id, packetDetails, result.isSuccess);
                            if (isDeatisInserted > 0)
                            {
                                int packet = 0;
                                var packetItem = new PromoPacketNoDetailsVM()
                                {
                                    packetingMasterId = result.isSuccess,
                                    packetNo = "1",
                                    PacketNoDetailId = 0,
                                    refNo = ""
                                };
                                List<PromoPacketNoDetailsVM> packetingDetails = new List<PromoPacketNoDetailsVM>();
                                packetingDetails.Add(packetItem);

                                packet = await SavePromoPacketNo(id, packetingDetails, result.isSuccess);
                                
                            }


                        }
                    }
                }

                return masterSaveCounter;

            }
            catch (Exception ex)
            {

                return 0;
            }


        }
        public async Task<int> SavePromoTransferDetails(string id, List<PromoTransferDetailsViewModel> models, int prodTrnfrId, int? toSbuId)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (PromoTransferDetailsViewModel model in models)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetPromoTransferDetails {id},{model.packetDistributionDetailsId},{prodTrnfrId},{model.territoryCode},{model.transferQuantity}, {toSbuId},{model.packetingMasterId}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {

                return 0;
            }
            
        }
        public async Task<int> SavePromoReceiveDetails(string id, List<DepotPromoReceiveDetailsViewModel> models, int prodTrnfrId, int packetDistributionId)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (DepotPromoReceiveDetailsViewModel model in models)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"SpSetPromoReceiveDetails {id},{model.promoReceiveDetailsId},{prodTrnfrId},{model.territoryCode},{model.transferQuantity},{packetDistributionId}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {

                return 0;
            }
            
        }
        public async Task<int> SaveDepotPromoDistributionDetails(string id, List<DepotPromoDistributionDetailsViewModel> models, int prodTrnfrId, int depotPromoReceiveMasterId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (DepotPromoDistributionDetailsViewModel model in models)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"SpSetDepotPromoReceiveDetails {id},{model.distributionDetailsId},{prodTrnfrId},{model.territoryCode},{model.transferQuantity},{depotPromoReceiveMasterId}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }
        public async Task<int> SavePromoPacketDetails(string id, List<PromoPacketingDetailsVM> models, int masterId)
        {
            var result = new SaveUpdateValueViewModel();

            foreach (PromoPacketingDetailsVM model in models)
            {
                if (model.transferQty > 0)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"SpSetPromoPacketDetails {id},{model.PacketingDetailId},{masterId},{model.productWiseSpecificationId},{model.requisitionDetailId},{model.requisitionQty},{model.transferQty}").AsNoTracking().FirstOrDefaultAsync();
                }
                
            }
            return result.isSuccess;
        }
        public async Task<int> SavePromoPacketNo(string id, List<PromoPacketNoDetailsVM> models, int masterId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (PromoPacketNoDetailsVM model in models)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"SpSetPromoPacketNoDetails { id},{model.PacketNoDetailId},{masterId},{model.packetNo},{model.refNo}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetPromoTransferById(int? userId, int? prodTrnfrId)
        {
            var result = await _context.jsonViewModels.FromSql($"SpGetPromoTransferJSON {userId}, {prodTrnfrId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetPromoReceivedById(int? userId, int? prodTrnfrId)
        {
            var result = await _context.jsonViewModels.FromSql($"SpGetPromoReceivedJSON {userId}, {prodTrnfrId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetPromoDistributionById(int? userId, int? prodTrnfrId)
        {
            var result = await _context.jsonViewModels.FromSql($"SpGetPromoDistributionJSON {userId}, {prodTrnfrId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetPromoPacketById(int? userId, int? packetingMasterId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"SpGetPromoPacketByIdJSON {userId}, {packetingMasterId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public async Task<JsonViewModel> GetDepotCodeByTerritoryCode(string territoryCode)
        {
            var result = await _context.jsonViewModels.FromSql($"SpGetDepotCodeJSON {territoryCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetPromoTransferDetailsByMasterId(int? prodTrnfrId)
        {
            var result = await _context.jsonViewModels.FromSql($"SpGetPromoTransferDetailsJson {prodTrnfrId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetPromoReceiveDetailsByMasterId(int? prodTrnfrId)
        {
            var result = await _context.jsonViewModels.FromSql($"SpGetPromoReceiveDetailsJson {prodTrnfrId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetDepotPromoDistributionDetailsByMasterId(int? prodTrnfrId)
        {
            var result = await _context.jsonViewModels.FromSql($"SpGetDepotPromoDistributeDetailsJson {prodTrnfrId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetPromoPacketDetailsByMasterId(int? packetingMasterId)
        {   
            var result = await _context.jsonViewModels.FromSql($"SpGetPromoPacketDetailsJson {packetingMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetPromoPacketNoDetailsByMasterId(int? packetingMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"SpGetPromoPacketNoDetailsJson {packetingMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> getDistribution(int sbuId)
        {
            var result = await _context.jsonViewModels.FromSql($"SpGetDistributionJson {sbuId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> getReceived(int sbuId)
        {
            var result = await _context.jsonViewModels.FromSql($"SpGetReceivedJson {sbuId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> getRequisition(string userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SpGetRequisition {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetTerritoryByRequisition(int requisitionId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"SpGetTerritoryByReq {requisitionId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        public async Task<JsonViewModel> GetAreaManagerCodeByRequisition(int requisitionId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"SpGetAreaByPromoRequisition {requisitionId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
        public async Task<JsonViewModel> GetRSMCodeByRequisition(int requisitionId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"SpGetRegionCodeByPromoRequisition {requisitionId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
        public async Task<JsonViewModel> GetProductReqDetails(int? userId, string territoryCode,int requisitionId, string allocationType)
        {
            try
            {
                territoryCode = territoryCode == "null" ? null : territoryCode;
                var result = await _context.jsonViewModels.FromSql($"SpGetProductReqDetails {userId},{territoryCode}, {requisitionId},{allocationType}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                return new JsonViewModel();
            }
            
        }
        public async Task<JsonViewModel> GetAllPacketByDistribution(int distributionId)
        {
            var result = await _context.jsonViewModels.FromSql($"SpGetAllPacketByDistribution {distributionId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAllPacketByReceived(int distributionId)
        {
            var result = await _context.jsonViewModels.FromSql($"SpGetAllPacketByReceived {distributionId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeletePromoTransferById(string id, int promoTrnfrId)
        {
            try
            {

                var result = await _context.saveUpdateViewModels.FromSql($"SpDeletePromoTransfer {id}, {promoTrnfrId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeletePromoReceiveById(string id, int promoTrnfrId)
        {
            try
            {

                var result = await _context.saveUpdateViewModels.FromSql($"SpDeletePromoReceive {id}, {promoTrnfrId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeleteDepotPromoDistributionById(string id, int promoTrnfrId)
        {
            try
            {

                var result = await _context.saveUpdateViewModels.FromSql($"SpDepotDeletePromoDistribution {id}, {promoTrnfrId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeletePromoPacketById(string id, int promoTrnfrId)
        {
            try
            {

                var result = await _context.saveUpdateViewModels.FromSql($"SpDeletePromoPacket {id}, {promoTrnfrId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<JsonViewModel> GetAllTerritoryCodes(int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"promoSpGetAllTerritoryCodes {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAllAreaCodes(int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetAllAreacodes {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAllRSMCode(int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetAllRSMCodes {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAllProductCodes(int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"promoSpGetAllProductCodes {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetProductSubCategoryByCategoryId(int? userId, int? productCatId)
        {
            var result = await _context.jsonViewModels.FromSql($"SpGetProductSubCategoryJSON {userId}, {productCatId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetPromoDisburseSummary(int? userId, DateTime fDate, DateTime tDate, string depotCode, string territoryCode)
        {
            var result = await _context.jsonViewModels.FromSql($"SpGetPromoDisburseSummaryReportJSON {userId},{fDate}, {tDate}, {depotCode},{territoryCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }


        #region Promo MOBILE API
        public async Task<JsonViewModel> GetAllDistributionNoByMIO(int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"promoSpGetAllDistributionNoByMIO {employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAllDistributionNoByAM(int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"promoSpGetAllDistributionNoByAM {employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetPacketItemsByDistributionId(int distributionId, int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"promoSpGetPacketItemsByDistributionId {distributionId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetPacketItemsByDistributionIdForAM(int distributionId, int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"promoSpGetPacketItemsByDistributionId {distributionId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> TerritoryReceivePromoItems(string id, TerritoryPromoStockMasterModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"promoSpSetTerritoryPromoItemStockReceive {id},{model.territoryStockMasterId}, {model.promoStockDate}, {model.promoStockNo}, {1}, {model.distributionMasterId}, {model.territoryCode},{model.stockFor} ").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception)
            {

                return 0;
            }
          
        }
        public async Task<int> TerritoryReceivePromoItemDetails(string id, List<TerritoryPromoStockDetailsModel> detailsModel, int promoItemMasterId)
        {

            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (TerritoryPromoStockDetailsModel model in detailsModel)
                {
                    if (model.receivedQty > 0)
                    {
                        result = await _context.saveUpdateValueViewModels.FromSql($"promoSpSetTerritoryPromoItemStockReceiveDetails {id},{model.territoryStockDetailId},{promoItemMasterId},{model.packetingDetailId},{model.productWiseSpecificationId},{model.receivedQty}").AsNoTracking().FirstOrDefaultAsync();
                    }

                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {

                return 0;
            }
           
        }


        public async Task<int> PromoTerritoryDisburseItems(string id, TerritoryPromoStockMasterModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"promoSpSetTerritoryDisbursePromoItem  {id},{model.territoryStockMasterId}, {model.promoStockDate}, {model.promoStockNo}, {2}, {model.doctorScheduleId}, {model.chemistScheduleId},{model.territoryCode}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception)
            {

                return 0;
            }

        }
        public async Task<int> TerritoryDisbursePromoItemDetails(string id, List<TerritoryPromoStockDetailsModel> detailsModel, int promoItemMasterId)
        {

            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (TerritoryPromoStockDetailsModel model in detailsModel)
                {
                    if (model.stockOutQty > 0)
                    {
                        result = await _context.saveUpdateValueViewModels.FromSql($"promoSpSetTerritoryDisbursePromoItemDetails {id},{model.territoryStockDetailId},{promoItemMasterId}, {model.productWiseSpecificationId},{model.stockOutQty}").AsNoTracking().FirstOrDefaultAsync();
                    }

                }
                return result.isSuccess;
            }
            catch (Exception)
            {

                return 0;
            }

        }

        public async Task<JsonViewModel> PromoDisburseDetailsReport(int userId, DateTime fDate, DateTime tDate, string depotCode, string territoryCode)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"SpGetPromoDisburseDetailsJson {userId},{fDate},{tDate},{depotCode},{territoryCode}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        public async Task<JsonViewModel> PromoStockReport(int userId, DateTime? fDate, DateTime? tDate, int? productWiseSpecificationId)
        {
            try
            {
                string specId = null;
                if(productWiseSpecificationId == null || productWiseSpecificationId == 0)
                {
                    specId = null;
                }
                else
                {
                    specId = productWiseSpecificationId.ToString();
                }
                    

                var result = await _context.jsonViewModels.FromSql($"InvSpGetPromoStockReportJSON {userId},{fDate},{tDate},{specId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> SetPromoRequisitionUploadDetails(int? userId, string DepotCode, string territoryCode, string productCode, decimal? quantity, string UploadId, string areaManagerCode, string rsmCode)
        {
            int Id = int.Parse(UploadId);
            var result = await _context.saveUpdateViewModels.FromSql($"InsertPromoRequisitionUploadDetails {UploadId}, {DepotCode}, {territoryCode}, {productCode}, {quantity}, {userId}, {areaManagerCode},{rsmCode}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<bool> SetPromoRequisitionUpload(int? userId, PromoRequisitionProductUploadViewModel models)
        {
            try
            {
                JsonViewModel data = await SetPromoRequisitionUpload(userId, models.program, models.allocationTypeId);
                dynamic jsonData = JsonConvert.DeserializeObject(data.data);
                string masterUploadId = jsonData[0]["UploadMasterId"].ToString();
                if(models.allocationTypeId == "A")
                {
                    foreach (var item in models.lstDetailsViewModel)
                    {
                        var result = await SetPromoRequisitionUploadDetails(userId, item.depotCode, null, item.productCode, item.quantity, masterUploadId, item.territoryCode, null);// here territoryCode is actually areaManagerCode
                    }
                    return true;
                }
                if (models.allocationTypeId == "R")
                {
                    foreach (var item in models.lstDetailsViewModel)
                    {
                        var result = await SetPromoRequisitionUploadDetails(userId, item.depotCode, null, item.productCode, item.quantity, masterUploadId,null,item.territoryCode);// here territoryCode is actually areaManagerCode
                    }
                    return true;
                }
                else
                {
                    foreach (var item in models.lstDetailsViewModel)
                    {
                        var result = await SetPromoRequisitionUploadDetails(userId, item.depotCode, item.territoryCode, item.productCode, item.quantity, masterUploadId, null,null);
                    }
                    return true;
                }
                
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<JsonViewModel> SetPromoRequisitionUpload(int? userId, string program, string allocationType)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InsertPromoRequisitionUploadMaster {userId}, {program}, {allocationType}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Balk Packeting
        public async Task<JsonViewModel> GetPromoTerritotiesForBulkPacketing(int employeeId, int promoRequisitionMasterId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetPromoRequisiton {employeeId}, {promoRequisitionMasterId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        private async Task<int> GetRequisionDetailsId(int requMasterId, string locationCode, int productWiseSpecId, string requFor)
        {
            if(requFor == "T")
            {
                var details = await _context.PromoRequisitionUploadDetails.Where(x => x.promoRequisitionMasterId == requMasterId && x.productWiseSpecificationId == productWiseSpecId && x.territoryCode == locationCode).FirstOrDefaultAsync();
                return details.promoRequisitionDetailsId;
            }
            if (requFor == "A")
            {
                var details = await _context.PromoRequisitionUploadDetails.Where(x => x.promoRequisitionMasterId == requMasterId && x.productWiseSpecificationId == productWiseSpecId && x.areaManagerCode == locationCode).FirstOrDefaultAsync();
                return details.promoRequisitionDetailsId;
            }
            if (requFor == "R")
            {
                var details = await _context.PromoRequisitionUploadDetails.Where(x => x.promoRequisitionMasterId == requMasterId && x.productWiseSpecificationId == productWiseSpecId && x.regionCode == locationCode).FirstOrDefaultAsync();
                return details.promoRequisitionDetailsId;
            }
            return 0;

        }
        
    }
}
