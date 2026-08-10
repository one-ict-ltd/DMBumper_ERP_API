using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Sales.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Sales.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Sales
{
    public class SalesExecutiveMemberService: ISalesExecutiveMemberService
    {
        private readonly ERPDbContext _context;
        public SalesExecutiveMemberService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveExecutiveMember(int? userId, List<SalExecutiveTeamViewModel> salExecutiveTeamViewModels)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (SalExecutiveTeamViewModel m in salExecutiveTeamViewModels)
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetExecutiveMember {userId},{m.ExecutiveTeamId},{m.TeamLeaderId},{m.TeamMemberId}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<JsonViewModel> GetExecutiveMember(int? executiveTeamId)
        {
            executiveTeamId = executiveTeamId ?? 0;
            var result = await _context.jsonViewModels.FromSql($"salSpGetExecutiveMemberJSON {executiveTeamId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteExecutiveMember(int? userId, int executiveTeamId)
        {
            try
            {
                var result = new SaveUpdateViewModel();
                result = await _context.saveUpdateViewModels.FromSql($"SalSpDeleteExecutiveMember {userId},{executiveTeamId}").AsNoTracking().FirstOrDefaultAsync();

                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
