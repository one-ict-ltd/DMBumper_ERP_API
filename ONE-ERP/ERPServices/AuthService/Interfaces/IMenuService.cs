using ONEERP.Areas.Auth.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.AuthService.Interfaces
{
    public interface IMenuService
    {

        #region Menu Types
        Task<bool> SaveMenuTypes(string id, MenuTypesViewModel menuTypesViewModel);
        Task<JsonViewModel> GetMenuTypesById(int id);
        Task<bool> DeleteMenuTypesById(string id, int menuTypeId);

        #endregion

        #region Module
        Task<bool> SaveModule(string Id, ModuleViewModel moduleViewModel);
        Task<JsonViewModel> GetModuleById(int id);
        Task<bool> DeleteModuleById(string Id, int moduleId);

        #endregion

        #region Menus

        Task<bool> SaveMenus(string id, MenuViewModel menuViewModel);
        Task<JsonViewModel> GetMenusById(int id, int moduleId, int isParent);
        Task<bool> DeleteMenusById(string id, int menuId);

        #endregion

        #region User Group
        Task<bool> SaveUserGroup(string Id, UserGroupViewModel userGroupViewModel);
        Task<JsonViewModel> GetUserGroupById(int id);
        Task<bool> DeleteUserGroupById(string Id, int userGroupId);

        #endregion

        #region User Wise Company
        Task<bool> SaveUserWiseCompany(string Id, UserWiseCompanyViewModel userWiseCompanyViewModel);
        Task<JsonViewModel> GetUserWiseCompanyById(int id);
        Task<bool> DeleteUserWiseCompanyById(string Id, int userCompanyId);

        #endregion

        #region User Permission Group

        Task<int> SaveUserPermissionGroup(string id, List<UserPermissionGroupViewModel> userPermissionGroupViewModel, int companyId, int userGroupId);
        Task<int> SaveUserPermissionGroupV2(string id, List<UserPermissionGroupViewModel> userPermissionGroupViewModel, int companyId, int userGroupId);
        Task<JsonViewModel> GetUserPermissionGroupById(int companyId, int userGroupId, int userPermissionGroupId);
        Task<bool> DeleteUserPermissionGroupById(string id, int companyId, int userGroupId, int userPermissionGroupId);

        #endregion

        #region Module Permission

        Task<int> SaveModulePermission(string id, List<ModulePermissionsViewModel> modulePermissionListViewModels, int companyId);
        Task<JsonViewModel> GetModulePermissionById(int companyId, int modulePermissionId);
        Task<bool> DeleteModulePermissionById(string id, int companyId, int modulePermissionId);

        #endregion

        #region Menu Permission

        Task<int> SaveMenuPermission(string id, List<MenuPermissionViewModel> menuPermissionViewModels, int companyId, int moduleId, int userGroupId, int employeeId, DateTime effectiveDate);
        Task<JsonViewModel> GetMenuPermissionById(int companyId, int moduleId, int userGroupId, int employeeId, int menuPermissionId);
        Task<bool> DeleteMenuPermissionById(string id, int companyId, int moduleId, int userGroupId, int employeeId, int menuPermissionId);

        #endregion

        #region Report Type

        Task<bool> SaveReportType(string id, ReportTypeViewModel model);
        Task<JsonViewModel> GetReportTypeById(int reportTypeId);
        Task<bool> DeleteReportTypeById(string id, int reportTypeId);

        #endregion

        #region Report

        Task<bool> SaveReport(string id, ReportViewModel model);
        Task<JsonViewModel> GetReportById(int moduleId, int reportId);
        Task<bool> DeleteReportById(string id, int reportId);

        #endregion

        #region Report Permission

        Task<int> SaveReportPermission(string id, List<ReportPermissionViewModel> reportPermissionViewModels, int employeeId, int reportPermissionId);
        Task<JsonViewModel> GetReportPermissionById(int employeeId, int reportPermissionId);       
        Task<bool> DeleteReportPermissionById(string id, int employeeId, int reportPermissionId);
        Task<JsonViewModel> GetReportByUserPermission(string employeeId, string reportType);

        #endregion

        #region For Report Report Type(Details/Summary)
        Task<JsonViewModel> GetDdlRptReportType();

        #endregion
        Task<JsonViewModel> getServerDateTime(int? userId);
    }
}
