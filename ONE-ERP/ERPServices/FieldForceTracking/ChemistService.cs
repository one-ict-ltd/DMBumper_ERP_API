using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.FieldForceTracking.Models;
using ONEERP.Data;
using ONEERP.Data.Entity.FieldForceTracking;
using ONEERP.ERPServices.FieldForceTracking.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ONEERP.Areas.Auth.Models;

namespace ONEERP.ERPServices.FieldForceTracking
{
    public class ChemistService : IChemistService
    {
        private readonly ERPDbContext _context;

        public ChemistService(ERPDbContext context)
        {
            _context = context;
        }        

        public async Task<IEnumerable<CmnChemist>> GetAllCmnChemist()
        {
            return await _context.CmnChemist.Where(x => x.IsActive == 1 && x.IsDeleted == 0).AsNoTracking().ToListAsync();
        }
        public async Task<CmnChemist> GetCmnChemistbyId(int Id)
        {
            return await _context.CmnChemist.Where(x => x.ChemistID == Id).AsNoTracking().FirstOrDefaultAsync();
        }
        
        public async Task<JsonViewModel> GetChemistList(int Id)
        {
            try
            {
                var data= await _context.chemistListViewModelLoads.FromSql($"getChemistnlist {Id}").AsNoTracking().ToListAsync();
                // var result = await _context.jsonViewModels.FromSql($"getChemist {Id}").AsNoTracking().FirstOrDefaultAsync();
                JsonViewModel result = new JsonViewModel();
                result.data= JsonSerializer.Serialize(data);
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }      
        }

        public async Task<IEnumerable<ChemistListAPIViewModel>> GetChemistListAPIViewModel(string Id)
        {
            var result = await _context.chemistListAPIViewModels.FromSql($"getChemistlist {Id}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ChemistListAPIViewModel>> GetChemistListAPIViewModelBycode(string Id,string code)
        {
            var result = await _context.chemistListAPIViewModels.FromSql($"getChemistlistByCode {Id},{code}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<JsonViewModel> GetChemistListAPIViewModelJson(string Id,string employeeNo)
        {
            var result = await _context.jsonViewModels.FromSql($"getChemistlistJson {Id},{employeeNo}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetChemistListAPIViewModelJsonWithConversionCode(string Id, string employeeNo)
        {
            var result = await _context.jsonViewModels.FromSql($"getChemistlistJsonWithConversionCode {Id},{employeeNo}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<IEnumerable<ChemistListAPIViewModel>> GetChemistListAPIbyMktViewModel(string Id)
        {
            var result = await _context.chemistListAPIViewModels.FromSql($"getChemistlistbyMkt {Id}").AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<IEnumerable<ChemistDataViewModel>> GetChemistDataViewModels()
        {
            var result = await _context.chemistDataViewModels.FromSql($"getChemistquery").AsNoTracking().ToListAsync();
            return result;
        }
        
        public async Task<bool> setChemist(ChemistListViewModel chemist, int id)
        {
            try
            {
                var result = await _context.saveScheduleViewModels.FromSql($"setChemistDataByAdmin {id},{chemist.chemistID},{chemist.chemistNo},{chemist.chemistname},{chemist.partyTypeId},{chemist.address},{chemist.latitude},{chemist.longitude},{chemist.mobileno},{chemist.telephoneno},{chemist.marketName},{chemist.propritor},{chemist.ownerName},{chemist.creditlimit},{chemist.credit_days},{chemist.druglicense},{chemist.isActive},{chemist.marketId},{chemist.territoryid},{chemist.areaId},{chemist.regionId},{chemist.depoId},{chemist.zoneId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                _context.Dispose();
                return false;
            }
        }

        public async Task<int> UpdateChemistListWithConversionCode(UpdateChemistListViewModel models, int id)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (var model in models.lstDetailsViewModel)
                {
                     result = await _context.saveUpdateValueViewModels.FromSql($"AccSpUpdateChemistList {id},{model.ChemistID},{model.ConversionCode},{model.ChemistCodeApprovalId}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<bool> DeleteChemist(int id)
        {
            var party = _context.AccParty.Where(x => x.partyId == id).FirstOrDefault();
            party.isDelete = true;
            party.isActive = false;
            _context.AccParty.Attach(party);
            _context.Entry(party).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }

    }
}
