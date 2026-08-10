using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Sales.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Sales.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Sales
{
    public class SalesDistributionService : ISalesDistributionService
    {
        private readonly ERPDbContext _context;
        public SalesDistributionService(ERPDbContext context)
        {
            _context = context;
        }

        #region SalesDistribution Master

        public async Task<bool> DeleteSalesDistributionById(string id, int distributionMasterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteSalDistributionMaster {id}, {distributionMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<int> SaveSalesDistribution(string id, SalesDistributionMasterViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalDistributionMaster {id}, {model.distributionMasterId}, {model.distributionDate}, {model.vehicleNo}, {model.driverName}, {model.driverMobile}, {model.deliveryAddress}, {model.isActive}, {model.deliveryManName}, {model.deliveryManMobile}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<JsonViewModel> GetSalesDistributionById(int? distributionMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalDistributionMasterListJson {distributionMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetMaxSalesDistributionNumber(DateTime datetime)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetMaxSalDistributionNumberJson {datetime}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetDepoWiseSalesInvoiceList(int? depoId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetDepoWiseSalesInvoiceListJson {depoId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region SalesDistribution Details

        public async Task<JsonViewModel> GetSalesDistributionDetailsByInvoiceId(int? salesInvoiceId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalDistributionDetailBySalesInvoiceJson {salesInvoiceId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSalesDistributionDetailsByMasterId(int? distributionMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalDistributionDetailJson {distributionMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteSalesDistributionDetailsById(string id, int distributionDetailId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteSalDistributionDetail {id}, {distributionDetailId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<int> SaveSalesDistributionDetails(string id, List<SalesDistributionDetailsViewModel> models, int distributionMasterId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (var model in models)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalDistributionDetail {id},{model.distributionDetailId},{distributionMasterId},{model.salesInvoiceId},{model.salesInvDetailsId},{model.invoiceQty},{model.distributionQty},{model.isSelect}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        #endregion

        #region Approval

        public async Task<JsonViewModel> GetSalesDistributionApprovedList(string userId, int? distributionMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalDistributionMasterApprovedListJson {userId}, {distributionMasterId}").AsNoTracking().FirstOrDefaultAsync();

            return result;
        }
        public async Task<JsonViewModel> GetSalesDistributionListForApproval(string userId, int? distributionMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalDistributionMasterListForApprovalJson {userId}, {distributionMasterId}").AsNoTracking().FirstOrDefaultAsync();

            return result;
        }
        public async Task<int> ApproveSalesDistribution(string userId, List<SalesDistributionMasterViewModel> models, string approvalStatus)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (var model in models)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalDistributionMasterAproval {userId}, {model.distributionMasterId},{approvalStatus},{model.isSelect}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        #endregion

        #region Reports

        public async Task<JsonViewModel> GetSalesDistributionReportDataById(int? distributionMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalDistributionReportJson {distributionMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetDestructionReportById(int? userId, int? masterId, string rType, string depotCode, DateTime fDate, DateTime tDate)
        {
            var sql = $"SalSpGetDamageExpireTransferSummaryById {userId}, {masterId}, {rType}, {depotCode}, {fDate}, {tDate}";
            var result = await _context.jsonViewModels.FromSql($"SalSpGetDamageExpireTransferSummaryById {userId}, {masterId}, {rType}, {depotCode}, {fDate}, {tDate}").AsNoTracking().FirstOrDefaultAsync();
            //Console.Write($"SalSpGetDamageExpireTransferSummaryById {userId}, {masterId}, {rType}, {depotCode}, {fDate}, {tDate}");
            return result;
        }



        #endregion

        #region  miscellaneous item for Factory

        public async Task<int> SaveMiscellaneousItem(int? id, MiscellaneousItemViewModel model)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalMiscellaneousItem {id}, {model.miscellaneousItemId},{model.itemDate},{model.sbuId}, {model.miscellaneousNo}, {model.miscellaneousTypeId},{model.remarks}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetMiscellaneousItemById(int? id, int? miscellaneousItemId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalMiscellaneousItemJson {id},{miscellaneousItemId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveMiscellaneousItemDetails(int? id, List<MiscellaneousItemDetailsViewModel> models, int miscellaneousItemId)
        {
            var result = new SaveUpdateValueViewModel();
            //if (miscellaneousItemId > 0)
            //{
            //    await _context.saveUpdateValueViewModels.FromSql($"SalSpDeleteSalMiscellaneousItemDetails {id}, {miscellaneousItemId}").AsNoTracking().FirstOrDefaultAsync();
            //}
            foreach (var model in models)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalMiscellaneousItemDetails {id},{model.miscellaneousItemDetailsId}, {miscellaneousItemId},{model.productSpecificationId}, {model.ctnQty}, {model.looseQty}, {model.price},{model.remarks},{model.batchNo},{model.mgfDate},{model.expireDate}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetMiscellaneousItemDetailsByMasterId(int? id, int? miscellaneousItemId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalMiscellaneousItemWithDetailsByIdJson {id},{miscellaneousItemId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> DeleteMiscellaneousItem(int? id, int miscellaneousItemId)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpDeleteSalMiscellaneousItem {id},{miscellaneousItemId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<int> DeleteMiscellaneousItemDetails(int? id, int miscellaneousItemDetailsId)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpDeleteSalMiscellaneousItemDetails {id}, {miscellaneousItemDetailsId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetMaxMiscellaneousNumber(int? userId, DateTime datetime)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetMaxMiscellaneousNumberJson {userId}, {datetime}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        #endregion miscellaneous item   for factory

        #region  miscellaneous item  for depot

        public async Task<int> SaveMiscellaneousItemDepot(int? id, MiscellaneousItemViewModel model)
        {
            try
            {

                var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalMiscellaneousItemDepot {id}, {model.miscellaneousItemId},{model.itemDate},{model.sbuId}, {model.miscellaneousNo}, {model.miscellaneousTypeId},{model.remarks},{model.RePackProductTransferId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<JsonViewModel> GetMiscellaneousItemDepotById(int? id, int? miscellaneousItemId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalMiscellaneousItemDepotJson {id},{miscellaneousItemId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveMiscellaneousItemFileDepot(int? id, List<MiscellaneousItemFileViewModel> models, int miscellaneousItemId)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                //if (miscellaneousItemId > 0)
                //{
                //    await _context.saveUpdateValueViewModels.FromSql($"SalSpDeleteSalMiscellaneousItemDetailsDepot {id}, {miscellaneousItemId}").AsNoTracking().FirstOrDefaultAsync();
                //}
                foreach (var model in models)
                {
                    string[] res = model.imageFile.Split(',');
                    if (res.Length > 1)
                    {
                        try
                        {
                            Byte[] bytes = Convert.FromBase64String(res[1]);

                            string[] extention = res[0].Split("/");
                            string servePath = ("./wwwroot/MicellaneousFiles");
                            if (!System.IO.Directory.Exists(servePath)) System.IO.Directory.CreateDirectory(servePath);
                            string fileName = ($"{DateTime.Now.Ticks}.{extention[1].Replace(";base64", "")}");
                            string filePath = ($"{servePath}/{fileName}");
                            File.WriteAllBytes(filePath, bytes);

                            model.filePath = filePath;//fileName
                        }
                        catch (Exception)
                        {
                            model.filePath = null;//fileName
                            throw;
                        }
                    }

                    if (!string.IsNullOrEmpty(model.filePath))
                    {
                        result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetMiscellaneousItemFileDepot {id},{model.miscellaneousItemFileId}, {miscellaneousItemId},{model.docInfo},{model.filePath}").AsNoTracking().FirstOrDefaultAsync();
                    }
                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<int> SaveMiscellaneousItemDetailsDepot(int? id, List<MiscellaneousItemDetailsViewModel> models, int miscellaneousItemId)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                //if (miscellaneousItemId > 0)
                //{
                //    await _context.saveUpdateValueViewModels.FromSql($"SalSpDeleteSalMiscellaneousItemDetailsDepot {id}, {miscellaneousItemId}").AsNoTracking().FirstOrDefaultAsync();
                //}
                foreach (var model in models)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalMiscellaneousItemDetailsDepot {id},{model.miscellaneousItemDetailsId}, {miscellaneousItemId},{model.productSpecificationId}, {model.ctnQty}, {model.looseQty}, {model.price},{model.remarks},{model.batchNo},{model.mgfDate},{model.expireDate}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<JsonViewModel> GetMiscellaneousItemDetailsDepotByMasterId(int? id, int? miscellaneousItemId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalMiscellaneousItemWithDetailsDepotByIdJson {id},{miscellaneousItemId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> DeleteMiscellaneousItemDepot(int? id, int miscellaneousItemId)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpDeleteSalMiscellaneousItemDepot {id},{miscellaneousItemId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<int> DeleteMiscellaneousItemDetailsDepot(int? id, int miscellaneousItemId)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpDeleteSalMiscellaneousItemDetailsDepot {id}, {miscellaneousItemId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetMaxMiscellaneousNumberDepot(int? userId, DateTime datetime)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetMaxMiscellaneousNumberDepotJson {userId}, {datetime}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAllMiscellaneousType(int? userId, string param)
        {
            var result = await _context.jsonViewModels.FromSql($"GetAllMiscellaneousType {userId}, {param}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        #endregion miscellaneous item  for depot

        #region  miscellaneous item  for depot(Approval)
        public async Task<JsonViewModel> GetALLMiscellaneousItemDepotByApproval(int? id, int? isApproved)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetALLSalMiscellaneousItemDepotApprovalJson {id},{isApproved}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<int> SaveMiscellaneousItemForDepotApproval(int? userId, MiscellaneousItemApprovalViewModel model)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (MiscellaneousItemApprovalViewModel m in model.lstMasterViewModel)
                {
                    if (m.isSelect == true)
                    {
                        result = await _context.saveUpdateValueViewModels.FromSql($"SetMiscellaneousItemForDepotApproval {userId}, {m.miscellaneousItemId},{model.approvalStatusValue}").AsNoTracking().FirstOrDefaultAsync();
                    }
                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion miscellaneous item  for depot(Approval)

        public async Task<bool> SaveDealNotApplicableCustomerAndInstitute(string id, SalDealNotApplicableCustomerAndInstituteViewModel model)
        {
            try
            {

                var result = await _context.saveUpdateViewModels.FromSql($"SalSpSetDealNotApplicableCustomerAndInstitute {id}, {model.partyId},{model.bonusType},{model.customerType},{model.dealNotApplicableCustomerAndInstituteId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> getDealNotApplicableCustomerAndInstituteList(string userId, int dealNotApplicableCustomerAndInstituteId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetDealNotApplicableCustomerAndInstituteList {userId}, {dealNotApplicableCustomerAndInstituteId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
    }
}
