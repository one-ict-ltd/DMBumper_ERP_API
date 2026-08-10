using ONEERP.Areas.MasterData.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.MasterData.Interfaces
{
    public interface ICompanyService
    {
        #region Company Category
        Task<JsonViewModel> GetCompanyCategory(int employeeId);
        #endregion

        #region Company
        Task<bool> SaveCompany(string Id, CompanyViewModel company);
        Task<IEnumerable<CompanyListViewModel>> GetAllCompany();
        Task<CompanyListViewModel> GetCompanyById(int Id);
        Task<JsonViewModel> GetCompanyJsonById(int Id);
        Task<JsonViewModel> GetProbationPeriodJson();
        Task<JsonViewModel> getSeparationType();
        Task<bool> DeleteCompanyById(string Id, int CompanyId);
        #endregion
    }
}
