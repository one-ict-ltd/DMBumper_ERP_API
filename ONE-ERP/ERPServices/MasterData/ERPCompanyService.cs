using Microsoft.EntityFrameworkCore;
using ONEERP.Data;
using ONEERP.Data.Entity.Common;
using ONEERP.ERPServices.MasterData.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.MasterData
{
    public class ERPCompanyService: IERPCompanyService
    {
        private readonly ERPDbContext _context;

        public ERPCompanyService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveERPCompany(CmnCompany company)
        {
            if (company.companyId != 0)
                _context.CmnCompanys.Update(company);
            else
                _context.CmnCompanys.Add(company);
             await _context.SaveChangesAsync();
            return company.companyId;
        }

        public void UpdateCompanyLogoById(int compId, string fileName,string fileLocation)
        {
            var user = _context.CmnCompanys.Find(compId);
            //user.fileName = fileName;
            //user.filePath = fileLocation;
            _context.Entry(user).State = EntityState.Modified;
            
            _context.SaveChanges();
        }

        public async Task<IEnumerable<CmnCompany>> GetAllCompany()
        {
            var result= await _context.CmnCompanys.OrderBy(a => a.companyId).Take(1).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<CmnCompany> GetCompanyById(int Id)
        {
            return await _context.CmnCompanys.FindAsync(Id);
        }

        public async Task<bool> DeleteCompanyById(int id)
        {
            _context.CmnCompanys.Remove(_context.CmnCompanys.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
