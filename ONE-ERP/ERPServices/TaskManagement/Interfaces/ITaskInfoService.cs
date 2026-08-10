using ONEERP.Areas.TaskManagement.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.TaskManagement.Interfaces
{
    public interface ITaskInfoService
    {
        #region TaskInfoService

        Task<int> SaveTaskInfo(string userId, TaskInfoViewModel model);
        Task<int> SaveTaskInfo(string userId, List<TaskInfoViewModel> models);
        Task<bool> DeleteTaskInfoById(string userId, int taskInfoId);
        Task<JsonViewModel> GetTaskInfoById(int? userId, int? taskInfoId, DateTime? fdate, DateTime? tdate);
        Task<JsonViewModel> GetMaxTaskCode(int? taskInfoId);

        #endregion

        #region Task Common Service
        Task<JsonViewModel> GetTaskTypeList(int? taskTypeId);
        Task<JsonViewModel> GetTaskPriorityList(int? taskPriorityId);
        Task<JsonViewModel> TaskStatusList(int? taskStatusId);
        Task<JsonViewModel> GetTaskTeamMember(int? teamLeaderId);
        Task<JsonViewModel> GetParentTaskList(int? userId, int? taskInfoId);

        #endregion

        #region Task Team
        Task<int> SaveTaskTeam(string userId, TaskTeamViewModel model);
        Task<bool> DeleteTaskTeamById(string userId, int taskTeamId);
        Task<JsonViewModel> GetTaskTeamById(int? taskTeamMasterIdId, int? teamLeaderId);
        #endregion

        #region Employee Monthly Task Assign  
        Task<bool> SaveEmployeeMonthlyTaskAssign(string userId, List<EmployeeMonthlyTaskAssignViewModel> monthlyTaskAssignList);
        Task<JsonViewModel> GetEmployeeMonthlyTaskAssignById(int employeeMonthlyTaskAssignId, int employeeId);
        Task<JsonViewModel> GetEmployeeMonthlyTaskAssignByYearMonth(int employeeMonthlyTaskAssignId, int employeeId, int year, string month);
        Task<JsonViewModel> GetEmployeeMonthlyTaskAssignByYearMonthTeamMemberEmployeeId(int employeeMonthlyTaskAssignId, int employeeId, int teamMemberEmployeeId, int year, string month);
        Task<bool> DeleteEmployeeMonthlyTaskAssignById(string userId, int employeeMonthlyTaskAssignId);
        Task<JsonViewModel> GetEmployeeTeamByTeamLeadEmployeeId(int employeeId);
        Task<JsonViewModel> GetCoreFunctionByDepartmentId(int departmentId);
        #endregion

        #region Employee Weekly Task Assign  
        Task<bool> SaveEmployeeWeeklyTaskAssign(string userId, List<EmployeeWeeklyTaskAssignViewModel> weeklyTaskAssignList);
        Task<JsonViewModel> GetEmployeeWeeklyTaskAssignById(int employeeWeeklyTaskAssignId, int employeeId);
        Task<JsonViewModel> GetEmployeeWeeklyTaskAssignByYearMonthWeek(int employeeWeeklyTaskAssignId, int employeeId, int year, string month, string week);
        Task<bool> DeleteEmployeeWeeklyTaskAssignById(string userId, int employeeWeeklyTaskAssignId);

        #endregion

        #region Employee Weekly My Task Assign  
        Task<bool> SaveEmployeeWeeklyMyTaskAssign(string userId, List<EmployeeWeeklyTaskAssignViewModel> weeklyTaskAssignList);
        #endregion

    }
}
