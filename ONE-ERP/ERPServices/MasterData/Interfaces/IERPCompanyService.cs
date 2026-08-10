
using ONEERP.Data.Entity.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.MasterData.Interfaces
{
    public interface IERPCompanyService
    {
        Task<int> SaveERPCompany(CmnCompany company);

        void UpdateCompanyLogoById(int compId, string fileName, string fileLocation);

        Task<IEnumerable<CmnCompany>> GetAllCompany();

        Task<CmnCompany> GetCompanyById(int id);

        Task<bool> DeleteCompanyById(int id);
    }
}
