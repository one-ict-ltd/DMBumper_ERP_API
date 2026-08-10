using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Accounting.Models;
using ONEERP.Areas.Auth.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Accounting.Transaction.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.Transaction
{
    public class VoucherDetailService : IVoucherDetailService
    {
        private readonly ERPDbContext _context;

        public VoucherDetailService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveVoucherDetails(string Id, List<VoucherDetailViewModel> voucherDetailViewModels, List<CostCentreAllocationViewModel> costCentreAllocationViewModels, List<VoucherAttachmentlViewModel> voucherAttachmentList, int voucherMasterId,int? isPosted)
        {
            try
            {
                await _context.saveUpdateViewModels.FromSql($"AccSpDeleteVoucherDetails {Id},{voucherMasterId},{0}").AsNoTracking().FirstOrDefaultAsync();
                await _context.saveUpdateViewModels.FromSql($"AccSpDeleteVoucherAttachment {Id},{voucherMasterId},{0}").AsNoTracking().FirstOrDefaultAsync();
                await _context.saveUpdateViewModels.FromSql($"AccSpDeleteCostCentreAllocationbyVoucherMasterId {Id},{voucherMasterId}").AsNoTracking().FirstOrDefaultAsync();
                var result = new SaveUpdateValueViewModel();
                var resultc = new SaveUpdateViewModel();
                foreach (VoucherDetailViewModel voucherDetailViewModel in voucherDetailViewModels)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetVoucherDetails {Id},{0},{voucherMasterId},{voucherDetailViewModel.ledgerId},{voucherDetailViewModel.partyId},{voucherDetailViewModel.amount},{voucherDetailViewModel.transactionModeId},{voucherDetailViewModel.isPrinAcc},{voucherDetailViewModel.isActive},{voucherDetailViewModel.accountName},{voucherDetailViewModel.partyName},{voucherDetailViewModel.remarksDetail}").AsNoTracking().FirstOrDefaultAsync();


                    if (result.isSuccess > 0 && costCentreAllocationViewModels.Any(x => x.ledgerId == voucherDetailViewModel.ledgerId && x.partyId == voucherDetailViewModel.partyId))
                    {
                        foreach (CostCentreAllocationViewModel costCentreAllocationViewModel in costCentreAllocationViewModels.Where(x => x.ledgerId == voucherDetailViewModel.ledgerId && x.partyId == voucherDetailViewModel.partyId))
                        {
                            resultc = await _context.saveUpdateViewModels.FromSql($"AccSpSetCostCentreAllocation {Id},{costCentreAllocationViewModel.costCentreAllocationId},{costCentreAllocationViewModel.costCentreId},{voucherMasterId},{result.isSuccess},{costCentreAllocationViewModel.amount},{costCentreAllocationViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
                        }
                    }
                    else if (result.isSuccess > 0 && !costCentreAllocationViewModels.Any(x => x.ledgerId == voucherDetailViewModel.ledgerId))
                    {

                        resultc.isSuccess = true;
                    }
                    else
                    {
                        resultc.isSuccess = false;
                    }

                }

                foreach (var item in voucherAttachmentList)
                {
                    if (item.voucherAttachmentId > 0 && item.fileString == null)
                    {
                        result = await _context.saveUpdateValueViewModels.FromSql($"AccSetVoucherAttachment {Id},{voucherMasterId},{item.voucherAttachmentId},{item.fileName},{item.remarks},{item.attachmentUrl}").AsNoTracking().FirstOrDefaultAsync();
                        continue;
                    }
                    string[] res = item.fileString?.Split(',');
                    if (res?.Length > 1)
                    {
                        Byte[] bytes = Convert.FromBase64String(res[1]);
                        string servePath = ("./wwwroot/VoucherAttachment");
                        if (!System.IO.Directory.Exists(servePath)) System.IO.Directory.CreateDirectory(servePath);
                        string fileName = ($"{DateTime.Now.Ticks}.{item.ext}");
                        string filePath = ($"{servePath}/{fileName}");
                        File.WriteAllBytes(filePath, bytes);

                        item.attachmentUrl = filePath;
                    }


                    result = await _context.saveUpdateValueViewModels.FromSql($"AccSetVoucherAttachment {Id},{voucherMasterId},{item.voucherAttachmentId},{item.fileName},{item.remarks},{item.attachmentUrl}").AsNoTracking().FirstOrDefaultAsync();

                }
                return resultc.isSuccess && (result.isSuccess > 0);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public async Task<IEnumerable<VoucherDetailListViewModel>> GetVoucherDetailList()
        {
            var result = await _context.voucherDetailListViewModels.FromSql($"AccSpGetVoucherDetails {0}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<VoucherDetailListViewModel>> GetVoucherMasterListbyVoucherMasterId(int voucherMasterId)
        {
            var result = await _context.voucherDetailListViewModels.FromSql($"AccSpGetVoucherDetails {voucherMasterId}").AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<JsonViewModel> GetVoucherDetailListbyVoucherMasterIdJson(int voucherMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetVoucherDetailsJson {voucherMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetVoucherAttachmentListbyVoucherMasterIdJson(int voucherMasterId, int voucherAttachmentId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetVoucherAttachmentJson {voucherMasterId},{voucherAttachmentId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteVoucherDetailById(string Id, int voucherMasterId, int voucherDetailId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteVoucherDetails {Id},{voucherMasterId},{voucherDetailId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
    }
}
