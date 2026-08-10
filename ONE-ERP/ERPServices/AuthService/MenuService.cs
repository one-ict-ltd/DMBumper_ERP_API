using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Data;
using ONEERP.Data.Entity;
using ONEERP.ERPServices.AuthService.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.AuthService
{
    public class MenuService : IMenuService
    {
        private readonly ERPDbContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public MenuService(ERPDbContext context, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        #region Menu Types
        public async Task<bool> SaveMenuTypes(string Id, MenuTypesViewModel menuTypesViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetMenuTypes {Id},{menuTypesViewModel.menuTypeId},{menuTypesViewModel.menuTypeName},{menuTypesViewModel.description},{menuTypesViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetMenuTypesById(int id)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetMenuTypes {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteMenuTypesById(string id, int menuTypeId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteMenuTypes {id},{menuTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Module
        public async Task<bool> SaveModule(string Id, ModuleViewModel moduleViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetModules {Id},{moduleViewModel.moduleId},{moduleViewModel.moduleName},{moduleViewModel.description},{moduleViewModel.imageURL},{moduleViewModel.modulePath},{moduleViewModel.sequence},{moduleViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetModuleById(int id)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetModules {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteModuleById(string id, int moduleId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteModules {id},{moduleId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Menus
        public async Task<bool> SaveMenus(string id, MenuViewModel menuViewModel)
        {

            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetMenus {id},{menuViewModel.menuId},{menuViewModel.menuName},{menuViewModel.menuShortName},{menuViewModel.menuTypeId},{menuViewModel.moduleId},{menuViewModel.menuPath},{menuViewModel.reportName},{menuViewModel.reportPath},{menuViewModel.isParent},{menuViewModel.parentId},{menuViewModel.sequence},{menuViewModel.menuIcon},{menuViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetMenusById(int id, int moduleId, int isParent)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetMenus {id},{moduleId},{isParent}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteMenusById(string id, int menuId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteMenus {id},{menuId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region User Group
        public async Task<bool> SaveUserGroup(string Id, UserGroupViewModel userGroupViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetUserGroup {Id},{userGroupViewModel.userGroupId},{userGroupViewModel.groupName},{userGroupViewModel.shortName},{userGroupViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetUserGroupById(int id)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetUserGroup {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteUserGroupById(string id, int userGroupId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteUserGroup {id},{userGroupId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region User Wise Company
        public async Task<bool> SaveUserWiseCompany(string Id, UserWiseCompanyViewModel userWiseCompanyViewModel)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetUserWiseCompany {Id},{userWiseCompanyViewModel.userCompanyId},{userWiseCompanyViewModel.employeeId},{userWiseCompanyViewModel.companyId},{userWiseCompanyViewModel.isDefault},{userWiseCompanyViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetUserWiseCompanyById(int id)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetUserWiseCompany {id}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteUserWiseCompanyById(string id, int userCompanyId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteUserWiseCompany {id},{userCompanyId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region User Permission Group

        public async Task<int> SaveUserPermissionGroup(string id, List<UserPermissionGroupViewModel> userPermissionGroupViewModels, int companyId, int userGroupId)
        {
            await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteUserPermissionGroup {id},{companyId},{userGroupId},{0}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateValueViewModel();
            foreach (UserPermissionGroupViewModel userPermissionGroupViewModel in userPermissionGroupViewModels)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"CmnSpSetUserPermissionGroup {id},{0},{userGroupId},{userPermissionGroupViewModel.employeeId},{companyId},{userPermissionGroupViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;

        }
        public async Task<int> SaveUserPermissionGroupV2(string id, List<UserPermissionGroupViewModel> models, int companyId, int userGroupId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (var item in models)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"CmnSpSetUserPermissionGroupV2 {id},{(item.userPermissionGroupId == null ? 0 : item.userPermissionGroupId)},{userGroupId},{item.employeeId},{companyId},{item.isActive}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;            
        }

        public async Task<JsonViewModel> GetUserPermissionGroupById(int companyId, int userGroupId, int userPermissionGroupId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetUserPermissionGroup {companyId},{userGroupId},{userPermissionGroupId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteUserPermissionGroupById(string id, int companyId, int userGroupId, int userPermissionGroupId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteUserPermissionGroup {id},{companyId},{userGroupId},{userPermissionGroupId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Module Permission        

        public async Task<int> SaveModulePermission(string id, List<ModulePermissionsViewModel> modulePermissionListViewModels, int companyId)
        {
            await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteModulePermissions {id},{companyId},{0}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateValueViewModel();
            foreach (ModulePermissionsViewModel modulePermissionListViewModel in modulePermissionListViewModels)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"CmnSpSetModulePermissions {id},{0},{companyId},{modulePermissionListViewModel.moduleId},{modulePermissionListViewModel.defaultMenuId},{modulePermissionListViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetModulePermissionById(int companyId, int modulePermissionId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetModulePermissions {companyId},{modulePermissionId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteModulePermissionById(string id, int companyId, int modulePermissionId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteModulePermissions {id},{companyId},{modulePermissionId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Menu Permission
        public async Task<int> SaveMenuPermission(string id, List<MenuPermissionViewModel> menuPermissionViewModels, int companyId, int moduleId, int userGroupId, int employeeId, DateTime effectiveDate)
        {
            await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteMenuPermission {id},{companyId},{moduleId},{userGroupId},{employeeId},{0}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateValueViewModel();
            foreach (MenuPermissionViewModel menuPermissionViewModel in menuPermissionViewModels)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"CmnSpSetMenuPermission {id},{0},{menuPermissionViewModel.menuId},{moduleId},{userGroupId},{employeeId},{companyId},{effectiveDate},{menuPermissionViewModel.enableView},{menuPermissionViewModel.enableInsert},{menuPermissionViewModel.enableUpdate},{menuPermissionViewModel.enableDelete},{menuPermissionViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetMenuPermissionById(int companyId, int moduleId, int userGroupId, int employeeId, int menuPermissionId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetMenuPermission {companyId},{moduleId},{userGroupId},{employeeId},{menuPermissionId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteMenuPermissionById(string id, int companyId, int moduleId, int userGroupId, int employeeId, int menuPermissionId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteMenuPermission {id},{companyId},{moduleId},{userGroupId},{employeeId},{menuPermissionId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Report Type

        public async Task<bool> SaveReportType(string id, ReportTypeViewModel model)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"saaaa {id},{model.reportTypeId},{model.reportTypeName},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetReportTypeById(int reportTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetReportType {reportTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteReportTypeById(string id, int reportTypeId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"deeee {id},{reportTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Report  

        public async Task<bool> SaveReport(string id, ReportViewModel model)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpSetReport {id},{model.reportId},{model.reportTypeId},{model.moduleId},{model.reportName},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetReportById(int moduleId, int reportId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetReport {moduleId},{reportId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteReportById(string id, int reportId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteReport {id},{reportId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        #endregion

        #region Report Permission

        public async Task<int> SaveReportPermission(string id, List<ReportPermissionViewModel> reportPermissionViewModels, int employeeId, int reportPermissionId)
        {
            await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteReportPermissions {id},{employeeId},{reportPermissionId}").AsNoTracking().FirstOrDefaultAsync();
            var result = new SaveUpdateValueViewModel();
            foreach (ReportPermissionViewModel model in reportPermissionViewModels)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"CmnSpSetReportPermissions {id},{0},{employeeId},{model.reportId},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetReportPermissionById(int employeeId, int reportPermissionId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetReportPermission {employeeId},{reportPermissionId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> DeleteReportPermissionById(string id, int employeeId, int reportPermissionId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"CmnSpDeleteReportPermissions {id},{employeeId},{reportPermissionId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetReportByUserPermission(string employeeId, string reportType)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetReportByUserPermission {employeeId},{reportType}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region For Report Report Type(Details/Summary)
        public async Task<JsonViewModel> GetDdlRptReportType()
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetDdlRptReportType").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion 

        public async Task<JsonViewModel> getServerDateTime(int? userId)
        {
            var result = await _context.jsonViewModels.FromSql($"CmnSpGetServerDateTime {userId}").AsNoTracking().FirstOrDefaultAsync();      
            return result;
        }

    }
}
