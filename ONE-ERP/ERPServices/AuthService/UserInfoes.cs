using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.MasterData.Models;
using ONEERP.Data;
using ONEERP.Data.Entity;
using ONEERP.Data.Entity.HRM;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.Models;
using ONEERP.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.AuthService
{
    public class UserInfoes : IUserInfoes
    {
        private readonly ERPDbContext _context;
        public UserInfoes(ERPDbContext context)
        {
            _context = context;
        }

        #region Users
        public async Task<IEnumerable<AspNetUsersViewModel>> GetAllUserInfo()
        {
            try
            {
                var result = (from U in _context.Users

                              select new AspNetUsersViewModel
                              {
                                  aspnetId = U.Id,
                                  companyId = (U.companyId == null) ? 0 : U.companyId,
                                  UserName = U.UserName,
                                  UserTypeId = (U.userTypeId == null) ? 0 : U.userTypeId,
                                  Email = U.Email,
                                  //  EmpCode = U.EmpCode,
                                  //FinancialValue = U.MaxAmount,
                                  //UserId = (U.userId == null) ? 0 : U.userId,
                                  isActive = (U.isActive == null) ? 0 : U.isActive,
                                  EmpName = "",
                                  EmployeeId = 0,
                                  DivisionName = "",
                                  projectId = 1,
                                  DesignationName = "",
                                  projId = 1,
                                  projectName = "",
                                  //imageUrl= _context.photographs.Where(x => x.employeeId==emp.Id).Select(x=>x.url).FirstOrDefaultAsync(),
                                  specialBranchUnitId = 0,
                                  companyName = ""
                              }).ToListAsync();
                var data = await result;
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<JsonViewModel> GetUsersById(string userId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetAspNetUsers {userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetUsersByUserName(string userName)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetAspNetUsersByUserName {userName}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }


       
        public async Task<bool> DeleteUsersById(string id, string userId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteAspNetUsers {id},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetUserProfileJson(string userName)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"CmnSpGetLogProfileJson {userName}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public async Task<JsonViewModel> GetUserProfileJsonNew(string userName)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetUserWiseMenuJson {userName}").AsNoTracking().FirstOrDefaultAsync();
            //var result = await _context.jsonViewModels.FromSql($"CmnSpGetLogProfileJsonNew {userName}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<AspNetUsersProfileViewModel> AspNetUsersProfileViewModel(string userName)
        {
            var data = await _context.aspNetUsersProfileViewModels.FromSql($"sp_GetAspnetuserProfileByuser {userName}").AsNoTracking().FirstOrDefaultAsync();
            return data;
        }

        public async Task<bool> userlogininfo(string UserName, string Latitude, string Longitude, string Address, int Islogin, string tocken, string deviceNo)
        {
            try
            {
                var result = await _context.saveScheduleViewModels.FromSql($"insertUserLoginInfo {UserName},{Latitude},{Longitude},{Address},{Islogin},{tocken},{deviceNo}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                _context.Dispose();
                return false;
            }
        }

        public async Task<bool> userconnectioninfo(string UserName, DateTime Date, string Time, int Islocation, int IsDataConnected)
        {
            try
            {
                var result = await _context.saveScheduleViewModels.FromSql($"insertdatauserconnectioninfo {UserName},{Date},{Time},{Islocation},{IsDataConnected}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                _context.Dispose();
                return false;
            }
        }

        public async Task<IEnumerable<MIOListViewModel>> MIOListViewModels()
        {
            return await _context.mIOListViewModels.FromSql($"FftSpGetMIOList").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<LoginInfoDataViewModel>> GetNotLoginInfoDataViews()
        {
            try
            {
                return await _context.loginInfoDataViewModels.FromSql($"getnotlogindata").AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<LoginInfoDataViewModel>> GetLoginInfoDataViews()
        {
            try
            {
                return await _context.loginInfoDataViewModels.FromSql($"getlogindata").AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<LoginInfoDataViewModel>> GetNotLocationInfoDataViews()
        {
            try
            {
                return await _context.loginInfoDataViewModels.FromSql($"getnotocationdata").AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region Old
        public async Task<AspNetUsersViewModel> GetUserInfoByUser(string userName)
        {
            try
            {
                // var result = new AspNetUsersViewModel();
                var result = (from U in _context.Users
                                  // join E in _context.employeeInfos on U.Id equals E.ApplicationUserId into EE
                                  // from emp in EE.DefaultIfEmpty()
                                  //join pl in _context.ProjectConstructionLocations.Include(x => x.project) on U.Id equals pl.ApplicationUserId into pp
                                  // from PCL in pp.DefaultIfEmpty()
                                  //  join D in _context.departments on emp.departmentId equals D.Id into DD
                                  // from dpt in DD.DefaultIfEmpty()

                                  //where U.UserName == userName || U.PhoneNumber == userName || U.Email == userName

                                  // comments this code on 2022-Mar-28
                              join emp in _context.HrmEmployee on U.employeeId equals emp.employeeId

                              where U.UserName == userName //|| U.PhoneNumber == userName || U.Email == userName
                              && emp.isActive == true

                              select new AspNetUsersViewModel
                              {
                                  aspnetId = U.Id,
                                  companyId = (U.companyId == null) ? 0 : U.companyId,
                                  UserName = U.UserName,
                                  UserTypeId = (U.userTypeId == null) ? 0 : U.userTypeId,
                                  Email = U.Email,
                                  EmpCode = "",
                                  FinancialValue = 0,
                                  UserId = 0,
                                  isActive = (U.isActive == null) ? 0 : U.isActive,
                                  EmpName = "",
                                  //EmployeeId = 0,
                                  EmployeeId = U.employeeId,
                                  DivisionName = "",
                                  projectId = 1,
                                  DesignationName = "",
                                  projId = 1,
                                  projectName = "",
                                  //imageUrl= _context.photographs.Where(x => x.employeeId==emp.Id).Select(x=>x.url).FirstOrDefaultAsync(),
                                  specialBranchUnitId = 0,
                                  PassExpiredAt = U.PassExpiredAt
                              }).FirstOrDefaultAsync();
                var data = await result;
                return data;
            }
            catch (Exception ex)
            {

                throw;
            }
            

        }
        public async Task<HrmEmployee> GetEmployeeById(int Id)
        {
            return await _context.HrmEmployee.FindAsync(Id);
        }

        public async Task<IEnumerable<string>> GetRoleListByUserId(string Id)
        {
            return await _context.UserRoles.Where(x => x.UserId == Id).Select(x => x.RoleId).ToListAsync();
        }

        public async Task<bool> DeleteUserRoleListByUserId(string Id)
        {
            _context.UserRoles.RemoveRange(_context.UserRoles.Where(x => x.UserId == Id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteRoleById(string Id)
        {
            _context.Roles.Remove(_context.Roles.Where(x => x.Id == Id).First());
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<ApplicationUser> GetUserBasicInfoes(string userName)
        {
            return await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
        }
        public async Task<ApplicationUser> GetUserBasicInfoesbyId(string Id)
        {
            try
            {
                return await _context.Users.Where(x => x.Id == Id).FirstOrDefaultAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<UserProfileViewModel> GetUserprofileInfoByUser(string userName)
        {
            var result = (from U in _context.Users
                          where U.UserName == userName || U.PhoneNumber == userName || U.Email == userName
                          select new UserProfileViewModel
                          {
                              empName = U.UserName,
                              empMobile = U.PhoneNumber,
                              empAddress = "",
                              empAreaCode = "",
                              empTerritorycodeCode = "",
                              empArea = "",
                              empImage = U.imagePath,
                              empDepartment = "",
                              empDesignation = "",
                          }).FirstOrDefaultAsync();
            var data = await result;
            return data;
        }

        public async Task<AspNetUsersViewModel> GetSbuIdByEmployeeEmail(string emailId)
        {
            var result = new AspNetUsersViewModel();
            result.specialBranchUnitId = 1;
            return result;
        }

        public async Task<IEnumerable<AspNetUsersViewModel>> GetUserInfoList()
        {
            List<AspNetUsersViewModel> result = new List<AspNetUsersViewModel>();

            var aspnetolelist = _context.UserRoles.ToList();
            var aspnetrolenamelist = _context.Roles.ToList();
            List<AspNetUsersViewModel> aspNetUsersViewModels = new List<AspNetUsersViewModel>();
            foreach (AspNetUsersViewModel data in result)
            {
                var roleId = aspnetolelist.Where(x => x.UserId == data.aspnetId).ToList();
                List<string> role = new List<string>();
                foreach (var UserRole in roleId)
                {
                    string rnam = aspnetrolenamelist.Where(x => x.Id == UserRole.RoleId).Select(x => x.Name).First();
                    role.Add(rnam);
                }
                aspNetUsersViewModels.Add(new AspNetUsersViewModel
                {
                    aspnetId = data.aspnetId,
                    UserName = data.UserName,
                    UserTypeId = data.UserTypeId,
                    Email = data.Email,
                    EmpCode = data.EmpCode,
                    FinancialValue = data.FinancialValue,
                    UserId = data.UserId,
                    isActive = data.isActive,
                    departmentName = data.departmentName,
                    empType = data.empType,
                    joiningDate = data.joiningDate,
                    mobileNo = data.mobileNo,
                    email = data.email,
                    status = data.status,
                    photoId = data.photoId,
                    EmpName = data.EmpName,
                    EmployeeId = data.EmployeeId,
                    DesignationName = data.DesignationName,
                    roleId = string.Join(",", role),
                    DivisionName = ""
                });
            }
            return aspNetUsersViewModels;
        }

        public async Task<int> GetMaxUserId()
        {
            var result = 0; //await _context.Users.MaxAsync(x => x.userId);
            return result;
        }

        public async Task<bool> userlogininfo(string userName, int Islogin, string token)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"CmnSPSetUserLoginInfo {userName},{Islogin},{token}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                _context.Dispose();
                return false;
            }
        }

        public async Task<IEnumerable<RegisterViewModel>> GetEmployeeForRegister()
        {
            try
            {
                // return await _context.RegisterViewModels.FromSql($"GetEmployeeForRegister").AsNoTracking().ToListAsync();
                return new List<RegisterViewModel>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region field force tracking------

        #region New service for TADRZ relation change
        public async Task<JsonViewModel> GetRegionByZoneCode(int? userId, string ZoneCode)
        {
            return await _context.jsonViewModels.FromSql($"getRegionByZoneCode {userId},{ZoneCode}").AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<JsonViewModel> GetDepoByRegionCode(int? userId, string RegionCode)
        {
            return await _context.jsonViewModels.FromSql($"getDepoByRegionCode {userId},{RegionCode}").AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<JsonViewModel> GetAllDepot(int? userId, string code)
        {
            return await _context.jsonViewModels.FromSql($"getAllDepot {userId},{code}").AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<JsonViewModel> GetAreaByRegionCode(int? userId, string RegionCode)
        {
            return await _context.jsonViewModels.FromSql($"getAreaByRegionCode2 {userId},{RegionCode}").AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<JsonViewModel> GetAreaByDepoCode(int? userId, string DepoCode)
        {
            return await _context.jsonViewModels.FromSql($"getAreaByDepoCode {userId},{DepoCode}").AsNoTracking().FirstOrDefaultAsync();
        }

        #endregion


        public async Task<IEnumerable<ZoneListViewModel>> ZoneListViewModels(int ZoneId)
        {
            try
            {
                return await _context.zoneListViewModels.FromSql($"getZone {ZoneId}").AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<ZoneListViewModel>> ZoneListViewModels()
        {
            try
            {
                return await _context.zoneListViewModels.FromSql($"getZone").AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<ZoneListViewModel>> ZoneListViewModelsByEmp(int employeeId)
        {
            try
            {
                return await _context.zoneListViewModels.FromSql($"getZone {0},{employeeId}").AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public async Task<IEnumerable<DepoListViewModel>> DepoListViewModels()
        {
            return await _context.depoListViewModels.FromSql($"CmnSpGetDepoList").AsNoTracking().ToListAsync();
        }

        public async Task<JsonViewModel> GetDepoById(int DepotID)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getDepo {DepotID}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<JsonViewModel> GetDepoByZoneCode(string code)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getDepobyZoneCode {code}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<JsonViewModel> GetDepoByZoneCodeByUser(int employeeId,string code)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getDepobyZoneCode {code},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<JsonViewModel> GetRegionbydepocode(string code)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getRegionbydepocode {code}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetRegionbydepocodeByUser(int employeeId,string code)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getRegionbydepocode {code},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetRegionByZoneOrDepoCode(string zoneCode, string depoCode)
        {
            var result = await _context.jsonViewModels.FromSql($"SpGetRegionByZoneOrDepoCode {zoneCode}, {depoCode}").AsNoTracking().FirstOrDefaultAsync();
            return result;

        }


        public async Task<JsonViewModel> GetAreabyRegioncode(string code)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getAreabyRegioncode {code}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetTerritorybyAreacode(string code)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getTerritorybyAreacode {code}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> getTerritorybyUser(string code)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getTerritorybyUser {code}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<JsonViewModel> getPendingPickingAreaByUser(int? empId, string areaCode)
        {
            try
            {
                //var sql = $"getPendingPickingAreaByUser {empId},{areaCode}";
                var result = await _context.jsonViewModels.FromSql($"getPendingPickingAreaByUser {empId},{areaCode}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<JsonViewModel> getTerritoryForPickingByUser(int? empId, string areaCode)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getTerritoryForPickingByUser {empId},{areaCode}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JsonViewModel> GetMarketbyTerritorycode(string code, int? employeeId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getMarketbyterritorycode {code},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<IEnumerable<RegionListViewModel>> RegionListViewModels()
        {
            return await _context.regionListViewModels.FromSql($"CmnSpGetRegionList").AsNoTracking().ToListAsync();
        }

        public async Task<JsonViewModel> GetRegionById(int RegionID)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getRegion {RegionID}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<JsonViewModel> GetMarketById(int MarketId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getMarket {MarketId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<IEnumerable<AreaListViewModel>> AreaListViewModels()
        {
            return await _context.areaListViewModels.FromSql($"CmnSpGetAreaList").AsNoTracking().ToListAsync();
        }

        public async Task<JsonViewModel> getAreaListViewModels(int AreaID)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getArea {AreaID}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<JsonViewModel> getAreaListViewModelsByUser(int AreaID,int employeeId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getArea {AreaID},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public async Task<IEnumerable<TeritoryListViewModel>> TeritoryListViewModels()
        {
            return await _context.teritoryListViewModels.FromSql($"CmnSpGetTerritoryList").AsNoTracking().ToListAsync();
        }
        public async Task<JsonViewModel> GetTerritoryById(int RegionID)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getTerritory {RegionID}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public async Task<JsonViewModel> GetTerritoryByIdByUser(int RegionID,int employeeId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"getTerritory {RegionID},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public async Task<IEnumerable<MarketListViewModel>> MarketListViewModels()
        {
            return await _context.marketListViewModels.FromSql($"getMarket").AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteZoneById(string id, int Id)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"DeleteZone {Id}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> DeleteAreaById(string id, int Id)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"DeleteCmnArea {Id}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> DeleteMarketById(string id, int Id)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"DeleteCmnMarket {Id}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<bool> DeleteDepoById(string id, int Id)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"DeleteDepo {Id}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> setZone(ZoneListViewModel zone, int id)
        {
            try
            {
                var result = await _context.saveScheduleViewModels.FromSql($"setZoneByAdmin {id},{zone.Id},{zone.Code},{zone.Name},{zone.IsActive}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                _context.Dispose();
                return false;
            }
        }
        public async Task<bool> setDepo(DepoListViewModel model, int id)
        {
            try
            {
                var result = await _context.saveScheduleViewModels.FromSql($"setDepoByAdmin {id},{model.Id},{model.Code},{model.Name},{model.IsActive},{model.ZoneCode}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                _context.Dispose();
                return false;
            }
        }

        public async Task<bool> setRegion(RegionListViewModel model, int id)
        {
            try
            {
                var result = await _context.saveScheduleViewModels.FromSql($"setRegionByAdmin {id},{model.Id},{model.Code},{model.Name},{model.IsActive},{model.DepotCode},{model.ZoneCode},{model.mobileNo}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                _context.Dispose();
                return false;
            }
        }

        public async Task<bool> setArea(AreaListViewModel model, int id)
        {
            try
            {
                var result = await _context.saveScheduleViewModels.FromSql($"setArea {id},{model.Id},{model.Code},{model.Name},{model.IsActive},{model.RegionCode},{model.DepotCode},{model.ZoneCode},{model.mobileNo}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                _context.Dispose();
                return false;
            }
        }
        public async Task<bool> setTerritory(TeritoryListViewModel model, int id)
        {
            try
            {
                var result = await _context.saveScheduleViewModels.FromSql($"setTerritory {id},{model.Id},{model.Code},{model.Name},{model.IsActive},{model.AreaCode},{model.RegionCode},{model.DepotCode},{model.ZoneCode},{model.salesLimit},{model.mobileNo}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                _context.Dispose();
                return false;
            }
        }
        public async Task<bool> DeleteTerritoryById(string id, int Id)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"DeleteTerritory {Id}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> DeleteRegionById(string id, int Id)
        {
            try
            {
                var result = await _context.saveUpdateViewModels.FromSql($"DeleteRegion {Id}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> setMarket(MarketListViewModel model, int id)
        {
            try
            {
                var result = await _context.saveScheduleViewModels.FromSql($"setMarketByAdmin {id},{model.Id},{model.Code},{model.Name},{model.IsActive},{model.TerritoryCode},{model.AreaCode},{model.RegionCode},{model.DepotCode},{model.ZoneCode}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                _context.Dispose();
                return false;
            }
        }

        public async Task<IEnumerable<MIOCurrentLocationViewModel>> MIOCurrentLocationViewModels(string Zone, string Depot, string Region, string Area, string Territory, string EmpCode)
        {
            try
            {
                return await _context.mIOCurrentLocationViewModels.FromSql($"getCurrentLocation {Zone},{Depot},{Region},{Area},{Territory},{EmpCode}").AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<MIOCurrentLocationViewModel>> MIOCurrentLocationDViewModels(string EmPCode)
        {
            try
            {
                return await _context.mIOCurrentLocationViewModels.FromSql($"getCurrentLocationDN {EmPCode}").AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        /// For APPS
        public async Task<IEnumerable<MIOCurrentLocationViewModel>> MIOCurrentLocationViewModelsByMIOForApps(string Zone, string Depot, string Region, string Area, string Territory, string EmpCode, string Date)
        {
            return await _context.mIOCurrentLocationViewModels.FromSql($"FftSpGetCurrentLocationMIOForApps {Zone},{Depot},{Region},{Area},{Territory},{EmpCode},{Date}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MIOCurrentLocationViewModel>> MIOCurrentLocationViewModelsByMIO(string Zone, string Depot, string Region, string Area, string Territory, string EmpCode, string Date)
        {
            return await _context.mIOCurrentLocationViewModels.FromSql($"getCurrentLocationMIO {Zone},{Depot},{Region},{Area},{Territory},{EmpCode},{Date}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<MIOCurrentLocationNNViewModel>> MIOCurrentLocationViewModelsByMIO2(string Zone, string Depot, string Region, string Area, string Territory, string EmpCode, string Date)
        {
            try
            {
                var data = await _context.MIOCurrentLocationViewModels2.FromSql($"getCurrentLocationMIO {Zone},{Depot},{Region},{Area},{Territory},{EmpCode},{Date}").AsNoTracking().ToListAsync();
                return data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<MIOCurrentLocationViewModel>> MIOCurrentLocationViewNModelsByMIO(string Zone, string Depot, string Region, string Area, string Territory, string EmpCode, string Date)
        {//for Golbal API
            try
            {
                var data = await _context.mIOCurrentLocationViewModels.FromSql($"getCurrentLocationMION {Zone},{Depot},{Region},{Area},{Territory},{EmpCode},{Date}").AsNoTracking().ToListAsync();
                return data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
        public async Task<IEnumerable<MIOCurrentLocationViewModel>> MIOCurrentLocationDViewModelsN()
        {
            try
            {
                return await _context.mIOCurrentLocationViewModels.FromSql($"getCurrentLocationDN").AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<IEnumerable<SummaryDataViewModel>> GetSummaryData(string Id,string Zone, string Depot, string Region, string Area, string Territory, string EmpCode, string Date)
        {
            return await _context.summaryDataViewModels.FromSql($"getSumData {Id},{Zone},{Depot},{Region},{Area},{Territory},{EmpCode},{Date}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MIOCurrentLocationNNViewModel>> MIOCurrentLocationNNViewModels()
        {
            try
            {
                return await _context.MIOCurrentLocationNNViewModels.FromSql($"getCurrentLocationDNN").AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region License Check

        public async Task<CompanyListViewModel> GetCompanyById(int? Id)
        {
            var result = await _context.companyListViewModels.FromSql($"CmnSpGetCompany {Id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> GetLicenseStaus(string cName)
        {
            bool status = false;
            try
            {
                string api = $"http://103.106.236.90:10000/api/License/LcStatus?cname={cName.Replace("&", "And")}";
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                client.BaseAddress = new Uri(api);
                var response = client.GetAsync(api, System.Net.Http.HttpCompletionOption.ResponseContentRead).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = response.Content.ReadAsStringAsync().Result;
                    var model = (License)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResult, (typeof(License)));

                    if (model.data.Count > 0)
                        status = model.data[0].LcStatus == 1 ? true : false;
                }
            }
            catch (Exception ex)
            {
                status = true;
                //throw;
            }

            return status;
        }

        #endregion


        public async Task<bool> CheckPasswordValidity(DateTime? expireDate)
        {
            var currentTime = DateTime.Now;
            await Task.Run(() =>
            {
                expireDate = expireDate.HasValue ? expireDate : DateTime.Now;
            });
            return expireDate >= currentTime;
        }

        public async Task<bool> UpdatePasswordValidity(string userName)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnUpdatePasswordExpiryDate {userName}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public Task<bool> IsDummyPassword(string password)
        {
            
            bool isDummy = password.Trim() == "demo@123";
            return Task.FromResult(isDummy);
        }


    }

    class License
    {
        public bool status { get; set; }
        public string message { get; set; }
        public List<Data> data { get; set; }
    }
    class Data
    {
        public int LcStatus { get; set; } = 0;
    }
}
