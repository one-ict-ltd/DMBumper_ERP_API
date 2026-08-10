using ONEERP.Areas.Accounting.Models;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.Transaction.Interfaces
{
    public interface IChequeBookService
    {
        #region ChequeBook Master
        Task<int> SaveChequeBookMaster(string id, ChequeBookMasterViewModel chequeBookMasterViewModel);
        Task<JsonViewModel> GetChequeBookMasterById(int companyId, int sbuId, int chequeBookMasterId);
        Task<bool> DeleteChequeBookMasterById(string id, int chequeBookMasterId);
        Task<JsonViewModel> GetVoucherForCreateCheque(int companyId, int sbuId);

        #endregion

        #region ChequeBook Details
        Task<int> SaveChequeBookDetails(string id, List<ChequeBookDetailsViewModel> chequeBookDetailsViewModel, int chequeBookMasterId);
        Task<JsonViewModel> GetChequeBookDetailsByMasterId(int chequeBookMasterId);
        Task<bool> DeleteChequeBookDetailsById(string id, int chequeBookDetailsId);

        #endregion      

    }
}
