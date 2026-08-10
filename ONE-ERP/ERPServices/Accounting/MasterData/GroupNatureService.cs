using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Accounting.Models;

using ONEERP.Data;
using ONEERP.ERPServices.Accounting.MasterData.Interfaces;
using ONEERP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Accounting.MasterData
{
    public class GroupNatureService : IGroupNatureService
    {
        private readonly ERPDbContext _context;

        public GroupNatureService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveGroupNature(string Id, GroupNatureViewModel groupNatureViewModel)
        {

            var result = await _context.saveUpdateViewModels.FromSql($"AccSpSetGroupNature {Id},{groupNatureViewModel.groupNatureId},{groupNatureViewModel.natureName},{groupNatureViewModel.printOrder},{groupNatureViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<IEnumerable<GroupNatureListViewModel>> GetGroupNature()
        {
            var result = await _context.groupNatureListViewModels.FromSql($"AccSpGetGroupNature {0}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<GroupNatureListViewModel> GetGroupNatureById(int id)
        {
            var result = await _context.groupNatureListViewModels.FromSql($"AccSpGetGroupNature {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        } 
        public async Task<JsonViewModel> GetGroupNatureByIdJson(int id)
        {
            var result = await _context.jsonViewModels.FromSql($"AccSpGetGroupNatureJson {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteGroupNatureById(string Id, int groupNatureId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"AccSpDeleteGroupNature {Id},{groupNatureId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
    }
}
