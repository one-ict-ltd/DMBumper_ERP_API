using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Sales.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Sales.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text.Json;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel; // For .xlsx files (Excel 2007+)
using NPOI.HSSF.UserModel; // For .xls files (Excel 97-2003)
using NPOI.SS.Util;
using NPOI.SS.Formula.Functions;
using System.IO;
using System.Linq;
using System.Text;

namespace ONEERP.ERPServices.Sales
{
    public class SalesInvoiceService : ISalesInvoiceService
    {
        private readonly ERPDbContext _context;
        private MemoryStream _myMemory;
        public SalesInvoiceService(ERPDbContext context)
        {
            _context = context;
        }

        #region SalesInvoiceService Master

        public async Task<bool> DeleteSalesInvoiceById(string id, int salesInvoiceId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteSalesInvoice {id}, {salesInvoiceId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<bool> DeleteGDNById(string id, int salesInvoiceId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteGDNConfirmation {id}, {salesInvoiceId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<bool> DeleteSalesPicking(int? userId, int pickingMasterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteSalesPicking {userId}, {pickingMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<bool> DeleteDispatch(int? userId, int masterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteDispatch {userId}, {masterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetSalesInvoiceById(int? salesInvoiceId, int? userId, DateTime? fDate, DateTime? tDate)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesInvoiceJSON {salesInvoiceId},{userId},{fDate},{tDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetInvoiceGDNConfirmation(int? salesInvoiceId, int? userId, DateTime? fDate, DateTime? tDate, int gdnType)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetInvoiceGDNConfirmationJSONData {salesInvoiceId},{userId},{fDate},{tDate},{gdnType}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetSalesInvoiceForPosById(int? salesInvoiceId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesInvoiceForPosJSON {salesInvoiceId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetCurrentStock(int storeId, int productWiseSpecificationId, string batchNo)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetGetCurrentStockJSON {storeId},{productWiseSpecificationId},{batchNo}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetProductBatch(int storeId, int productWiseSpecificationId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetProductBatchJSON {storeId},{productWiseSpecificationId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> SetCurrentStock(int storeId, string productCode, decimal ProposedStockQty, string batchNo)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpSetCurrentStock {storeId},{productCode},{ProposedStockQty},{batchNo}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetItemWsieBonus(int? partyId, int? productWiseSpecificationId, DateTime? invoiceDate, decimal? invQty)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetItemWsieBonusJSON {invoiceDate},{partyId},{productWiseSpecificationId},{invQty}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetCollectionDiscountNotApplicableProductList(int? userId, int? partyId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetCollectionDiscountNotApplicableProductList {userId},{partyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetMaxSalesInvoiceNumber(int userId, DateTime datetime)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetMaxSalesInvoiceNumberJSON {userId},{datetime}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<string> ValidateCurrentStockForOrder(int? userId, int orderId, int? storeId, int? productWiseSpecificationId, decimal? invoiceQty)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"SalSpValidateCurrentStock {userId},{orderId},{storeId},{productWiseSpecificationId},{invoiceQty}").AsNoTracking().FirstOrDefaultAsync();
                return result.data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<string> ValidateCustomerDuesStatusForOrder(int? userId, int orderId)
        {
            string msg = "";
            try
            {
                var res = await _context.jsonViewModels.FromSql($"SalSpValidateCustomerDuesStatusForOrder {userId},{orderId}").AsNoTracking().FirstOrDefaultAsync();
                //result.data;

                var DuesStatus = JsonConvert.DeserializeObject<List<ValidateCustomerDuesStatusViewModel>>(res.data);
                if (DuesStatus != null && DuesStatus.Count > 0)
                {
                    if (DuesStatus[0].creditLimitCrossed.ToUpper() == "YES")
                    {
                        msg = $"Credit Limit Crossed !";
                    }
                    if (DuesStatus[0].overDuesStatus.ToUpper() == "YES")
                    {
                        msg = $"This Customer has over dues invoice !";
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "Something went wrong!";
            }
            return msg;
        }
        public async Task<int> GenerateSalesInvoiceBySalesOrder(int? userId, GenerateInvoiceViewModel models)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {
                foreach (ApprovedSalesOrderViewModel model in models.lstApprovedOrderList)
                {
                    if (model.isSelect == true)
                    {
                        result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetGenerateSalesInvoiceBySalesOrder {userId},{model.salesOrderId}").AsNoTracking().FirstOrDefaultAsync();
                    }

                    var SalesInvoiceId = result.isSuccess;
                    foreach (SalesCreditNoteViewModel m in model.lstCreditNoteViewModel)
                    {
                        if ((bool)m.isSelect && SalesInvoiceId > 0)
                        {
                            var crn = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetProductExpireReturnInvoiceUpdate {userId},{m.productExpireReturnId},{SalesInvoiceId}").AsNoTracking().FirstOrDefaultAsync();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                result.isSuccess = 0;
                throw;
            }
            return result.isSuccess;
        }

        public async Task<int> GenerateSalesInvoiceBySalesOrder_v2(int? userId, GenerateInvoiceViewModel models)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {
                foreach (ApprovedSalesOrderViewModel model in models.lstApprovedOrderList)
                {
                    if (model.isSelect == true)
                    {
                        result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetGenerateSalesInvoiceBySalesOrder_v2 {userId},{model.salesOrderId}").AsNoTracking().FirstOrDefaultAsync();
                    }

                    var SalesInvoiceId = result.isSuccess;
                    foreach (SalesCreditNoteViewModel m in model.lstCreditNoteViewModel)
                    {
                        if ((bool)m.isSelect && SalesInvoiceId > 0)
                        {
                            var crn = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetProductExpireReturnInvoiceUpdate {userId},{m.productExpireReturnId},{SalesInvoiceId}").AsNoTracking().FirstOrDefaultAsync();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                result.isSuccess = 0;
            }
            return result.isSuccess;
        }

        public async Task<int> SaveSalesInvoice(string id, SalesInvoiceViewModel model)
        {
            try
            {
                //var str = $"SalSpSetSalesInvoice {id}, {model.salesInvoiceId}, {model.salesInvoiceNo}, {model.salesInvoiceDate}, {model.paymentDate}, {model.storeId}, {model.partyId}, {model.mobileNo}, {model.alternateMobileNo}, {model.address}, {model.totalGross}, {model.totalVat}, {model.totalAit}, {model.shippingCost}, {model.totalDiscountAmount}, {model.grandTotal}, {model.approvalStatus}, {model.isActive}, {0}, {0}, {model.refNo}, {model.transactionTypeId}";

                var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesInvoice {id}, {model.salesInvoiceId}, {model.salesInvoiceNo}, {model.salesInvoiceDate}, {model.paymentDate}, {model.storeId}, {model.partyId}, {model.mobileNo}, {model.alternateMobileNo}, {model.address}, {model.totalGross}, {model.totalVat}, {model.totalAit}, {model.shippingCost}, {model.totalDiscountAmount}, {model.grandTotal}, {model.approvalStatus}, {model.isActive}, {0}, {0}, {model.refNo}, {model.transactionTypeId}").AsNoTracking().FirstOrDefaultAsync();


                foreach (SalesCreditNoteViewModel m in model.lstCreditNoteViewModel)
                {
                    var SalesInvoiceId = result.isSuccess;
                    if ((bool)m.isSelect && SalesInvoiceId > 0)
                    {
                        var crn = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetProductExpireReturnInvoiceUpdate {id},{m.productExpireReturnId},{SalesInvoiceId}").AsNoTracking().FirstOrDefaultAsync();
                    }
                }

                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        public async Task<int> SaveMoneyReceiptNote(string id, MoneyReceiptNoteViewModel m)
        {
            try
            {
                //var str = $"SalSpSetMoneyReceiptNote {id}, {m.moneyReceiptId},{ m.moneyReceiptNo},{ m.moneyReceiptDate},{ m.depotCode},{ m.territoryCode},{ m.mioCode},{ m.receivedFromPerson},{ m.remarks},{ m.mrTypeId},{ m.amount},{ m.paymentModeId},{ m.chequeNo},{ m.chequeDate},{ m.trxNo},{ m.bankName},{ m.branchName}";

                var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetMoneyReceiptNote {id}, {m.moneyReceiptId},{m.moneyReceiptNo},{m.moneyReceiptDate},{m.depotCode},{m.territoryCode},{m.mioCode},{m.receivedFromPerson},{m.remarks},{m.mrTypeId},{m.amount},{m.paymentModeId},{m.chequeNo},{m.chequeDate},{m.trxNo},{m.bankName},{m.branchName}").AsNoTracking().FirstOrDefaultAsync();

                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<JsonViewModel> ValidateMoneyReceiptNoteTrxnNo(int? userId, string trxnNo)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"SalSpValidateMoneyReceiptTransactionNo {userId}, {trxnNo}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                JsonViewModel jv = new JsonViewModel();
                jv.data = "Something went wrong!";
                return jv;
            }
        }
        public async Task<int> SaveMoneyReceipt(string id, MoneyReceiptViewModel m)
        {
            try
            {
                //var str = $"SalSpSetMoneyReceiptNote {id}, {m.moneyReceiptId},{ m.moneyReceiptNo},{ m.moneyReceiptDate},{ m.depotCode},{ m.territoryCode},{ m.mioCode},{ m.receivedFromPerson},{ m.remarks},{ m.mrTypeId},{ m.amount},{ m.paymentModeId},{ m.chequeNo},{ m.chequeDate},{ m.trxNo},{ m.bankName},{ m.branchName}";

                var result = await _context.saveUpdateValueViewModels.FromSql($"SpSetMoneyReceipt {id}, {m.moneyReceiptId},{m.receiptNo}, {m.moneyReceiptDate},{m.depotCode},{m.territoryCode},{m.mioCode},{m.mrTypeId}, {m.moneyBook}").AsNoTracking().FirstOrDefaultAsync();

                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<int> DeleteMoneyReceiptDetails(int masterId)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"DeleteMoneyReceiptDetails {masterId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<int> SaveMoneyReceiptDetails(string id, List<MoneyReceiptDetailsViewModel> models, int masterId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (MoneyReceiptDetailsViewModel model in models)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"SpSetMoneyReceiptDetails {id},{model.moneyReceiptDetailsId},{model.number}, {model.isSet}, {masterId}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<int> SaveSalesPicking(string id, int PickingMasterId, DateTime? pickingDate)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"SetSalesPicking {id}, {PickingMasterId}, {pickingDate}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<int> SaveDamageExpireProductsReturn(string id, int? damageExpireProductReturnMasterId, int? miscellaneousTypeId, DateTime? date, string MarketOrDepo)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"SetDamageExpireProductsReturn {id}, {damageExpireProductReturnMasterId},{miscellaneousTypeId},{date},{MarketOrDepo}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<int> SaveSalesDispatch(string id, int dispatchMasterId, int? employeeId, DateTime? date)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"SetSalesDispatch {id}, {dispatchMasterId},{employeeId},{date}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetSalesDispatchDetailsbyId(int distributionMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGeGetSalesDispatchDetailsbyId {distributionMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveSalesPickingSammary(string id, int PickingMasterId, int salesInvoiceId)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"SetSalesPickingSammary {id}, {PickingMasterId},{salesInvoiceId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<int> SaveSalesDispatchDetails(string id, int dispatchMasterId, int pickingMasterId, int? salesInvoiceId)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"SetSalesDispatchDetails {id}, {dispatchMasterId},{pickingMasterId},{salesInvoiceId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<int> SaveDamageExpireProductsReturnDetails(string id, int damageExpireProductReturnMasterId, int MiscellaneousItemDetailId, decimal qty, int productSpecificationId)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"SetDamageExpireProductsReturnDetails {id}, {damageExpireProductReturnMasterId},{MiscellaneousItemDetailId},{qty},{productSpecificationId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<int> DestructionNoteApproval(int? userId, DestructionNoteApprovalViewModel model)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (DestructionNoteApprovalViewModel m in model.lstMasterViewModel)
                {
                    if (m.isSelect == true)
                    {
                        result = await _context.saveUpdateValueViewModels.FromSql($"SetDestructionNoteApproval {userId}, {m.damageExpireProductReturnMasterId},{model.approvalStatusValue}").AsNoTracking().FirstOrDefaultAsync();
                    }
                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveSalesPickingDetails(string id, int PickingMasterId, int productWiseSpecificationId, decimal? invoiceQty)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"SetSalesPickingDetails {id}, {PickingMasterId},{productWiseSpecificationId},{invoiceQty}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<int> SaveInvoiceGDNConfirmation(string id, string salesInvoiceIds, int gdnType)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"SetSalesInvoiceGDNConfirmation {id}, {salesInvoiceIds},{gdnType}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }

        public async Task<int> SaveGDNConfirmationLogs(string id, string salesInvoiceIds)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"SetGDNConfirmationLogs {id}, {salesInvoiceIds}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetProductSerialNoByProductSpec(int productWiseSpecificationId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSerialNoByProductSpec {productWiseSpecificationId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSalesInvoiceAmountById(int salesInvoiceId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesInvoiceAmountById {salesInvoiceId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetSalesInvoiceByPartyId(int partyId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesInvoiceByPartyId {partyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetMoneyReceiptType(int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetMoneyReceiptType {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetMaxMoneyReceiptNo(int? userId, DateTime? invoiceDate)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetMaxMoneyReceiptNo {userId}, {invoiceDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAllMoneyReceiptNote(int? userId, int? masterId, DateTime? fdate, DateTime? tdate)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetAllMoneyReceiptNote {userId}, {masterId}, {fdate}, {tdate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAllMoneyReceipt(int? userId, int? masterId, DateTime? fdate, DateTime? tdate)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetAllMoneyReceipt {userId}, {masterId}, {fdate}, {tdate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAllMoneyReceiptDetails(int? masterId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetAllMoneyReceiptDetails  {masterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetAllPendingMoneyRecipts(int? userId, string territoryCode, string mioCode)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetAllPendingMoneyRecipts {userId}, {territoryCode}, {mioCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAllPendingMoneyReciptsNew()
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetAllPendingMoneyReciptsNew").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAllPendingMoneyReciptsForBill(int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetAllPendingMoneyReciptsForBill {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region Sales Invoice Details
        public async Task<bool> DeleteSalesInvoiceDetailsById(string id, int salesInvDetailsId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteSalesInvoiceDetails {id}, {salesInvDetailsId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeleteMoneyReceiptNoteById(int? userId, int masterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteMoneyReceiptNoteById {userId}, {masterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<bool> DeleteMoneyReceiptById(int? userId, int masterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteMoneyReceiptById {userId}, {masterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetSalesInvoiceDetailsByMasterId(int? salesInvoiceId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesInvoiceDetailsJSON {salesInvoiceId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetAllPartysByTypeId(int userId, int? partyTypeId, int? sbuId, string territoryCode)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetAllPartysByTypeJSON {userId},{partyTypeId}, {sbuId}, {territoryCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAllActivePartysByTypeId(int userId, int? partyTypeId, int? sbuId, string territoryCode)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetAllActivePartysByTypeJSON {userId},{partyTypeId}, {sbuId}, {territoryCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAllActivePartysForChallanByTypeId(int userId, int? partyTypeId, int? sbuId, string territoryCode)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetAllActivePartysForChallanByTypeJSON {userId},{partyTypeId}, {sbuId}, {territoryCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAllActivePartysForBillByTypeId(int userId, int? partyTypeId, int? sbuId, string territoryCode)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetAllActivePartysForBillByTypeJSON {userId},{partyTypeId}, {sbuId}, {territoryCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAllMIOByTerritory(int? userId, string territoryCode)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetAllMIOByTerritoryJSON {userId},{territoryCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }



        //public async Task<JsonViewModel> GetAllPartysByTypeId(int? partyTypeId, int? sbuId)
        //{
        //    var result = await _context.jsonViewModels.FromSql($"SalSpGetAllPartysByTypeJSON {partyTypeId}, {sbuId}").AsNoTracking().FirstOrDefaultAsync();
        //    return result;
        //}
        public async Task<JsonViewModel> GetPartyDetailsById(int? partyId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetPartyDetailsByIdJSON {partyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetProductSpecDetailsBySpecId(int? productSpecId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetProductSpecDetailsBySpecIdJSON {productSpecId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<string> GetValidateProductStockForInvoice(string userId, int? storeId, int? productWiseSpecificationId, string batchNo, decimal? invoiceQty, int? partyId, DateTime? salesInvoiceDate, bool? hasNationalBonus)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"salSpGetValidateProductStockForInvoice {userId},{storeId},{productWiseSpecificationId},{batchNo},{invoiceQty},{partyId},{salesInvoiceDate},{hasNationalBonus}").AsNoTracking().FirstOrDefaultAsync();
                return result.data;
            }
            catch (Exception ex)
            {
                return "Validation Process Failed!";
            }
        }

        public async Task<int> SaveSalesInvoiceDetails(string id, List<SalesInvoiceDetailsViewModel> models, int salesInvoiceId, int storeId, int companyId)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {
                result.isSuccess = 1;
                foreach (SalesInvoiceDetailsViewModel model in models)
                {
                    //var str = $"SalSpSetSalesInvoiceDetails {id},{model.salesInvDetailsId},{salesInvoiceId},{model.productId},{model.productWiseSpecificationId},{model.invoiceQty},{model.price},{model.vat},{model.ait},{model.discountAmount},{model.Total},{model.isActive},{model.isSelect},{model.barcodeId},{model.serialNo},{model.hasNationalBonus},{model.batchNo}"; 



                    //if (result1.Count > 1)
                    //{


                    //}
                    //else
                    //{
                    //    result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesInvoiceDetails {id},{model.salesInvDetailsId},{salesInvoiceId},{model.productId},{model.productWiseSpecificationId},{model.invoiceQty},{model.price},{model.vat},{model.ait},{model.discountAmount},{model.Total},{model.isActive},{model.isSelect},{model.barcodeId},{model.serialNo},{model.hasNationalBonus},{model.batchNo}").AsNoTracking().FirstOrDefaultAsync();
                    //}

                    int isFirstRow = 1;
                    int? prevProdSpec = 0;

                    #region commented on 05-Feb-2024

                    /*
                    if (companyId == 1)
                    {
                        result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesInvoiceDetails {id},{model.salesInvDetailsId},{salesInvoiceId},{model.productId},{model.productWiseSpecificationId},{model.invoiceQty},{model.price},{model.vat},{model.ait},{model.discountAmount},{model.Total},{model.isActive},{model.isSelect},{model.barcodeId},{model.serialNo},{model.hasNationalBonus},{model.batchNo},{0},{model.invoiceQty}, {isFirstRow}").AsNoTracking().FirstOrDefaultAsync();
                    }
                    else
                    {
                        int isdone = 0;
                        var result1 = await _context.salesBatchViewModels.FromSql($"SalSpGetProductBatch {storeId},{model.productWiseSpecificationId}").AsNoTracking().ToListAsync();
                        decimal qty = (decimal)model.invoiceQty;
                        decimal aqty = 0;
                        decimal? ttotal = 0;
                        foreach (SalesBatchsViewModel x in result1)
                        {
                            if (qty > 0)
                            {
                                if (qty < x.currentStock)
                                {
                                    aqty = qty;
                                }
                                else
                                {
                                    aqty = x.currentStock;
                                }

                                ttotal = (aqty * model.price) + (aqty * model.vat) + (aqty * model.ait) - (aqty * model.discountAmount);

                                qty = qty - aqty;

                                if (qty <= 0)
                                {
                                    isdone = 1;
                                }

                                if (prevProdSpec != model.productWiseSpecificationId)
                                {
                                    result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesInvoiceDetails {id},{model.salesInvDetailsId},{salesInvoiceId},{model.productId},{model.productWiseSpecificationId},{aqty},{model.price},{model.vat},{model.ait},{model.discountAmount},{ttotal},{model.isActive},{model.isSelect},{model.barcodeId},{model.serialNo},{model.hasNationalBonus},{x.batchNo},{isdone},{model.invoiceQty},{isFirstRow}").AsNoTracking().FirstOrDefaultAsync();

                                    if (result.isSuccess == 0)
                                    {
                                        prevProdSpec = model.productWiseSpecificationId;
                                        //return 0;
                                    }
                                }
                                isFirstRow++;
                                // if()
                            }
                        }
                    }
                    */

                    #endregion

                    int isdone = 0;
                    //var result1 = await _context.salesBatchViewModels.FromSql($"SalSpGetProductBatch {storeId},{model.productWiseSpecificationId}").AsNoTracking().ToListAsync();

                    // var BatchWiseStock = await _context.salesBatchViewModels.FromSql($"SalSpGetProductBatch {storeId},{model.productWiseSpecificationId},{model.salesInvoiceId},{model.invoiceQty},{model.hasNationalBonus}").AsNoTracking().ToListAsync();
                    var BatchWiseStock = await _context.salesBatchViewModels.FromSql($"SalSpGetProductBatch {storeId},{model.productWiseSpecificationId},{salesInvoiceId},{model.invoiceQty},{model.hasNationalBonus}").AsNoTracking().ToListAsync();

                    decimal qty = (decimal)model.invoiceQty;
                    decimal aqty = 0;
                    decimal? ttotal = 0;

                    int? isProcess = 0;

                    if (BatchWiseStock != null && BatchWiseStock.Count > 0)
                    {
                        isProcess = BatchWiseStock[0].isProcess;
                    }

                    if ((isProcess ?? 0) == 1)
                    {
                        foreach (SalesBatchsViewModel x in BatchWiseStock)
                        {
                            if (qty > 0)
                            {
                                if (qty < x.currentStock)
                                {
                                    aqty = qty;
                                }
                                else
                                {
                                    aqty = x.currentStock;
                                }

                                ttotal = (aqty * model.price) + (aqty * model.vat) + (aqty * model.ait) - (aqty * model.discountAmount);

                                qty = qty - aqty;

                                if (qty <= 0)
                                {
                                    isdone = 1;
                                }

                                if (prevProdSpec != model.productWiseSpecificationId)
                                {
                                    result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesInvoiceDetails {id},{model.salesInvDetailsId},{salesInvoiceId},{model.productId},{model.productWiseSpecificationId},{aqty},{model.price},{model.vat},{model.ait},{model.discountAmount},{ttotal},{model.isActive},{model.isSelect},{model.barcodeId},{model.serialNo},{model.hasNationalBonus},{x.batchNo},{isdone},{model.invoiceQty},{isFirstRow}").AsNoTracking().FirstOrDefaultAsync();

                                    if (result.isSuccess == 0)
                                    {
                                        prevProdSpec = model.productWiseSpecificationId;
                                        //return 0;
                                    }
                                }
                                isFirstRow++;
                                // if()
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetSalesDashboardChartData(int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesDashboardChartDataJSON {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetSalesDashboardDueChartData(int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesDashboardDueChartDataJSON  {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetSalesDashboardData(DateTime? fromDate, DateTime? toDate, string userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesDashboardDataJSON {fromDate}, {toDate}, {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSalesDashboardDataDetails(DateTime? fromDate, DateTime? toDate, int userId, int type, int partyId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesDashboardDataJSONDetails {fromDate}, {toDate}, {userId},{type},{partyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSalesDashboardDataDetailsPartyWise(DateTime? fromDate, DateTime? toDate, int userId, int type)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesDashboardDataJSONDetailsPartyWise {fromDate}, {toDate}, {userId},{type}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetBarcodeDetails(string barcodeNo)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"InvSpGetBarcodeDetailsForPOSJSON {barcodeNo}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<JsonViewModel> GetCustomerDuesStatus(int? userId, int partyId, string territoryCode)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetCustomerDuesStatus {userId}, {partyId}, {territoryCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetTargetVsAchievementReport(int? userId, string depotCode, string territoryCode, DateTime fDate, DateTime tDate)
        {
            return await _context.jsonViewModels.FromSql($"SalSpGetTargetVsAchievementForJSON {userId},{depotCode},{territoryCode},{fDate},{tDate}").AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<JsonViewModel> GetNationalOutStandingReport(int? userId, string reportName, string reportType, string zoneCode, string regionCode, string areaCode, string depotCode, string territoryCode, DateTime? fDate, DateTime? tDate, int? productWiseSpecificationId, string reportFormat, int isJsonOutput, int isDuesAmtOnly, string invoiceNo, string mioCode)
        {
            var sql = $"SalSpGetNationalOutStandingReportCR {userId},{reportName},{reportType},{zoneCode},{regionCode},{areaCode},{depotCode},{territoryCode},{fDate},{tDate},{productWiseSpecificationId},{isJsonOutput},{isDuesAmtOnly},{invoiceNo},{mioCode}";
            try
            {
                var data = await _context.jsonViewModels.FromSql($"SalSpGetNationalOutStandingReportCR {userId},{reportName},{reportType},{zoneCode},{regionCode},{areaCode},{depotCode},{territoryCode},{fDate},{tDate},{productWiseSpecificationId},{isJsonOutput},{isDuesAmtOnly},{invoiceNo},{mioCode}").AsNoTracking().FirstOrDefaultAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region Tender Quotation

        public async Task<int> SaveTenderQuotation(string id, TenderQuotationViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"TndrSpSetQuotation {id}, {model.quotationMasterId}, {model.quotationNo}, {model.quotationDate}, {model.paymentDate}, {model.storeId}, {model.partyId}, {model.mobileNo}, {model.alternateMobileNo}, {model.address}, {model.totalGross}, {model.totalVat}, {model.totalAit}, {model.shippingCost}, {model.totalDiscountAmount}, {model.grandTotal}, {model.approvalStatus}, {true}, {0}, {0}, {model.refNo}, {model.transactionTypeId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public async Task<int> SaveTenderQuotationDetails(string id, List<TenderQuotationDetailsViewModel> models, int quotationMasterId)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {
                result.isSuccess = 1;
                foreach (TenderQuotationDetailsViewModel model in models)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"TndrSpSetQuotationDetails {id},{model.quotationDetailsId},{quotationMasterId},{model.productId},{model.productWiseSpecificationId},{model.invoiceQty},{model.price},{model.vat},{model.ait},{model.discountAmount},{model.Total},{model.isActive},{model.isSelect},{model.barcodeId},{model.serialNo},{model.hasNationalBonus},{""},{1},{model.invoiceQty},{1},{model.specification},{model.remarks}").AsNoTracking().FirstOrDefaultAsync();            
                 }
                
            }
            catch (Exception ex)
            {
                return 0;
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetTenderQuotationId(int? quotationMasterId, int? userId, DateTime? fDate, DateTime? tDate)
        {
            var result = await _context.jsonViewModels.FromSql($"TndrSpGetQuotationJSON {quotationMasterId},{userId},{fDate},{tDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetTenderQuotationDetailsById(int? quotationMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"TndrSpGetTenderQuotationDetailsById {quotationMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteTenderQuotationById(string id, int quotationMasterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"TndrSpDeleteTenderQuotation {id}, {quotationMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        #endregion

        #region  Tender Quotation Approval
        public async Task<JsonViewModel> GetALLTenderQuotationApproval(int? id, int? isApproved)
        {
            var result = await _context.jsonViewModels.FromSql($"TndrSpGetQuotationApprovalJSON {id},{isApproved}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<int> SaveTenderQuotationApproval(int? userId, TenderQuotationApprovalViewModel model)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (TenderQuotationApprovalViewModel m in model.lstMasterViewModel)
                {
                    if (m.isSelect == true)
                    {
                        result = await _context.saveUpdateValueViewModels.FromSql($"TndrSpSetQuotationApproval {userId}, {m.quotationMasterId},{model.approvalStatusValue}").AsNoTracking().FirstOrDefaultAsync();
                    }
                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Tender Quotation Approval

        #region Tender Challan

        public async Task<int> SaveTenderChallan(string id, TenderChallanViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"TndrSpSetChallan {id}, {model.challanMasterId}, {model.challanNo}, {model.challanDate},{model.storeId}, {model.partyId}, {model.mobileNo}, {model.alternateMobileNo}, {model.address}, {model.totalGross}, {model.totalVat}, {model.totalAit}, {model.shippingCost}, {model.totalDiscountAmount}, {model.grandTotal}, {model.approvalStatus}, {true}, {model.planId}, {model.refNo}, {model.orderType}, {model.isFinal}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public async Task<int> SaveTenderChallanDetails(string id, List<TenderChallanDetailsViewModel> models, int challanMasterId)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {
                result.isSuccess = 1;
                foreach (TenderChallanDetailsViewModel model in models.Where(x => x.isSelect == true))
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"TndrSpSetChallanDetails {id},{model.challanDetailsId},{challanMasterId},{model.quotationDetailsId},{model.quotationMasterId},{model.productId},{model.productWiseSpecificationId},{model.challanQty},{model.price},{model.vat},{model.ait},{model.discountAmount},{model.Total},{model.isActive},{model.isSelect},{model.barcodeId},{model.serialNo},{model.batchNo},{model.specification},{model.remarks}").AsNoTracking().FirstOrDefaultAsync();
                }

            }
            catch (Exception ex)
            {
                return 0;
            }
            return result.isSuccess;
        }

        public async Task<int> SaveTenderBill(string id, TenderBillViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"TndrSpSetBill {id}, {model.billMasterId}, {model.billNo}, {model.billDate}, {model.storeId}, {model.partyId}, {model.mobileNo}, {model.alternateMobileNo}, {model.address}, {model.totalGross}, {model.totalVat}, {model.totalAit}, {model.shippingCost}, {model.totalDiscountAmount}, {model.grandTotal}, {model.billStatus}, {true}, {model.planId}, {model.refNo}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> SaveTenderBillDetails(string id, List<TenderBillDetailsViewModel> models, int billMasterId)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {
                result.isSuccess = 1;
                foreach (TenderBillDetailsViewModel model in models.Where(x => x.isSelect == true))
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"TndrSpSetBillDetails {id},{model.billDetailsId},{billMasterId},{model.challanDetailsId},{model.productId},{model.productWiseSpecificationId},{model.billQty},{model.price},{model.vat},{model.ait},{model.discountAmount},{model.Total},{model.isActive},{model.isSelect},{model.barcodeId},{model.serialNo},{model.batchNo},{model.specification},{model.remarks}").AsNoTracking().FirstOrDefaultAsync();
                }

            }
            catch (Exception ex)
            {
                return 0;
            }
            return result.isSuccess;
        }

        public async Task<int> SaveTenderFinalChallanDetails(string id, List<TenderFinalChallanDetailsViewModel> models, int challanMasterId)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {
                result.isSuccess = 1;
                foreach (TenderFinalChallanDetailsViewModel model in models.Where(x => x.isSelect == true))
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"TndrSpSetFinalChallanDetails {id},{model.finalChallanDetailsId},{model.challanDetailsId},{challanMasterId},{model.quotationDetailsId},{model.quotationMasterId},{model.productId},{model.productWiseSpecificationId},{model.challanQty},{model.price},{model.vat},{model.ait},{model.discountAmount},{model.Total},{model.isActive},{model.isSelect},{model.barcodeId},{model.serialNumber},{model.batchNo},{model.specification},{model.remarks},{model.deliveryStatus}").AsNoTracking().FirstOrDefaultAsync();
                }

            }
            catch (Exception ex)
            {
                return 0;
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetTenderChallanById(int? challanMasterId, int? userId, DateTime? fDate, DateTime? tDate)
        {
            var result = await _context.jsonViewModels.FromSql($"TndrSpGetChallanJSON {challanMasterId},{userId},{fDate},{tDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetTenderChallanWihoutQuotationById(int? challanMasterId, int? userId, DateTime? fDate, DateTime? tDate)
        {
            var result = await _context.jsonViewModels.FromSql($"TndrSpGetChallanWihoutQuotationJSON {challanMasterId},{userId},{fDate},{tDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetTenderBillById(int? billMasterId, int? userId, DateTime? fDate, DateTime? tDate)
        {
            var result = await _context.jsonViewModels.FromSql($"TndrSpGetBillJSON {billMasterId},{userId},{fDate},{tDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetQuotationForChallan(int? userId, int? partyId)
        {
            var result = await _context.jsonViewModels.FromSql($"TndrSpGetQuotationForChallanJSON {userId},{partyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetTenderQuotationDetailsForChallanById(int? quotationMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"TndrSpGetTenderQuotationDetailsForChallanById {quotationMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetChallanForBill(int? userId, int? partyId)
        {
            var result = await _context.jsonViewModels.FromSql($"TndrSpGetChallanForBillJSON {userId},{partyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetChallanDetailsForBillById(int? challanMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"TndrSpGetChallanDetailsForBillJSON {challanMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetTenderChallanDetailsForFinalChallanByQuotationMasterId(int? quotationMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"TndrSpGetTenderChallanDetailsForFinalChallanByQuotationMasterId {quotationMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        #endregion

        #region T&C
        public async Task<int> SaveSalesInvoiceTC(string id, List<SalesInvoiceTandCViewModel> models, int salesInvoiceId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (SalesInvoiceTandCViewModel model in models)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesInvoiceTC {id},{model.salesInvoiceTCId},{salesInvoiceId},{model.termsAndCondition},{model.isActive},{model.isSelect}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }


        public async Task<JsonViewModel> GetSalesInvoiceTCByMasterId(int? salesInvoiceId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesInvoiceTCJSON {salesInvoiceId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteSalesInvoiceTCById(string id, int? salesInvoiceTCId, bool? isSelect)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteSalesInvoiceTC {id}, {salesInvoiceTCId},{isSelect}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Reports

        public async Task<JsonViewModel> GetDateRangeWiseUserName(DateTime? fromDate, DateTime? toDate, int employeeId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"SalSpGetDateRangeWiseUserNameJSON {fromDate}, {toDate},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetSalesInvoiceReportData(int? salesInvoiceId, int? partyId, DateTime? fromDate, DateTime? toDate, string userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesInvoiceReportDataJSON {salesInvoiceId}, {partyId}, {fromDate}, {toDate}, {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAddressForReportFooter(int? companyId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetAddressForReportFooterJSON {companyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSalesInvoiceReportDataById(int? salesInvoiceId)//, int? partyId, DateTime? fromDate, DateTime? toDate, string userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesInvoiceReportDataByIdJSON {salesInvoiceId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSalesReportByInvId(int? salesInvoiceId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpRptSalesByInvId {salesInvoiceId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSalesInvoiceListByPartyId(int? partyId, DateTime? fDate, DateTime? tDate)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesInvoiceListJSON {partyId},{fDate},{tDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSalesInvoiceSearchResult(string SearchingText, DateTime? FromDate, DateTime? ToDate)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesInvoiceSearchResult {SearchingText}, {FromDate}, {ToDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSaleRegisterReport(int? userId, string depoCode, string territoryCode, int? partyId, DateTime? fDate, DateTime? tDate, string zoneCode, string regionCode, string areaCode)
        {
            //zoneCode = string.IsNullOrEmpty(zoneCode) ? "NULL" : ("'" + zoneCode + "'");
            //regionCode = string.IsNullOrEmpty(regionCode) ? "NULL" : ("'" + regionCode + "'");
            //areaCode = string.IsNullOrEmpty(areaCode) ? "NULL" : ("'" + areaCode + "'");
            return await _context.jsonViewModels.FromSql($"salSpGetSalesRegister {userId},{depoCode},{territoryCode},{partyId},{fDate},{tDate},{zoneCode}, {regionCode}, {areaCode}").AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<JsonViewModel> GetSaleRegisterReportForBill(int? userId, int? partyId, DateTime? fDate, DateTime? tDate)
        {
            return await _context.jsonViewModels.FromSql($"salSpGetSalesRegisterForBill {userId},{partyId},{fDate},{tDate}").AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<JsonViewModel> GetZoneRegionWiseSalesCollectionBalanceReport(int? userId, string zoneCode, string regionCode, string areaCode, string territoryCode, DateTime fDate, DateTime tDate, string type, string mioType)
        {
            return await _context.jsonViewModels.FromSql($"SalSpGetZoneRegionWiseSalesCollectionBalanceJSON_N {userId},{zoneCode},{regionCode},{areaCode},{territoryCode},{fDate},{tDate},{type},{mioType}").AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<JsonViewModel> GetProductWiseNationalSalesReport(int? userId, DateTime? fDate, DateTime? tDate, string depoCode, string territoryCode, int? partyId)
        {
            return await _context.jsonViewModels.FromSql($"SalSpGetProductWiseNationalSales {userId},{fDate},{tDate},{depoCode},{territoryCode},{partyId}").AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<JsonViewModel> GetNationalProductSalesReport(int? userId, DateTime? fDate, DateTime? tDate, string depoCode, string territoryCode, int? partyId)
        {
            return await _context.jsonViewModels.FromSql($"SalSpGetNationalProductSales {userId},{fDate},{tDate},{depoCode},{territoryCode},{partyId}").AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<JsonViewModel> GetWeeklyProductMonitorReport(int? userId, DateTime? fDate, DateTime? tDate, string zoneCode, string regionCode, string areaCode, string depotCode, string territoryCode, string empCode)
        {
            return await _context.jsonViewModels.FromSql($"salSpGetWeeklyProductMonitorReportJSON {userId},{fDate}, {tDate}, {zoneCode}, {regionCode}, {areaCode}, {depotCode}, {territoryCode}, {empCode}").AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<JsonViewModel> GetMioProductSalesReport(int? userId, string depotCode, string territoryCode, DateTime fDate, DateTime tDate, string zoneCode, string regionCode, string areaCode, int? partyId, int? productWiseSpecificationId)
        {
            try
            {
                return await _context.jsonViewModels.FromSql($"SalSpGetMioProductSalesJSON {userId},{depotCode},{territoryCode},{fDate},{tDate},{zoneCode}, {regionCode}, {areaCode},{partyId},{productWiseSpecificationId}").AsNoTracking().FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<JsonViewModel> GetZone(int? userId)
        {
            return await _context.jsonViewModels.FromSql($"SalSpGetZoneJSON {userId}").AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<JsonViewModel> GetRegion(int? userId, string zoneCode)
        {
            return await _context.jsonViewModels.FromSql($"SalSpGetRegionJSON {userId}, {zoneCode}").AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<JsonViewModel> GetArea(int? userId, string regionCode)
        {
            return await _context.jsonViewModels.FromSql($"SalSpGetAreaJSON {userId}, {regionCode}").AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<JsonViewModel> GetAreaForNationalSalesReport(int? userId, string regionCode)
        {
            return await _context.jsonViewModels.FromSql($"SalSpGetAreaJSONForNationalSalesReport {userId}, {regionCode}").AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<JsonViewModel> GetTerritory(int? userId, string areaCode)
        {
            return await _context.jsonViewModels.FromSql($"SalSpGetTerritoryJSON {userId}, {areaCode}").AsNoTracking().FirstOrDefaultAsync();
        }

        #endregion

        #region Approval

        public async Task<int> ApproveSalesInvoiceMaster(string userId, string approvalStatus, List<SalesInvoiceViewModel> models)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (var model in models)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesInvoiceMasterAproval {userId}, {model.salesInvoiceId},{approvalStatus},{model.isSelect}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }
        public async Task<int> UpdateSalesInvoiceDetails(string userId, List<SalesInvoiceDetailsViewModel> models)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (var model in models)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"SalSpUpdateSalesInvoiceForApproval {userId}, {model.salesInvoiceId},{model.salesInvDetailsId},{model.invoiceQty},{model.Total}").AsNoTracking().FirstOrDefaultAsync();
            }

            if (result.isSuccess == 1)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesInvoiceMasterAproval {userId}, {models[0].salesInvoiceId},{1},{1}").AsNoTracking().FirstOrDefaultAsync();
            }

            else result.isSuccess = 0;

            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetSalesInvoiceMasterListForApproval(string userId, int salesInvoiceId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesInvoiceMasterListForApprovalJson {userId}, {salesInvoiceId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetSalesInvoiceMasterListByStatus(string userId, int status, string territoryCode, int? transactionTypeId, string areaCode)
        {
            //var txt = $"SalSpGetSalesInvoiceMasterListByStatusJson {userId}, {status}, {territoryCode}, {transactionTypeId}, {areaCode}";

            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesInvoiceMasterListByStatusJson {userId}, {status}, {territoryCode}, {transactionTypeId}, {areaCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetSalesInvoiceMasterListByStatusandTerritory(string userId, int status, string territoryCode)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesInvoiceMasterListByTerritoryandStatusJson {userId}, {status},{territoryCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetSalesPickingMasterListJson(string userId, int pikingMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesPickingMasterListJson {userId}, {pikingMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetMiscellaneousItemDepotListJson(string userId, int typeId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetMiscellaneousItemDepotListJson {userId}, {typeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetMiscellaneousItemMarketListJson(string userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetMiscellaneousItemMarketListJson {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSalSpGetAllSalesDispatchByIdJson(string userId, int masterId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetAllSalesDispatchByIdJson {masterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAllDamageExpireReturnByIdJson(string userId, int masterId, string MarketOrDepo)
        {
            //this service used for three different pages report.(Damage expire report,Market expire report,Approval report)
            var result = await _context.jsonViewModels.FromSql($"SalSpGetAllDamageExpireReturnByIdJson {masterId},{MarketOrDepo}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> SalSpGetAllPickingDetailsByMasterIdJson(string userId, int pikingMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetAllPickingDetailsByMasterIdJson {pikingMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetSalesPickingSummaryByMasterIdJson(string userId, int pikingMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetAllPickingSummaryByMasterIdJson {pikingMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetSalesInvoiceListfromDispatchJson(string userId, int dispatchMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesInvoiceListfromDispatchJson {userId}, {dispatchMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSalesInvoiceListfromDispatchJson_v2(string userId, int dispatchMasterId, int? partyId, DateTime? collectionDate, string territoryCode, int? transactionTypeId, string mioCode)
        {
            //var rs = $"SalSpGetSalesInvoiceListfromDispatchJson_v2 {userId}, {dispatchMasterId}, {partyId}, {collectionDate}, {territoryCode}, {transactionTypeId}, {mioCode}";

            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesInvoiceListfromDispatchJson_v2 {userId}, {dispatchMasterId}, {partyId}, {collectionDate}, {territoryCode}, {transactionTypeId}, {mioCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSalesInvoiceListForBillCollection(string userId, int? collectionMasterId, int? partyId, DateTime? collectionDate)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesInvoiceListForBillCollectionJson {userId}, {collectionMasterId}, {partyId}, {collectionDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSalSpGetAllPickingJson(int? userId, DateTime? fDate, DateTime? tDate)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetAllPickingJson {userId},{fDate},{tDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetSalSpGetAllSalesDispatchJson(int employeeId, DateTime? fromDate, DateTime? toDate)
        {
            //var result = await _context.jsonViewModels.FromSql($"GetSalSpGetAllSalesDispatchJson {employeeId}").AsNoTracking().FirstOrDefaultAsync();
            var result = await _context.jsonViewModels.FromSql($"SalSpGetAllSalesDispatchJson {employeeId},{fromDate},{toDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetAllDamageExpireProductReturn(string MarketOrDepo, int? employeeId, int? isApproved)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetAllDamageExpireProductReturn {MarketOrDepo},{employeeId},{isApproved}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetAllDestructionNoteReceive(int? employeeId, int? masterId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetAllDestructionNoteReceive {employeeId},{masterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetSalesInvoiceDetailsByIdForApproval(int salesInvoiceId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesInvoiceDetailsForApprovalJSON {salesInvoiceId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetAllDepot(int? userId)
        {
            return await _context.jsonViewModels.FromSql($"SalSpGetAllDepot {userId}").AsNoTracking().FirstOrDefaultAsync();
        }

        #endregion

        #region Create Sales Auto Voucher       

        public async Task<int> CreateAutoJournalForSalesInvoice(string id, SalesInvoiceViewModel model)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpCreateSalesInvoiceJournal {id},{model.grandTotal},{model.salesInvoiceDate},{model.salesInvoiceNo},{model.partyId}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }

        public async Task<int> CreateAutoJournalForSalesInvoiceOnCredit(string id, SalesInvoiceViewModel model)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpCreateSalesInvoiceJournalOnCredit {id},{model.grandTotal},{model.salesInvoiceDate},{model.salesInvoiceNo},{model.partyId}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }

        public async Task<int> CreateAutoJournalForSalesInvoiceOnAdvance(string id, SalesInvoiceViewModel model)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpCreateSalesInvoiceJournalOnAdvance {id},{model.grandTotal},{model.salesInvoiceDate},{model.salesInvoiceNo},{model.partyId}").AsNoTracking().FirstOrDefaultAsync();

            return result.isSuccess;
        }

        #endregion

        #region For Android App

        public async Task<JsonViewModel> GetSalesOrderDtlByInvIdForApp(int? salesInvoiceId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesOrderDtlByInvId {salesInvoiceId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetSalesOrderByChemist(int? chemistId, int? statusId, string fromDate, string toDate, int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesOrderByChemist {chemistId},{statusId},{fromDate},{toDate},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> UpdateSalesOrderStatusForApp(string id, int salesInvoiceId, int statusId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AppSalSpChangeSalesOrderStatus {id}, {salesInvoiceId}, {statusId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetSalesOrderByAdminForApprove(int? employeeId, int? statusId, string fromDate, string toDate)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesOrderByAdminForApprove {employeeId},{statusId},{fromDate},{toDate}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> ApproveSalesOrderStatusByAdmin(string userId, List<SalesInvoiceApproveViewModel> models)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (var model in models)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"SalSpUpdateSalesOrderStatusByAdmin {userId}, {model.salesInvoiceId},{model.status}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        #endregion

        #region Sales Party
        public async Task<int> SaveParty(string userId, SalesInvPartyViewModel model)
        {
            try/* for flyingCustomer */
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesParty {userId}, {model.partyId}, {model.companyId}, {model.sbuId}, {model.partyTypeId}, {model.partyName}, {model.partyMobile}, {model.partyAddress},{model.territoryCode}").AsNoTracking().FirstOrDefaultAsync();

                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<JsonViewModel> GetDuplicatePartyInfo(string partyName, string mobileNo)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetDuplicatePartyInfoJson {partyName}, {mobileNo}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }


        #endregion

        #region Sales Report Nationally
        public async Task<JsonViewModel> GetSalesReportNationally(int? userId, string reportName, string reportType, string zoneCode, string regionCode, string areaCode, string territoryCode, DateTime? fDate, DateTime? tDate, int? productWiseSpecificationId, int reportPeriod)
        {
            try
            {
                var fromDate = fDate != null ? ("'" + fDate + "'") : "NULL";
                var toDate = tDate != null ? ("'" + tDate + "'") : "NULL";
                string sql = $"SalSpGetMonthlyTotalSales {userId},{reportName},{reportType},{zoneCode},{regionCode},{areaCode},{territoryCode},{fDate},{tDate},{productWiseSpecificationId},{reportPeriod}";

                string sql2 = $"SalSpGetNationalSalesPerformance {userId},{reportName},{reportType},{zoneCode},{regionCode},{areaCode},{territoryCode},{fDate},{tDate},{productWiseSpecificationId},{reportPeriod},{1}";

                if (reportName == "MIONationalPerformanceByProduct" || reportName == "AMNationalPerformanceByProduct" || reportName == "RSMNationalPerformanceByProduct" || reportName == "SMNationalPerformanceByProduct" || reportName == "nationalPerformanceByProduct" || reportName == "nationalPerformanceByFFTotal")
                {
                    //var result = await _context.jsonViewModels.FromSql($"SalSpGetNationalSalesPerformance {userId},{reportName},{reportType},{zoneCode},{regionCode},{areaCode},{territoryCode},{fDate},{tDate},{productWiseSpecificationId},{1}").AsNoTracking().FirstOrDefaultAsync();
                    //return result;


                    var result = await _context.jsonViewModels.FromSql($"SalSpGetNationalSalesPerformance {userId},{reportName},{reportType},{zoneCode},{regionCode},{areaCode},{territoryCode},{fDate},{tDate},{productWiseSpecificationId}").AsNoTracking().FirstOrDefaultAsync();
                    return result;
                }
                else
                {
                    var result2 = await _context.jsonViewModels.FromSql($"SalSpGetMonthlyTotalSales {userId},{reportName},{reportType},{zoneCode},{regionCode},{areaCode},{territoryCode},{fDate},{tDate},{productWiseSpecificationId},{reportPeriod}").AsNoTracking().FirstOrDefaultAsync();

                    return result2;
                }


                //return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<string> GetSalesReportNationallyExcelOnly(int? userId, string reportName, string reportType, string zoneCode, string regionCode, string areaCode, string territoryCode, DateTime? fDate, DateTime? tDate, int? productWiseSpecificationId, int reportPeriod, string reportTypeName, string zoneName, string regionName, string territoryName, string areaName, string productName)
        {


            try
            {
                var fromDate = fDate != null ? ("'" + fDate + "'") : "NULL";
                var toDate = tDate != null ? ("'" + tDate + "'") : "NULL";
                string sql = $"SalSpGetMonthlyTotalSales {userId},{reportName},{reportType},{zoneCode},{regionCode},{areaCode},{territoryCode},{fDate},{tDate},{productWiseSpecificationId},{reportPeriod}";

                string sql2 = $"SalSpGetNationalSalesPerformance {userId},{reportName},{reportType},{zoneCode},{regionCode},{areaCode},{territoryCode},{fDate},{tDate},{productWiseSpecificationId},{reportPeriod},{1}";

                if (reportName == "MIONationalPerformanceByProduct" || reportName == "AMNationalPerformanceByProduct" || reportName == "RSMNationalPerformanceByProduct")
                {
                    var result = await _context.jsonViewModels.FromSql($"SalSpGetNationalSalesPerformance {userId},{reportName},{reportType},{zoneCode},{regionCode},{areaCode},{territoryCode},{fDate},{tDate},{productWiseSpecificationId},{1}").AsNoTracking().FirstOrDefaultAsync();
                    var data = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(result.data);

                    var selectedColumns = data.First().Keys.ToList();
                    return GenerateDynamicExcel(selectedColumns, data, reportName, reportTypeName, fDate?.ToString("MMM-yyyy"), tDate?.ToString("MMM-yyyy"), zoneName, regionName, territoryName, areaName, productName);



                }
                else
                {
                    var query = $"SalSpGetMonthlyTotalSales {userId},{reportName},{reportType},{zoneCode},{regionCode},{areaCode},{territoryCode},{fDate},{tDate},{productWiseSpecificationId},{reportPeriod}";

                    var result2 = await _context.jsonViewModels.FromSql($"SalSpGetMonthlyTotalSales {userId},{reportName},{reportType},{zoneCode},{regionCode},{areaCode},{territoryCode},{fDate},{tDate},{productWiseSpecificationId},{reportPeriod}").AsNoTracking().FirstOrDefaultAsync();

                    var data = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(result2.data);
                    var selectedColumns = data.First().Keys.ToList();
                    return GenerateDynamicExcel(selectedColumns, data, reportName, reportTypeName, fDate?.ToString("MMM-yyyy"), tDate?.ToString("MMM-yyyy"), zoneName, regionName, territoryName, areaName, productName);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> GetRptAccountScheduleReportByAccountGroupIdsExcelOnly(int companyId, int sbuId, string accountGroupIds, DateTime? fDate, DateTime? tDate, string reportType, int? natureId, int? isOb, string reportFormat)
        {


            try
            {
                var fromDate =Convert.ToDateTime( fDate).ToString("yyyy-MM-dd"); //!= null ? ("'" + fDate + "'") : "NULL";
                var toDate = Convert.ToDateTime(tDate).ToString("yyyy-MM-dd"); //!= null ? ("'" + tDate + "'") : "NULL";

                string sql = $"AccSpRptScheduleReportByAccountGroupIdsForCRJson {companyId},{sbuId},{0},'{Convert.ToDateTime(fDate).ToString("yyyy-MM-dd") }','{Convert.ToDateTime(tDate).ToString("yyyy-MM-dd")}','{reportType}','{accountGroupIds}',{isOb}";
                //var result2 = await _context.jsonViewModels.FromSql($"AccSpRptScheduleReportByAccountGroupIdsForCRJson {companyId},{sbuId},{0},'{fromDate}','{toDate}','{reportType}','{accountGroupIds}',{isOb}").AsNoTracking().FirstOrDefaultAsync();
                var result2 = await _context.jsonViewModels.FromSql($"AccSpRptScheduleReportByAccountGroupIdsForCRJson {companyId},{sbuId},{0},{fromDate},{toDate},{reportType},{accountGroupIds},{isOb}").AsNoTracking().FirstOrDefaultAsync();

                var data = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(result2.data);
                var selectedColumns = data.First().Keys.ToList();
                return GenerateDynamicSheduleExcel(selectedColumns, data, "Shedule Report", "", fDate?.ToString("MMM-yyyy"), tDate?.ToString("MMM-yyyy"), "", "", "", "", "");


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<JsonViewModel> GetNationalSalesPerformance(int? userId, string reportName, string reportType, string zoneCode, string regionCode, string areaCode, string territoryCode, DateTime? fDate, DateTime? tDate, int? productWiseSpecificationId)
        {
            try
            {
                var fromDate = fDate != null ? ("'" + fDate?.ToString("yyyy-MM-dd") + "'") : "NULL";
                var toDate = tDate != null ? ("'" + tDate?.ToString("yyyy-MM-dd") + "'") : "NULL";

                string sql = $"exec SalSpGetNationalSalesPerformance {userId},{reportName},{reportType},{zoneCode},{regionCode},{areaCode},{territoryCode},{fromDate},{toDate},{productWiseSpecificationId}";

                var result = await _context.jsonViewModels.FromSql($"SalSpGetNationalSalesPerformance {userId},{reportName},{reportType},{zoneCode},{regionCode},{areaCode},{territoryCode},{fDate},{tDate},{productWiseSpecificationId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<JsonViewModel> GetNationSalesClosingStatement(int? userId, DateTime? fDate, DateTime? tDate, int? productWiseSpecificationId)
        {
            try
            {
                var fromDate = fDate != null ? ("'" + fDate + "'") : "NULL";
                var toDate = tDate != null ? ("'" + tDate + "'") : "NULL";
                var result = await _context.jsonViewModels.FromSql($"SpNationalSalesClosingStatementJSON {userId}, {fDate},{tDate},{productWiseSpecificationId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetNationSalesClosingStatementLM(int? userId, DateTime? fDate, DateTime? tDate, int? productWiseSpecificationId)
        {
            try
            {
                var fromDate = fDate != null ? ("'" + fDate + "'") : "NULL";
                var toDate = tDate != null ? ("'" + tDate + "'") : "NULL";
                var result = await _context.jsonViewModels.FromSql($"SpNationalSalesClosingStatementLMJSON {userId}, {fDate},{tDate},{productWiseSpecificationId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<JsonViewModel> GetNationalStockByQtyReport(int? userId, DateTime? fDate, int? productWiseSpecificationId, string productTypeName)
        {
            try
            {
                var fromDate = fDate != null ? ("'" + fDate + "'") : "NULL";
                var result = await _context.jsonViewModels.FromSql($"InvSpGetNationalStockByQtyReportJSON {userId}, {fDate}, {productWiseSpecificationId}, {productTypeName}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region SalesReportName
        public async Task<JsonViewModel> GetReportsName(int reportMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalDMSGetReportMaster").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetReportDetails(int dmsReportMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"GetDMSReportDetailsByMasterId {dmsReportMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region Cash In Hand Report
        public async Task<JsonViewModel> GetCashInHand(string DepotCode, int? userId, DateTime fDate)
        {
            try
            {
                var fromDate = fDate != null ? ("'" + fDate + "'") : "NULL";

                var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesRemittanceCIHJSON {DepotCode},{userId},{fDate}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Check Depot and Territory code
        public async Task<JsonViewModel> CheckDepotandTerritory(int? userId, string DepotCode, string territoryCode, string productCode)
        {
            try
            {

                var result = await _context.jsonViewModels.FromSql($"SalSpGetDepotAndTerritory {userId},{DepotCode},{territoryCode},{productCode}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion






        #region Sales Order for Mobile App

        public async Task<JsonViewModel> GetSalesOrderById(int? salesOrderId, int? userId, DateTime? fDate, DateTime? tDate, int? approvalStatus, int? partyId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesOrderJSON {salesOrderId},{userId},{fDate},{tDate},{approvalStatus},{partyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteSalesOrderById(string id, int salesOrderId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteSalesOrder {id}, {salesOrderId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<bool> DeleteSalesOrderDetailsByOrderDetailsId(int? userId, int salesOrderDetailsId)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteSalesOrderDetailsByOrderDetailsId {userId}, {salesOrderDetailsId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<JsonViewModel> GetSalesOrderMasterApprovedList(string userId, int masterId, string territoryCode)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesOrderMasterApprovedListJson {userId}, {masterId}, {territoryCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetSalesOrderDetailsByIdForApproval(int salesInvoiceId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesOrderDetailsForApprovalJSON {salesInvoiceId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<int> SaveSalesOrder(string id, SalesOrderViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesOrder {id}, {model.salesOrderId}, {model.salesOrderNo}, {model.salesOrderDate}, {model.paymentDate}, {model.storeId}, {model.partyId}, {model.mobileNo}, {model.alternateMobileNo}, {model.address}, {model.totalGross}, {model.totalVat}, {model.totalAit}, {model.shippingCost}, {model.totalDiscountAmount}, {model.grandTotal}, {model.approvalStatus}, {model.isActive}, {0}, {0}, {model.refNo}, {model.transactionTypeId}").AsNoTracking().FirstOrDefaultAsync();

                //foreach (OrderCreditNoteViewModel m in model.lstCreditNoteViewModel)
                //{
                //    var SalesOrderId = result.isSuccess;
                //    if ((bool)m.isSelect && SalesOrderId > 0)
                //    {
                //        var crn = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetProductExpireReturnInvoiceUpdate {id},{m.productExpireReturnId},{SalesInvoiceId}").AsNoTracking().FirstOrDefaultAsync();
                //    }
                //}

                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        public async Task<int> SaveSalesOrderDetails(string id, List<SalesOrderDetailsViewModel> models, int salesOrderId, int storeId, int companyId)
        {
            var result = new SaveUpdateValueViewModel();
            try
            {
                result.isSuccess = 1;
                foreach (SalesOrderDetailsViewModel model in models)
                {
                    int isFirstRow = 1;
                    int? prevProdSpec = 0;

                    int isdone = 0;

                    //var BatchWiseStock = await _context.salesBatchViewModels.FromSql($"SalSpGetProductBatch_ForOrder {storeId},{model.productWiseSpecificationId},{model.salesOrderId},{model.orderQty},{model.hasNationalBonus}").AsNoTracking().ToListAsync();
                    var BatchWiseStock = await _context.salesBatchViewModels.FromSql($"SalSpGetProductBatch_ForOrder {storeId},{model.productWiseSpecificationId},{salesOrderId},{model.orderQty},{model.hasNationalBonus}").AsNoTracking().ToListAsync();

                    decimal qty = (decimal)model.orderQty;
                    decimal aqty = 0;
                    decimal? ttotal = 0;

                    int? isProcess = 0;

                    if (BatchWiseStock != null && BatchWiseStock.Count > 0)
                    {
                        isProcess = BatchWiseStock[0].isProcess;
                    }

                    if ((isProcess ?? 0) == 1)
                    {
                        foreach (SalesBatchsViewModel x in BatchWiseStock)
                        {
                            if (qty > 0)
                            {
                                if (qty < x.currentStock)
                                {
                                    aqty = qty;
                                }
                                else
                                {
                                    aqty = x.currentStock;
                                }

                                ttotal = (aqty * model.price) + (aqty * model.vat) + (aqty * model.ait) - (aqty * model.discountAmount);

                                qty = qty - aqty;

                                if (qty <= 0)
                                {
                                    isdone = 1;
                                }

                                if (prevProdSpec != model.productWiseSpecificationId)
                                {
                                    result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesOrderDetails {id},{model.salesOrderDetailsId},{salesOrderId},{model.productId},{model.productWiseSpecificationId},{aqty},{model.price},{model.vat},{model.ait},{model.discountAmount},{ttotal},{model.isActive},{model.isSelect},{model.barcodeId},{model.serialNo},{model.hasNationalBonus},{x.batchNo},{isdone},{model.orderQty},{isFirstRow}").AsNoTracking().FirstOrDefaultAsync();

                                    if (result.isSuccess == 0)
                                    {
                                        prevProdSpec = model.productWiseSpecificationId;
                                        //return 0;
                                    }
                                }
                                isFirstRow++;
                                // if()
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return result.isSuccess;
        }
        public async Task<int> SaveSalesOrderTC(string id, List<SalesOrderTandCViewModel> models, int salesOrderId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (SalesOrderTandCViewModel model in models)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalesOrderTC {id},{model.salesOrderTCId},{salesOrderId},{model.termsAndCondition},{model.isActive},{model.isSelect}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<string> GetValidateProductAvailableStockForOrder(string userId, int? storeId, int? productWiseSpecificationId, string batchNo, decimal? orderQty, int? partyId, DateTime? salesOrderDate, bool? hasNationalBonus)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"salSpGetValidateProductAvailableStockForOrder {userId},{storeId},{productWiseSpecificationId},{batchNo},{orderQty},{partyId},{salesOrderDate},{hasNationalBonus}").AsNoTracking().FirstOrDefaultAsync();
                return result.data;
            }
            catch (Exception ex)
            {
                return "Current stock validation process failed!";
            }
        }

        public async Task<JsonViewModel> GetAvailableStockForOrder(int? userId, int storeId, int productWiseSpecificationId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"salSpGetAvailableStockForOrder {userId},{storeId},{productWiseSpecificationId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                JsonViewModel m = new JsonViewModel();
                m.data = "[]";
                return m;
            }
        }


        #endregion

        // with SL column
        //public string GenerateDynamicExcel(List<string> selectedColumns, List<Dictionary<string, object>> data, string reportName, string reportTypeName, string fromDate, string toDate, string zone, string region, string territory, string area, string productName)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(zone))
        //            zone = "All";
        //        if (string.IsNullOrEmpty(region))
        //            region = "All";
        //        if (string.IsNullOrEmpty(area))
        //            area = "All";
        //        if (string.IsNullOrEmpty(territory))
        //            territory = "All";
        //        if (string.IsNullOrEmpty(productName))
        //            productName = "N/A";


        //        //int mergeUptoColumn = 5;
        //        DateTime reportDate = DateTime.Now;
        //        IWorkbook workbook = new XSSFWorkbook();
        //        ISheet sheet = workbook.CreateSheet("National Sales Report");


        //        IRow titleRow = sheet.CreateRow(0);
        //        ICell titleCell = titleRow.CreateCell(0);
        //        titleCell.SetCellValue(reportTypeName);
        //        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, selectedColumns.Count - 1)); // Merge across all columns


        //        IRow zoneRow = sheet.CreateRow(1);
        //        ICell zoneCell = zoneRow.CreateCell(0);
        //        zoneCell.SetCellValue($"Zone Name: {zone}");
        //        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 0, selectedColumns.Count - 1));


        //        IRow regionRow = sheet.CreateRow(2);
        //        ICell regionCell = regionRow.CreateCell(0);
        //        regionCell.SetCellValue($"Region Name: {region}");
        //        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 0, selectedColumns.Count - 1));

        //        IRow areaRow = sheet.CreateRow(3);
        //        ICell areaCell = areaRow.CreateCell(0);
        //        areaCell.SetCellValue($"Area Name: {area}");
        //        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 0, selectedColumns.Count - 1));


        //        IRow territoryRow = sheet.CreateRow(4);
        //        ICell territoryCell = territoryRow.CreateCell(0);
        //        territoryCell.SetCellValue($"Territory Name: {territory}");
        //        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 0, selectedColumns.Count - 1));

        //        IRow productRow = sheet.CreateRow(5);
        //        ICell productCell = productRow.CreateCell(0);
        //        productCell.SetCellValue($"Product Name: {productName}");
        //        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(5, 5, 0, selectedColumns.Count - 1));


        //        IRow dateRangeRow = sheet.CreateRow(6);
        //        ICell dateRangeCell = dateRangeRow.CreateCell(0);
        //        dateRangeCell.SetCellValue($"Period: {fromDate} To: {toDate}");
        //        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(6, 6, 0, selectedColumns.Count - 1));


        //        IRow headerRow = sheet.CreateRow(7);
        //        for (int i = 0; i < selectedColumns.Count; i++)
        //        {
        //            //if(selectedColumns[i] == "SL")
        //            headerRow.CreateCell(i).SetCellValue(selectedColumns[i]);
        //        }

        //        // Create cell styles for different colors
        //        ICellStyle whiteStyle = workbook.CreateCellStyle();
        //        whiteStyle.FillForegroundColor = IndexedColors.White.Index;
        //        whiteStyle.FillPattern = FillPattern.SolidForeground;

        //        ICellStyle skyBlue = workbook.CreateCellStyle();
        //        skyBlue.FillForegroundColor = IndexedColors.SkyBlue.Index;
        //        skyBlue.FillPattern = FillPattern.SolidForeground;

        //        ICellStyle yellowStyle = workbook.CreateCellStyle();
        //        yellowStyle.FillForegroundColor = IndexedColors.LightYellow.Index;
        //        yellowStyle.FillPattern = FillPattern.SolidForeground;

        //        ICellStyle greenStyle = workbook.CreateCellStyle();
        //        greenStyle.FillForegroundColor = IndexedColors.LightGreen.Index;
        //        greenStyle.FillPattern = FillPattern.SolidForeground;


        //        for (int rowIndex = 0; rowIndex < data.Count; rowIndex++)
        //        {
        //            IRow row = sheet.CreateRow(rowIndex + 8);

        //            bool shouldColorRowWhite = false;
        //            bool shouldColorRowSkyBlue = false;
        //            bool shouldColorRowGreen = false;
        //            bool shouldColorYellow = false;

        //            for (int colIndex = 0; colIndex < selectedColumns.Count; colIndex++)
        //            {
        //                string columnName = selectedColumns[colIndex];

        //                if (data[rowIndex].ContainsKey(columnName))
        //                {
        //                    object cellValue = data[rowIndex][columnName];
        //                    ICell cell = row.CreateCell(colIndex);

        //                    // Set the cell value
        //                    cell.SetCellValue(cellValue?.ToString());

        //                    // Determine the row color based on conditions
        //                    if (columnName == "SL" && cellValue?.ToString() == "1")
        //                    {
        //                        shouldColorRowWhite = true;
        //                    }
        //                    if (columnName == "SL" && cellValue?.ToString() == "2")
        //                    {
        //                        shouldColorRowGreen = true;
        //                    }
        //                    if (columnName == "SL" && cellValue?.ToString() == "3")
        //                    {
        //                        shouldColorRowSkyBlue = true;
        //                    }
        //                    if (columnName == "SL" && cellValue?.ToString() == "4")
        //                    {
        //                        shouldColorYellow = true;
        //                    }
        //                }
        //            }

        //            // Apply the corresponding styles
        //            if (shouldColorRowWhite)
        //            {
        //                for (int i = 0; i < selectedColumns.Count; i++)
        //                {
        //                    ICell cell = row.GetCell(i);
        //                    if (cell != null)
        //                    {
        //                        cell.CellStyle = whiteStyle;
        //                    }
        //                }
        //            }
        //            if (shouldColorRowSkyBlue)
        //            {
        //                for (int i = 0; i < selectedColumns.Count; i++)
        //                {
        //                    ICell cell = row.GetCell(i);
        //                    if (cell != null)
        //                    {
        //                        cell.CellStyle = skyBlue;
        //                    }
        //                }
        //            }
        //            if (shouldColorRowGreen)
        //            {
        //                for (int i = 0; i < selectedColumns.Count; i++)
        //                {
        //                    ICell cell = row.GetCell(i);
        //                    if (cell != null)
        //                    {
        //                        cell.CellStyle = greenStyle;
        //                    }
        //                }
        //            }
        //            if (shouldColorYellow)
        //            {
        //                for (int i = 0; i < selectedColumns.Count; i++)
        //                {
        //                    ICell cell = row.GetCell(i);
        //                    if (cell != null)
        //                    {
        //                        cell.CellStyle = yellowStyle;
        //                    }
        //                }
        //            }
        //        }

        //        // Generate the report name
        //        reportName = $"National sales report_{reportDate:yyyyMMdd_HHmmss}.xlsx";

        //        string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ExcelReports");
        //        string filePath = Path.Combine(directoryPath, reportName);

        //        if (!Directory.Exists(directoryPath))
        //        {
        //            Directory.CreateDirectory(directoryPath);
        //        }


        //        var files = Directory.GetFiles(directoryPath);
        //        foreach (var file in files)
        //        {
        //            File.Delete(file);
        //        }

        //        using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        //        {
        //            workbook.Write(fileStream);
        //            reportName = filePath;
        //        }

        //        return reportName;
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }


        //}


        //depricated SL column 
        public string GenerateDynamicExcel(List<string> selectedColumns, List<Dictionary<string, object>> data, string reportName, string reportTypeName, string fromDate, string toDate, string zone, string region, string territory, string area, string productName)
        {
            try
            {

                if (string.IsNullOrEmpty(zone)) zone = "All";
                if (string.IsNullOrEmpty(region)) region = "All";
                if (string.IsNullOrEmpty(area)) area = "All";
                if (string.IsNullOrEmpty(territory)) territory = "All";
                if (string.IsNullOrEmpty(productName)) productName = "N/A";

                DateTime reportDate = DateTime.Now;
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("National Sales Report");


                IRow titleRow = sheet.CreateRow(0);
                titleRow.CreateCell(0).SetCellValue(reportTypeName);
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, selectedColumns.Count - 1));

                sheet.CreateRow(1).CreateCell(0).SetCellValue($"Zone Name: {zone}");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 0, selectedColumns.Count - 1));

                sheet.CreateRow(2).CreateCell(0).SetCellValue($"Region Name: {region}");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 0, selectedColumns.Count - 1));

                sheet.CreateRow(3).CreateCell(0).SetCellValue($"Area Name: {area}");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 0, selectedColumns.Count - 1));

                sheet.CreateRow(4).CreateCell(0).SetCellValue($"Territory Name: {territory}");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 0, selectedColumns.Count - 1));

                sheet.CreateRow(5).CreateCell(0).SetCellValue($"Product Name: {productName}");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(5, 5, 0, selectedColumns.Count - 1));

                sheet.CreateRow(6).CreateCell(0).SetCellValue($"Period: {fromDate} To: {toDate}");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(6, 6, 0, selectedColumns.Count - 1));


                IRow headerRow = sheet.CreateRow(7);
                int colIndexForSheet = 0;
                for (int i = 0; i < selectedColumns.Count; i++)
                {
                    if (selectedColumns[i] != "SL")
                    {
                        headerRow.CreateCell(colIndexForSheet++).SetCellValue(selectedColumns[i]);
                    }
                }


                ICellStyle whiteStyle = workbook.CreateCellStyle();
                whiteStyle.FillForegroundColor = IndexedColors.White.Index;
                whiteStyle.FillPattern = FillPattern.SolidForeground;

                ICellStyle skyBlue = workbook.CreateCellStyle();
                skyBlue.FillForegroundColor = IndexedColors.SkyBlue.Index;
                skyBlue.FillPattern = FillPattern.SolidForeground;

                ICellStyle yellowStyle = workbook.CreateCellStyle();
                yellowStyle.FillForegroundColor = IndexedColors.LightYellow.Index;
                yellowStyle.FillPattern = FillPattern.SolidForeground;

                ICellStyle greenStyle = workbook.CreateCellStyle();
                greenStyle.FillForegroundColor = IndexedColors.LightGreen.Index;
                greenStyle.FillPattern = FillPattern.SolidForeground;


                for (int rowIndex = 0; rowIndex < data.Count; rowIndex++)
                {
                    IRow row = sheet.CreateRow(rowIndex + 8);
                    bool shouldColorRowWhite = false;
                    bool shouldColorRowSkyBlue = false;
                    bool shouldColorRowGreen = false;
                    bool shouldColorYellow = false;

                    colIndexForSheet = 0;
                    for (int colIndex = 0; colIndex < selectedColumns.Count; colIndex++)
                    {
                        string columnName = selectedColumns[colIndex];

                        if (data[rowIndex].ContainsKey(columnName))
                        {
                            object cellValue = data[rowIndex][columnName];


                            if (columnName == "SL")
                            {
                                if (cellValue?.ToString() == "1") shouldColorRowWhite = true;
                                if (cellValue?.ToString() == "2") shouldColorRowGreen = true;
                                if (cellValue?.ToString() == "3") shouldColorRowSkyBlue = true;
                                if (cellValue?.ToString() == "4") shouldColorYellow = true;
                            }
                            else
                            {
                                ICell cell = row.CreateCell(colIndexForSheet++);
                                cell.SetCellValue(cellValue?.ToString());
                            }
                        }
                    }


                    ICellStyle styleToApply = shouldColorRowWhite ? whiteStyle :
                                              shouldColorRowGreen ? greenStyle :
                                              shouldColorRowSkyBlue ? skyBlue :
                                              shouldColorYellow ? yellowStyle : null;

                    if (styleToApply != null)
                    {
                        for (int i = 0; i < selectedColumns.Count - 1; i++)
                        {
                            ICell cell = row.GetCell(i);
                            if (cell != null)
                            {
                                cell.CellStyle = styleToApply;
                            }
                        }
                    }
                }


                reportName = $"National sales report_{reportDate:yyyyMMdd_HHmmss}.xlsx";
                string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ExcelReports");
                string filePath = Path.Combine(directoryPath, reportName);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }


                var files = Directory.GetFiles(directoryPath);
                foreach (var file in files)
                {
                    File.Delete(file);
                }


                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fileStream);
                    reportName = filePath;
                }

                return reportName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GenerateDynamicSheduleExcel(List<string> selectedColumns, List<Dictionary<string, object>> data, string reportName, string reportTypeName, string fromDate, string toDate, string zone, string region, string territory, string area, string productName)
        {
            try
            {

                if (string.IsNullOrEmpty(zone)) zone = "All";
                if (string.IsNullOrEmpty(region)) region = "All";
                //if (string.IsNullOrEmpty(area)) area = "All";
                //if (string.IsNullOrEmpty(territory)) territory = "All";
                //if (string.IsNullOrEmpty(productName)) productName = "N/A";

                DateTime reportDate = DateTime.Now;
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("Schedule Report");


                IRow titleRow = sheet.CreateRow(0);
                titleRow.CreateCell(0).SetCellValue(reportTypeName);
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, selectedColumns.Count - 1));

                sheet.CreateRow(1).CreateCell(0).SetCellValue($"Company Name: One Pharma Limited");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 0, selectedColumns.Count - 1));

                sheet.CreateRow(2).CreateCell(0).SetCellValue($"Date Range: {fromDate} to {toDate}");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 0, selectedColumns.Count - 1));

                sheet.CreateRow(3).CreateCell(0).SetCellValue($"Report Name: Schedule Report");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 0, selectedColumns.Count - 1));

                //sheet.CreateRow(4).CreateCell(0).SetCellValue($"Territory Name: {territory}");
                //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 0, selectedColumns.Count - 1));

                //sheet.CreateRow(5).CreateCell(0).SetCellValue($"Product Name: {productName}");
                //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(5, 5, 0, selectedColumns.Count - 1));

                //sheet.CreateRow(6).CreateCell(0).SetCellValue($"Period: {fromDate} To: {toDate}");
                //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(6, 6, 0, selectedColumns.Count - 1));


                IRow headerRow = sheet.CreateRow(7);
                int colIndexForSheet = 0;
                for (int i = 0; i < selectedColumns.Count; i++)
                {
                    if (selectedColumns[i] != "SL")
                    {
                        headerRow.CreateCell(colIndexForSheet++).SetCellValue(selectedColumns[i]);
                    }
                }


                ICellStyle whiteStyle = workbook.CreateCellStyle();
                whiteStyle.FillForegroundColor = IndexedColors.White.Index;
                whiteStyle.FillPattern = FillPattern.SolidForeground;

                ICellStyle skyBlue = workbook.CreateCellStyle();
                skyBlue.FillForegroundColor = IndexedColors.SkyBlue.Index;
                skyBlue.FillPattern = FillPattern.SolidForeground;

                ICellStyle yellowStyle = workbook.CreateCellStyle();
                yellowStyle.FillForegroundColor = IndexedColors.LightYellow.Index;
                yellowStyle.FillPattern = FillPattern.SolidForeground;

                ICellStyle greenStyle = workbook.CreateCellStyle();
                greenStyle.FillForegroundColor = IndexedColors.LightGreen.Index;
                greenStyle.FillPattern = FillPattern.SolidForeground;


                for (int rowIndex = 0; rowIndex < data.Count; rowIndex++)
                {
                    IRow row = sheet.CreateRow(rowIndex + 8);
                    bool shouldColorRowWhite = false;
                    bool shouldColorRowSkyBlue = false;
                    bool shouldColorRowGreen = false;
                    bool shouldColorYellow = false;

                    colIndexForSheet = 0;
                    for (int colIndex = 0; colIndex < selectedColumns.Count; colIndex++)
                    {
                        string columnName = selectedColumns[colIndex];

                        if (data[rowIndex].ContainsKey(columnName))
                        {
                            object cellValue = data[rowIndex][columnName];


                            if (columnName == "SL")
                            {
                                if (cellValue?.ToString() == "1") shouldColorRowWhite = true;
                                if (cellValue?.ToString() == "2") shouldColorRowGreen = true;
                                if (cellValue?.ToString() == "3") shouldColorRowSkyBlue = true;
                                if (cellValue?.ToString() == "4") shouldColorYellow = true;
                            }
                            else
                            {
                                ICell cell = row.CreateCell(colIndexForSheet++);
                                cell.SetCellValue(cellValue?.ToString());
                            }
                        }
                    }


                    ICellStyle styleToApply = shouldColorRowWhite ? whiteStyle :
                                              shouldColorRowGreen ? greenStyle :
                                              shouldColorRowSkyBlue ? skyBlue :
                                              shouldColorYellow ? yellowStyle : null;

                    if (styleToApply != null)
                    {
                        for (int i = 0; i < selectedColumns.Count - 1; i++)
                        {
                            ICell cell = row.GetCell(i);
                            if (cell != null)
                            {
                                cell.CellStyle = styleToApply;
                            }
                        }
                    }
                }


                reportName = $"National sales report_{reportDate:yyyyMMdd_HHmmss}.xlsx";
                string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ExcelReports");
                string filePath = Path.Combine(directoryPath, reportName);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }


                var files = Directory.GetFiles(directoryPath);
                foreach (var file in files)
                {
                    File.Delete(file);
                }


                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fileStream);
                    reportName = filePath;
                }

                return reportName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<JsonViewModel> GetProductWiseSpecificationIdByName(int? userId, string productCode)
        {
            try
            {

                var result = await _context.jsonViewModels.FromSql($"SalSpGetProductSpecIdByProductName {userId}, {productCode}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Territory sales transfer
        public async Task<JsonViewModel> GetTerritoryForTerritoryTransfer(int RegionID, int employeeId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getTerritoryForTerritoryTransfer {RegionID},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public async Task<bool> TransferTerritoryData(int? userId, string fromTerritoryCode, string toTerritoryCode)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"spTransferTerritoryData {userId}, {fromTerritoryCode}, {toTerritoryCode}").AsNoTracking().FirstOrDefaultAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<int> UpdateFrizzProductStatus(string id, IEnumerable<FrizzProductViewModel> models)
        {
            try
            {
                //foreach (var item in models.Where(c => c.isSelect))
                foreach (var item in models)
                {
                    string batchNumber = string.IsNullOrEmpty(item.batchNumbers) ? null : item.batchNumbers;

                    var result = await _context.saveUpdateViewModels.FromSql($"UpdateProductWiseSpecForFrizzTransaction {id}, {item.productWiseSpecificationId},{batchNumber},{item.isSelect}").AsNoTracking().FirstOrDefaultAsync();
                    if (!result.isSuccess)
                        return 0;

                }
                return 1;


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<JsonViewModel> GetAppVersion(int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetAppVersion {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> SetAppVersion(string id, int appVersion, int newVersion)
        {
            try
            {


                var result = await _context.saveUpdateViewModels.FromSql($"SalSpSetAppVersion {id}, {appVersion},{newVersion}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SalesOrder Details
        public async Task<JsonViewModel> GetSalesOrderDetailsByMasterId(int? salesOrderId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesOrderDetailsJSON {salesOrderId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        #endregion
    }
}
