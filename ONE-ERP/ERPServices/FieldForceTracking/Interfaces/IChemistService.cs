
using ONEERP.Areas.FieldForceTracking.Models;
using ONEERP.Data.Entity.FieldForceTracking;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ONEERP.ERPServices.FieldForceTracking.Interfaces
{
    public interface IChemistService
    {        
        Task<IEnumerable<CmnChemist>> GetAllCmnChemist();        
        Task<CmnChemist> GetCmnChemistbyId(int Id);
        Task<JsonViewModel> GetChemistList(int Id);       
        Task<IEnumerable<ChemistDataViewModel>> GetChemistDataViewModels();
        Task<IEnumerable<ChemistListAPIViewModel>> GetChemistListAPIViewModel(string Id);
        Task<bool> setChemist(ChemistListViewModel doctor, int id);
        Task<bool> DeleteChemist(int id);
        Task<IEnumerable<ChemistListAPIViewModel>> GetChemistListAPIbyMktViewModel(string Id);
        Task<JsonViewModel> GetChemistListAPIViewModelJson(string Id,string employeeNo);
        Task<JsonViewModel> GetChemistListAPIViewModelJsonWithConversionCode(string Id, string employeeNo);
        Task<IEnumerable<ChemistListAPIViewModel>> GetChemistListAPIViewModelBycode(string Id, string code);
        Task<int> UpdateChemistListWithConversionCode(UpdateChemistListViewModel models, int id);
    }
}
