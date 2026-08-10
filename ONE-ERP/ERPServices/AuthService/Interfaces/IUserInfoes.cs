using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.MasterData.Models;
using ONEERP.Data.Entity;
using ONEERP.Data.Entity.HRM;
using ONEERP.Models;
using ONEERP.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPService.AuthService.Interfaces
{
    public interface IUserInfoes
    {

        #region Users

        Task<JsonViewModel> GetUsersById(string userId);
        Task<JsonViewModel> GetUsersByUserName(string userName);
        Task<bool> DeleteUsersById(string id, string userId);
        Task<JsonViewModel> GetUserProfileJson(string userName);
        Task<JsonViewModel> GetUserProfileJsonNew(string userName);
        Task<int> GetMaxUserId();
        Task<AspNetUsersProfileViewModel> AspNetUsersProfileViewModel(string userName);
        Task<bool> userlogininfo(string UserName, string Latitude, string Longitude, string Address, int Islogin, string token, string deviceNo);
        Task<bool> userconnectioninfo(string UserName, DateTime Date, string Time, int Islocation, int IsDataConnected);
        Task<IEnumerable<MIOListViewModel>> MIOListViewModels();
        Task<IEnumerable<LoginInfoDataViewModel>> GetNotLoginInfoDataViews();
        Task<IEnumerable<LoginInfoDataViewModel>> GetLoginInfoDataViews();
        Task<IEnumerable<LoginInfoDataViewModel>> GetNotLocationInfoDataViews();
        #endregion

        #region Old
        Task<AspNetUsersViewModel> GetUserInfoByUser(string userName);
        Task<ApplicationUser> GetUserBasicInfoes(string userName);
        Task<ApplicationUser> GetUserBasicInfoesbyId(string Id);
        Task<HrmEmployee> GetEmployeeById(int Id);
        Task<IEnumerable<AspNetUsersViewModel>> GetUserInfoList();
        Task<AspNetUsersViewModel> GetSbuIdByEmployeeEmail(string emailId);
        Task<UserProfileViewModel> GetUserprofileInfoByUser(string userName);

        Task<IEnumerable<string>> GetRoleListByUserId(string Id);
        Task<bool> DeleteUserRoleListByUserId(string Id);
        Task<bool> DeleteRoleById(string Id);
        Task<IEnumerable<RegisterViewModel>> GetEmployeeForRegister();
        Task<bool> userlogininfo(string userName, int Islogin, string token);

        Task<IEnumerable<MIOCurrentLocationViewModel>> MIOCurrentLocationDViewModelsN();
        #endregion

        #region Field force tracking-----
        Task<IEnumerable<AspNetUsersViewModel>> GetAllUserInfo();
        Task<IEnumerable<ZoneListViewModel>> ZoneListViewModels(int ZoneId);
        Task<IEnumerable<ZoneListViewModel>> ZoneListViewModels();
        Task<IEnumerable<DepoListViewModel>> DepoListViewModels();
        Task<JsonViewModel> GetDepoById(int DepotID);
        Task<JsonViewModel> GetDepoByZoneCode(string code);
        Task<JsonViewModel> GetRegionbydepocode(string code);
        Task<JsonViewModel> GetRegionByZoneOrDepoCode(string zoneCode, string depoCode);
        Task<JsonViewModel> GetTerritorybyAreacode(string code);
        Task<JsonViewModel> GetMarketbyTerritorycode(string code, int? employeeId);
        Task<JsonViewModel> GetAreabyRegioncode(string code);
        Task<JsonViewModel> GetDepoByZoneCodeByUser(int employeeId, string code);
        Task<JsonViewModel> GetRegionbydepocodeByUser(int employeeId, string code);
        Task<JsonViewModel> GetRegionById(int RegionID);
        Task<JsonViewModel> GetMarketById(int MarketId);
        Task<IEnumerable<RegionListViewModel>> RegionListViewModels();
        Task<IEnumerable<AreaListViewModel>> AreaListViewModels();
        Task<JsonViewModel> getAreaListViewModels(int AreaID);
        Task<JsonViewModel> getTerritorybyUser(string code);
        Task<JsonViewModel> getPendingPickingAreaByUser(int? empId, string areaCode);
        Task<JsonViewModel> getTerritoryForPickingByUser(int? empId, string areaCode);
        Task<IEnumerable<TeritoryListViewModel>> TeritoryListViewModels();
        Task<JsonViewModel> GetTerritoryById(int RegionID);
        Task<IEnumerable<MarketListViewModel>> MarketListViewModels();
        //   Task<IEnumerable<MIOListViewModel>> MIOListViewModels();
        Task<JsonViewModel> GetTerritoryByIdByUser(int RegionID, int employeeId);
        Task<bool> setZone(ZoneListViewModel model, int id);
        Task<bool> DeleteZoneById(string id, int Id);
        Task<bool> DeleteAreaById(string id, int Id);
        Task<bool> DeleteMarketById(string id, int Id);
        Task<bool> DeleteDepoById(string id, int Id);
        Task<bool> DeleteTerritoryById(string id, int Id);
        Task<bool> DeleteRegionById(string id, int Id);
        Task<JsonViewModel> getAreaListViewModelsByUser(int AreaID, int employeeId);
        Task<bool> setDepo(DepoListViewModel model, int id);
        Task<bool> setRegion(RegionListViewModel model, int id);
        Task<bool> setArea(AreaListViewModel model, int id);
        Task<bool> setTerritory(TeritoryListViewModel model, int id);
        Task<bool> setMarket(MarketListViewModel model, int id);
        Task<IEnumerable<MIOCurrentLocationViewModel>> MIOCurrentLocationViewModels(string Zone, string Depot, string Region, string Area, string Territory, string EmpCode);
        Task<IEnumerable<MIOCurrentLocationViewModel>> MIOCurrentLocationDViewModels(string EmPCode);
        Task<IEnumerable<MIOCurrentLocationViewModel>> MIOCurrentLocationViewModelsByMIOForApps(string Zone, string Depot, string Region, string Area, string Territory, string EmpCode, string Date);
        Task<IEnumerable<MIOCurrentLocationViewModel>> MIOCurrentLocationViewModelsByMIO(string Zone, string Depot, string Region, string Area, string Territory, string EmpCode, string Date);
        Task<IEnumerable<MIOCurrentLocationNNViewModel>> MIOCurrentLocationViewModelsByMIO2(string Zone, string Depot, string Region, string Area, string Territory, string EmpCode, string Date);
        Task<IEnumerable<MIOCurrentLocationViewModel>> MIOCurrentLocationViewNModelsByMIO(string Zone, string Depot, string Region, string Area, string Territory, string EmpCode, string Date);

        Task<IEnumerable<SummaryDataViewModel>> GetSummaryData(string Id,string Zone, string Depot, string Region, string Area, string Territory, string EmpCode, string Date);
        Task<IEnumerable<MIOCurrentLocationNNViewModel>> MIOCurrentLocationNNViewModels();
        #endregion
        Task<CompanyListViewModel> GetCompanyById(int? Id);
        Task<bool> GetLicenseStaus(string cName);

        Task<bool> CheckPasswordValidity(DateTime? expireDate);
        Task<bool> UpdatePasswordValidity(string userName);

        #region New service for TADRZ relation change

        Task<JsonViewModel> GetRegionByZoneCode(int? userId, string ZoneCode);
        Task<JsonViewModel> GetDepoByRegionCode(int? userId, string RegionCode);
        Task<JsonViewModel> GetAllDepot(int? userId, string code);
        Task<JsonViewModel> GetAreaByRegionCode(int? userId, string RegionCode);
        Task<JsonViewModel> GetAreaByDepoCode(int? userId, string DepoCode);
        Task<IEnumerable<ZoneListViewModel>> ZoneListViewModelsByEmp(int employeeId);

        #endregion

        #region for checkong dummy password
        Task<bool> IsDummyPassword(string password);
        #endregion
    }
}
