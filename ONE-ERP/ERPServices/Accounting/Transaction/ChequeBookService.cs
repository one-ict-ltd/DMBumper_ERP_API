using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Accounting.Models;
using ONEERP.Areas.Auth.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Accounting.Transaction.Interfaces;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.Transaction
{
    public class ChequeBookService : IChequeBookService
    {
        private readonly ERPDbContext _context;

        public ChequeBookService(ERPDbContext context)
        {
            _context = context;
        }

        #region ChequeBook Master
        public async Task<int> SaveChequeBookMaster(string id, ChequeBookMasterViewModel chequeBookMasterViewModel)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetChequeBookMaster {id},{chequeBookMasterViewModel.chequeBookMasterId},{chequeBookMasterViewModel.chequeBookId},{chequeBookMasterViewModel.bankName},{chequeBookMasterViewModel.accountName},{chequeBookMasterViewModel.accountNumber},{chequeBookMasterViewModel.chequeNumberCurrent},{chequeBookMasterViewModel.chequeNumberStarting},{chequeBookMasterViewModel.chequeDate},{chequeBookMasterViewModel.chequeAmount},{chequeBookMasterViewModel.isAccountPayee},{chequeBookMasterViewModel.isBearer},{chequeBookMasterViewModel.isNonNegotiable},{chequeBookMasterViewModel.isPayableOndateOnly},{chequeBookMasterViewModel.isVoid},{chequeBookMasterViewModel.isPrinted},{chequeBookMasterViewModel.isCleared},{chequeBookMasterViewModel.isWithoutDate},{chequeBookMasterViewModel.companyId},{chequeBookMasterViewModel.sbuId},{chequeBookMasterViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();           
           
            return result.isSuccess;
        }   
       
        public async Task<JsonViewModel> GetChequeBookMasterById(int companyId, int sbuId, int chequeBookMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetChequeBookMasterJson {companyId},{sbuId},{chequeBookMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }         
       
        public async Task<bool> DeleteChequeBookMasterById(string id, int chequeBookMasterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteChequeBookMaster {id},{chequeBookMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetVoucherForCreateCheque(int companyId, int sbuId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetVoucherForCreateCheque {companyId},{sbuId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region ChequeBook Details

        public async Task<int> SaveChequeBookDetails(string id, List<ChequeBookDetailsViewModel> chequeBookDetailsViewModels, int chequeBookMasterId)
        {
            await _context.saveUpdateViewModels.FromSql($"AccSpDeleteChequeBookDetails {id},{chequeBookMasterId},{0}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateValueViewModel();
            foreach (ChequeBookDetailsViewModel chequeBookDetailsViewModel in chequeBookDetailsViewModels)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"AccSpSetChequeBookDetails {id},{0},{chequeBookMasterId},{chequeBookDetailsViewModel.voucherDetailsId},{chequeBookDetailsViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetChequeBookDetailsByMasterId(int chequeBookMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetChequeBookDetailsJson {chequeBookMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteChequeBookDetailsById(string id, int chequeBookDetailsId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteChequeBookDetails {id},{0},{chequeBookDetailsId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

    }
}
