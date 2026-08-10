using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Production.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Production.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Production
{
    public class ProductionIssueAndReceivedService:IProductionIssueAndReceived
    {
        private readonly ERPDbContext _context;
        public ProductionIssueAndReceivedService(ERPDbContext context)
        {
            _context = context;
        }



        public async Task<JsonViewModel> GetIssueById(int? issueId,string typeOfIssue)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetProductIssue {issueId},{typeOfIssue}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetIssueByIdDate(int? userId, DateTime fromDate, DateTime toDate, int? issueId, string typeOfIssue)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetProductIssueDate {fromDate},{toDate}, {issueId},{typeOfIssue},{userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> SaveIssueMaster(string Id, ProductionIssueViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSaveProductionIssueMaster {Id}, {model.productIssueMasterId}, {model.issueNo}, {model.issueDate}, {model.typeOfIssue}, {model.requisitionId}, {model.issueQty}, {model.issueStatus}, {model.issueRemarks}, {model.storeId},{model.bomForId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> DeleteIssueById(string Id, int issueId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpDeleteProductIssueById {Id}, {issueId}").AsNoTracking().FirstOrDefaultAsync();
                return result.data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> SaveIssueDetails(string Id, List<ProductionIssueDetailViewModel> model, int issueId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (var data in model)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSaveIssueDetails {Id}, {data.productIssueDetailId},{data.requisitinDetailId}, {issueId}, {data.qty}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetIssueDetailsByMasterId(int? requisitionId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetProductIssueDetailsByMasterId {requisitionId}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
    

        public Task<bool> DeleteIssueDetailsById(string userId, int issueDetailsId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveReceiveMaster(string Id, ProductionReceiveViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpGetSaveProductReceiveMaster {Id}, {model.productReceiveMasterId}, {model.receiveNo}, {model.receiveDate}, {model.typeOfreceive}, {model.productIssueMasterId}, {model.receiveQty}, {model.receiveStatus}, {model.receiveRemarks},{model.bomForId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> DeleteReceiveById(string Id, int receiveId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpDeleteProductReceiveById {Id}, {receiveId}").AsNoTracking().FirstOrDefaultAsync();
                return result.data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetReceiveById(int? receiveId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetProductReceive {receiveId}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetReceiveByIdDate(int? userId, DateTime fromDate, DateTime toDate, int? receiveId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetProductReceiveDate {fromDate}, {toDate}, {receiveId}, {userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> SaveReceiveDetails(string Id, List<ProductionReceiveDetailViewModel> model, int receiveId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (var data in model)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSaveProductReceiveDetail {Id}, {data.productReceiveDetailId},{data.issueDetailId}, {receiveId}, {data.qty},{data.potency},{data.grnNo}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetReceiveDetailsByMasterId(int? receiveId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetProductReceiveDetailsByMasterId {receiveId}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> DeleteReceiveDetailsById(string userId, int receiveDetailsId)
        {
            throw new NotImplementedException();
        }

        public async Task<JsonViewModel> GetMaxReceiveMasterNumber(DateTime date, int type)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetMaxReceiveNo {date.ToString("yyyy-MMM-dd")},{type}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetIssueNoForReceive(int type, int? userId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getIssueNoForReceive {type},{userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetIssueDataById(int? issueId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetIssueDataForReceive {issueId}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetIssueDetailsByMasterIdForReceive(int? issueId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetIssueDetailsDataForReceive {issueId}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetRMPMReturnDetailsByReqMasterId(int? requisitionId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetRMPMReturnDetailsByReqMasterId {requisitionId}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetReturnDetailsByReturnMasterId(int? ProductReturnMasterId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetReturnDetailsByReturnMasterId {ProductReturnMasterId}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task<JsonViewModel> GetRequisitionNoForIssue(int type)
        //{
        //    try
        //    {
        //        var result = await _context.jsonViewModels.FromSql($"getRequisitionNoForIssue {type}").AsNoTracking().FirstOrDefaultAsync();
        //        return result;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public async Task<JsonViewModel> GetRMRequisitionById(int? requisitionId)
        //{
        //    try
        //    {
        //        var result = await _context.jsonViewModels.FromSql($"PrdSpGetRMRequisitionJsonData {requisitionId}").AsNoTracking().FirstOrDefaultAsync();
        //        return result;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public async Task<JsonViewModel> GetMaxReturnMasterNumber(DateTime ReturnDate, int type)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetMaxReturnNo {ReturnDate.ToString("yyyy-MMM-dd")},{type}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetRequisitionNumberforReturn(int type, int? userId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getRequisitionNoForReturn {type},{userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> SaveProductReturn(string Id, ProductionReturnViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSaveProductReturnMaster {Id}, {model.productReturnMasterId}, {model.returnNo}, {model.returnDate}, {model.TypeofReturn}, {model.productIssueMasterId},  {model.Status}, {model.remarks},{model.bomForId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> SaveProductReturnDetails(string Id, List<ProductionReturnDetailViewModel> model, int returnId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (var data in model)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSaveProductReturnDetails {Id}, {data.productReturnDetailId},{data.productIssueDetailId}, {returnId}, {data.returnQty},{data.potency},{data.grnNo},{data.grnDetailsId}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetReturnByIdDate(DateTime fromDate, DateTime toDate, int? returnId,int? userId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetReturnByIdandDate {fromDate}, {toDate}, {returnId},{userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> DeleteReturnMasterById(string Id, int ReturnMasterId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpDeleteReturnMasterById {Id}, {ReturnMasterId}").AsNoTracking().FirstOrDefaultAsync();
                return result.data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveProductReceiveFromReturn(string Id, ProductReceiveFromReturnViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSaveProductReceiveFromReturn {Id}, {model.ProductReceiveFromReturnMasterId}, {model.ProductReceiveFromReturnDate}, {model.TypeofReceive}, {model.ProductReturnMasterId}, {model.status},  {model.remarks},{model.bomForId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> SaveProductReceiveFromReturnDetails(string Id, List<ProductReceiveFromReturnDetailViewModel> model, int returnId)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (var data in model)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSaveProductReceiveFromReturnDetails {Id}, {data.ProductReceiveFromReturnDetailId},{data.productReturnDetailId}, {returnId}, {data.ProductIssueDetailId},{data.potency},{data.grnNo},{data.receivedQty},{data.grnDetailsId}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public async Task<JsonViewModel> GetReturnFromReceiveByIdDate(DateTime fromDate, DateTime toDate, int? ProductReceiveFromReturnMasterId,int? userId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetReturnFromReceiveByIdDate {fromDate}, {toDate}, {ProductReceiveFromReturnMasterId},{userId}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> DeleteProductReceiveFromReturnById(string Id, int ProductReceiveFromReturnMasterId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpDeleteProductReceiveFromReturnById {Id}, {ProductReceiveFromReturnMasterId}").AsNoTracking().FirstOrDefaultAsync();
                return result.data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> GetProductReceiveFromReturnDetails(int? ProductReceiveFromReturnMasterId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"PrdSpGetProductReceiveFromReturnDetails {ProductReceiveFromReturnMasterId}").AsNoTracking().FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
