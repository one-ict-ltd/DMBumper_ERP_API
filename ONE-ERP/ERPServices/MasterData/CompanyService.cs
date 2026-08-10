using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.MasterData.Models;
using ONEERP.Data;
using ONEERP.ERPServices.MasterData.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.MasterData
{
    public class CompanyService : ICompanyService
    {
        private readonly ERPDbContext _context;

        public CompanyService(ERPDbContext context)
        {
            _context = context;
        }

        #region Company Category
        public async Task<JsonViewModel> GetCompanyCategory(int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetCompanyCategory {employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region Company

        public async Task<bool> SaveCompany(string Id,CompanyViewModel company)
        {
           
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetCompany {Id},{company.companyId},{company.companyName},{company.aliasName},{company.ownerName},{company.managerName},{company.tradeLicense},{company.businessNature},{company.officeTelephone},{company.vatNo},{company.tinNo},{company.dateOfEstablishment},{company.permanentEmployee},{company.companyEmail},{company.alternetEmail},{company.liquidityRatio},{company.filePath},{company.filePathTwo},{company.filePathThree},{company.addressLine},{company.isActive},{company.website},{company.companyBankAccName},{company.companyBankAccNo},{company.imageHeight},{company.imageWidth}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }      

        public async Task<IEnumerable<CompanyListViewModel>> GetAllCompany()
        {

            var result = await _context.companyListViewModels.FromSql($"CmnSpGetCompany {0}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<CompanyListViewModel> GetCompanyById(int Id)
        {
            var result = await _context.companyListViewModels.FromSql($"CmnSpGetCompany {Id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        } 
        public async Task<JsonViewModel> GetCompanyJsonById(int Id)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetCompanyJson {Id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetProbationPeriodJson()
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetProbationPeriodJson").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> getSeparationType()
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetSeparateTypeJson").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteCompanyById(string Id, int CompanyId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteCompany {Id},{CompanyId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion
    }
}
