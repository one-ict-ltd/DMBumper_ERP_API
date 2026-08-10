using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Accounting.Models;
using ONEERP.Areas.Auth.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Accounting.Transaction.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.Transaction
{
    public class VoucherMasterService : IVoucherMasterService
    {
        private readonly ERPDbContext _context;

        public VoucherMasterService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<int> SaveVoucherMaster(string Id, VoucherMasterViewModel voucherMasterViewModel)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetVoucherMasters {Id},{voucherMasterViewModel.voucherMasterId},{voucherMasterViewModel.voucherDate},{voucherMasterViewModel.refNo},{voucherMasterViewModel.voucherTypeId},{voucherMasterViewModel.remarks},{voucherMasterViewModel.isPosted},{voucherMasterViewModel.voucherAmount},{voucherMasterViewModel.fundSourceId},{voucherMasterViewModel.companyId},{voucherMasterViewModel.sbuId},{voucherMasterViewModel.isActive},{voucherMasterViewModel.editRemarks}").AsNoTracking().FirstOrDefaultAsync();
           
           
            return result.isSuccess;
        }
        public async Task<int> SaveVoucherMasterExcel(string Id, VoucherMasterViewModelExcel voucherMasterViewModel)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetVoucherMasters {Id},{voucherMasterViewModel.voucherMasterId},{voucherMasterViewModel.voucherDate},{voucherMasterViewModel.refNo},{voucherMasterViewModel.voucherTypeId},{voucherMasterViewModel.remarks},{voucherMasterViewModel.isPosted},{voucherMasterViewModel.voucherAmount},{voucherMasterViewModel.fundSourceId},{voucherMasterViewModel.companyId},{voucherMasterViewModel.sbuId},{voucherMasterViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
           
           
            return result.isSuccess;
        }
        public async Task<VoucherMasterViewModel> ConvertVoucherExcelToVoucherMaster(VoucherMasterViewModelExcel voucherMasterViewModel)
        {
            var retObj = new VoucherMasterViewModel();
            
            retObj.refNo = voucherMasterViewModel.refNo;
            retObj.voucherDate = voucherMasterViewModel.voucherDate;
            retObj.voucherMasterId = voucherMasterViewModel.voucherMasterId;
            retObj.fundSourceId = voucherMasterViewModel.fundSourceId;
            retObj.companyId = voucherMasterViewModel.companyId;
            retObj.sbuId = voucherMasterViewModel.sbuId;
            retObj.isPosted = voucherMasterViewModel.isPosted;
            retObj.remarks = voucherMasterViewModel.remarks;
            retObj.voucherAmount = voucherMasterViewModel.lstMaster.Sum(x=> x.crAmount);
            retObj.ChequeNo = voucherMasterViewModel.ChequeNo;
            retObj.voucherTypeId = voucherMasterViewModel.voucherTypeId;

            foreach (var item in voucherMasterViewModel.lstMaster.GroupBy(x=> new { x.ledgerId, x.remarks }))
            {
                var itemdetails = item.FirstOrDefault();
                if (item.Sum(y=> y.drAmount) > 0)
                {
                    var vouchermasterDetails = new VoucherDetailViewModel();
                    vouchermasterDetails.amount = item.Sum(y => y.drAmount);
                    vouchermasterDetails.remarksDetail = itemdetails.remarks;
                    vouchermasterDetails.partyId= itemdetails.partyId ?? 0;
                    vouchermasterDetails.ledgerId = itemdetails.ledgerId;
                    vouchermasterDetails.accountName = itemdetails.accountName;
                    vouchermasterDetails.partyName = itemdetails.party;
                    vouchermasterDetails.isPrinAcc = false;
                    vouchermasterDetails.transactionModeId = 1;
                    retObj.lstdetailmodel.Add(vouchermasterDetails);
                }

                if (item.Sum(y => y.crAmount) > 0)
                {
                    var vouchermasterDetails = new VoucherDetailViewModel();
                    vouchermasterDetails.amount = item.Sum(y => y.crAmount);
                    vouchermasterDetails.remarksDetail = itemdetails.remarks;
                    vouchermasterDetails.partyId = itemdetails.partyId ?? 0;
                    vouchermasterDetails.ledgerId = itemdetails.ledgerId;
                    vouchermasterDetails.accountName = itemdetails.accountName;
                    vouchermasterDetails.partyName = itemdetails.party;
                    vouchermasterDetails.isPrinAcc = false;
                    vouchermasterDetails.transactionModeId = 2;
                    retObj.lstdetailmodel.Add(vouchermasterDetails);
                }
                if(item.Count() > 1)
                {
                    foreach (var cc in item)
                    {
                        var costCenter = new CostCentreAllocationViewModel();
                        costCenter.costCentreId = cc.costCentreId;
                        costCenter.ledgerId= cc.ledgerId;
                        costCenter.amount = cc.crAmount > 0 ? (decimal)cc.crAmount : (decimal)cc.drAmount;
                        retObj.lstcostmodel.Add(costCenter);
                    }

                }
            }
            if(retObj.voucherTypeId == 2)
            {
                var pinacc = retObj.lstdetailmodel.FirstOrDefault(c=> c.transactionModeId== 2);
                if (pinacc != null)
                {
                    pinacc.isPrinAcc = true;
                }
            }
            else if (retObj.voucherTypeId == 4)
            {
                var pinacc = retObj.lstdetailmodel.FirstOrDefault(c => c.transactionModeId == 1);
                if (pinacc != null)
                {
                    pinacc.isPrinAcc = true;
                }
            }
           

            return retObj;
        }
        public async Task<int> UpdateVoucherMaster(string Id, int ispost, VoucherPostingViewModel voucherPostingViewModel)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"updateVoucherMasterPosting {Id},{voucherPostingViewModel.voucherMasterId},{ispost},{voucherPostingViewModel.comments}").AsNoTracking().FirstOrDefaultAsync();
           
           
            return result.isSuccess;
        }
        public async Task<IEnumerable<VoucherMasterListViewModel>> GetVoucherMasterList()
        {
            var result = await _context.voucherMasterListViewModels.FromSql($"AccSpGetVoucherMaster {0},{0},{0},{0}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<VoucherMasterListViewModel>> GetVoucherMasterListbyVoucherMasterId(int voucherMasterId)
        {
            var result = await _context.voucherMasterListViewModels.FromSql($"AccSpGetVoucherMaster {voucherMasterId},{0},{0},{0}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<JsonViewModel> GetVoucherMasterListbyVoucherMasterIdJson(int voucherMasterId, int voucherTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetVoucherMasterJson {voucherMasterId},{voucherTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        } 

        public async Task<JsonViewModel> GetUploadedVoucherListJson(int userId, int voucherTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetUploadedVoucherJson {userId}, {voucherTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        } 

        public async Task<JsonViewModel> GetVoucherMasterListbyVoucherMasterIdDateJson(int voucherMasterId, int voucherTypeId, DateTime fromDate, DateTime toDate,int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetVoucherMasterWithDateJson {voucherMasterId},{voucherTypeId},{fromDate},{toDate},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        } 

        public async Task<JsonViewModel> GetVoucherEditDeleteCheckJson(int voucherMasterId, int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpVoucherEditDeleteCheckJson {voucherMasterId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        } 

        public async Task<JsonViewModel> GetVoucherMasterListbyVoucherMasterForPostingIdJson(int employeeId, int voucherMasterId, int voucherTypeId,int isPost)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"AccSpGetVoucherMasterForPostingJson {voucherMasterId},{voucherTypeId},{isPost},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        } 

        public async Task<JsonViewModel> GetVoucherMasterListbyVoucherMasterForPostingIdFactoryJson(int voucherMasterId, int voucherTypeId,int isPost)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetVoucherMasterForPostingFactoryJson {voucherMasterId},{voucherTypeId},{isPost}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }  

        public async Task<JsonViewModel> GetVoucherNoJson(int voucherType, DateTime voucherDate,int isCheque)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"AccSpGetVoucherNo {voucherType},{voucherDate}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
          
        }
        public async Task<IEnumerable<VoucherMasterListViewModel>> GetVoucherMasterListbyVoucherDate(DateTime voucherDate)
        {
            var result = await _context.voucherMasterListViewModels.FromSql($"AccSpGetVoucherMaster {0},{voucherDate},{0},{0}").AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<IEnumerable<VoucherMasterListViewModel>> GetVoucherMasterListbyVoucherTypeId(int voucherTypeId)
        {
            var result = await _context.voucherMasterListViewModels.FromSql($"AccSpGetVoucherMaster {0},{0},{voucherTypeId},{0}").AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<IEnumerable<VoucherMasterListViewModel>> GetVoucherMasterListbyPostStatus(int isPosted)
        {
            var result = await _context.voucherMasterListViewModels.FromSql($"AccSpGetVoucherMaster {0},{0},{0},{isPosted}").AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<bool> DeleteVoucherMasterById(string Id, int voucherMasterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteVoucherMaster {Id},{voucherMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetBalanceAmountByLedgerJson(int ledgerId, int? partyId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetBalanceAmount {ledgerId},{partyId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> CheckLockFiscalYear(string voucherDate)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpCheckLockYear {voucherDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
    }
}
