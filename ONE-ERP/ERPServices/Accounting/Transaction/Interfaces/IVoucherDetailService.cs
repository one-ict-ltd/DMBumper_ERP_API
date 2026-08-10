
using ONEERP.Areas.Accounting.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.Transaction.Interfaces
{
    public interface IVoucherDetailService 
    {
        Task<bool> SaveVoucherDetails(string Id, List<VoucherDetailViewModel> voucherDetailViewModels, List<CostCentreAllocationViewModel> costCentreAllocationViewModels, List<VoucherAttachmentlViewModel> voucherAttachmentList, int voucherMasterId, int? isPosted);
        Task<IEnumerable<VoucherDetailListViewModel>> GetVoucherDetailList();
        Task<IEnumerable<VoucherDetailListViewModel>> GetVoucherMasterListbyVoucherMasterId(int voucherMasterId);
        Task<bool> DeleteVoucherDetailById(string Id, int voucherMasterId, int voucherDetailId);
        Task<JsonViewModel> GetVoucherDetailListbyVoucherMasterIdJson(int voucherMasterId);
        Task<JsonViewModel> GetVoucherAttachmentListbyVoucherMasterIdJson(int voucherMasterId, int voucherAttachmentId);


    }
}
