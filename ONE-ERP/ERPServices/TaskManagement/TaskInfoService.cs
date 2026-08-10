using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.TaskManagement.Models;
using ONEERP.Data;
using ONEERP.ERPServices.TaskManagement.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.TaskManagement
{
    public class TaskInfoService : ITaskInfoService
    {
        private readonly ERPDbContext _context;
        public TaskInfoService(ERPDbContext context)
        {
            _context = context;
        }

        #region Task Info Service

        public async Task<int> SaveTaskInfo(string userId, TaskInfoViewModel model)
        {
            try
            {
                //var s = $"TaskSpSetTaskInfo {userId},{model.taskInfoId},{model.taskName},{model.taskCode},{model.taskTypeId},{model.employeeId},{model.assignToId},{model.taskPriorityId},{model.date},{model.expectedEndDate},{model.isParent},{model.parentTaskId},{model.isActive}";

                var result = await _context.saveUpdateValueViewModels.FromSql($"TaskSpSetTaskInfo {userId},{model.taskInfoId},{model.taskName},{model.taskCode},{model.description},{model.taskTypeId},{model.employeeId},{model.assignToId},{model.taskPriorityId},{model.date},{model.expectedEndDate},{model.isParent},{model.parentTaskId},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();

                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
                return 0;
            }

        }
        public async Task<int> SaveTaskInfo(string userId, List<TaskInfoViewModel> models)
        {
            try
            {
                var result = new SaveUpdateValueViewModel();
                foreach (var model in models)
                {
                    //var s = $"TaskSpSetTaskInfo {userId},{model.taskInfoId},{model.taskName},{model.taskCode},{model.taskTypeId},{model.employeeId},{model.assignToId},{model.taskPriorityId},{model.date},{model.expectedEndDate},{model.isParent},{model.parentTaskId},{model.isActive}";

                    result = await _context.saveUpdateValueViewModels.FromSql($"TaskSpSetTaskInfo {userId},{model.taskInfoId},{model.taskName},{model.taskCode},{model.description},{model.taskTypeId},{model.employeeId},{model.assignToId},{model.taskPriorityId},{model.date},{model.expectedEndDate},{model.isParent},{model.parentTaskId},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
                }
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
                return 0;
            }

        }
        public async Task<bool> DeleteTaskInfoById(string userId, int taskInfoId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"TaskSpDeleteTaskInfo {userId}, {taskInfoId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetTaskInfoById(int? userId, int? taskInfoId, DateTime? fdate, DateTime? tdate)
        {
            var result = await _context.jsonViewModels.FromSql($"TaskSpGetTaskInfoJson {userId},{taskInfoId},{fdate},{tdate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetMaxTaskCode(int? taskInfoId)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"TaskSpGetMaxTaskCodeJson {taskInfoId}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion


        #region Common Services


        public async Task<JsonViewModel> GetTaskTypeList(int? taskTypeId)
        {
            var result = await _context.jsonViewModels.FromSql($"TaskSpGetTaskTypeListJson {taskTypeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetTaskPriorityList(int? taskPriorityId)
        {
            var result = await _context.jsonViewModels.FromSql($"TaskSpGetTaskPriorityListJson {taskPriorityId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> TaskStatusList(int? taskStatusId)
        {
            var result = await _context.jsonViewModels.FromSql($"TaskSpGetTaskStatusListJson {taskStatusId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetTaskTeamMember(int? teamLeaderId)
        {
            var result = await _context.jsonViewModels.FromSql($"TaskSpGetTaskTeamMemberJson {teamLeaderId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetParentTaskList(int? userId, int? taskInfoId)
        {
            var result = await _context.jsonViewModels.FromSql($"TaskSpGetParentTaskListJson {userId},{taskInfoId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region Task Team
        public async Task<int> SaveTaskTeam(string userId, TaskTeamViewModel model)
        {
            try
            {
                var taskTeamMaster = new SaveUpdateValueViewModel();
                taskTeamMaster = await _context.saveUpdateValueViewModels.FromSql($"TaskSpSetTaskTeam {userId},{model.taskTeamMasterId},{model.teamLeaderId},{model.teamName},{model.teamCode},{model.description},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();

                foreach (var item in model.lstTaskTeamDetails)
                {
                    var result = await _context.saveUpdateValueViewModels.FromSql($"TaskSpSetTaskTeamDetails {userId},{item.taskTeamDetailId},{taskTeamMaster.isSuccess},{item.employeeId},{item.isActive}").AsNoTracking().FirstOrDefaultAsync();
                }
                return taskTeamMaster.isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
                return 0;
            }
        }
        public async Task<bool> DeleteTaskTeamById(string userId, int taskTeamMasterId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"TaskSpDeleteTaskTeam {userId}, {taskTeamMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetTaskTeamById(int? taskTeamMasterIdId, int? teamLeaderId)
        {
            var result = await _context.jsonViewModels.FromSql($"TaskSpGetTaskTeamJson {taskTeamMasterIdId},{teamLeaderId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        #endregion

        #region Employee Monthly Task Assign    

        public async Task<bool> SaveEmployeeMonthlyTaskAssign(string userId, List<EmployeeMonthlyTaskAssignViewModel> monthlyTaskAssignList)
        {
            var result = new SaveUpdateViewModel();
            foreach (var item in monthlyTaskAssignList)
            {
                result = await _context.saveUpdateViewModels.FromSql($"HrmSpSetEmployeeMonthlyTaskAssign {userId},{item.employeeMonthlyTaskAssignId},{item.teamLeadEmployeeId},{item.teamMemberEmployeeId},{item.departmentId},{item.designationId},{item.year},{item.month},{item.coreFunctionId},{item.taskQty},{item.description},{item.isActive}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetEmployeeMonthlyTaskAssignById(int employeeMonthlyTaskAssignId, int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetEmployeeMonthlyTaskAssign {employeeMonthlyTaskAssignId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetEmployeeMonthlyTaskAssignByYearMonth(int employeeMonthlyTaskAssignId, int employeeId, int year, string month)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetEmployeeMonthlyTaskAssignByYearMonth {employeeMonthlyTaskAssignId},{employeeId},{year},{month}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetEmployeeMonthlyTaskAssignByYearMonthTeamMemberEmployeeId(int employeeMonthlyTaskAssignId, int employeeId, int teamMemberEmployeeId, int year, string month)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetEmployeeMonthlyTaskAssignByYearMonthTeamMemberEmployeeId {employeeMonthlyTaskAssignId},{employeeId},{teamMemberEmployeeId},{year},{month}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteEmployeeMonthlyTaskAssignById(string userId, int employeeMonthlyTaskAssignId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpDeleteEmployeeMonthlyTaskAssign {userId},{employeeMonthlyTaskAssignId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetEmployeeTeamByTeamLeadEmployeeId( int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetEmployeeTeamByTeamLeadEmployeeId {employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetCoreFunctionByDepartmentId(int departmentId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetCoreFunctionByDepartmentId {departmentId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        #endregion

        #region Employee Weekly Task Assign    

        public async Task<bool> SaveEmployeeWeeklyTaskAssign(string userId, List<EmployeeWeeklyTaskAssignViewModel> weeklyTaskAssignList)
        {
            var result = new SaveUpdateViewModel();
            foreach (var item in weeklyTaskAssignList)
            {
                result = await _context.saveUpdateViewModels.FromSql($"HrmSpSetEmployeeWeeklyTaskAssign {userId},{item.employeeWeeklyTaskAssignId},{item.teamLeadEmployeeId},{item.teamMemberEmployeeId},{item.departmentId},{item.designationId},{item.year},{item.month},{item.week},{item.coreFunctionId},{item.taskQty},{item.description},{item.isActive}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetEmployeeWeeklyTaskAssignById(int employeeWeeklyTaskAssignId, int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetEmployeeWeeklyTaskAssign {employeeWeeklyTaskAssignId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetEmployeeWeeklyTaskAssignByYearMonthWeek(int employeeWeeklyTaskAssignId, int employeeId, int year, string month, string week)
        {
            var result = await _context.jsonViewModels.FromSql($"HrmSpGetEmployeeWeeklyTaskAssignByYearMonthWeek {employeeWeeklyTaskAssignId},{employeeId},{year},{month},{week}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteEmployeeWeeklyTaskAssignById(string userId, int employeeWeeklyTaskAssignId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"HrmSpDeleteEmployeeWeeklyTaskAssign {userId},{employeeWeeklyTaskAssignId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        #endregion

        #region Employee Weekly Task Assign    

        public async Task<bool> SaveEmployeeWeeklyMyTaskAssign(string userId, List<EmployeeWeeklyTaskAssignViewModel> weeklyTaskAssignList)
        {
            var result = new SaveUpdateViewModel();
            foreach (var item in weeklyTaskAssignList)
            {
                result = await _context.saveUpdateViewModels.FromSql($"HrmSpSetEmployeeWeeklyMyTaskAssign {userId},{item.employeeWeeklyTaskAssignId},{item.completedTaskQty},{item.status}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }
       
        #endregion

    }
}
