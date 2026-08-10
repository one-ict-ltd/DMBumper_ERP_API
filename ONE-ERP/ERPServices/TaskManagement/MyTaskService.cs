using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.TaskManagement.Models;
using ONEERP.Data;
using ONEERP.ERPServices.TaskManagement.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.TaskManagement
{
    public class MyTaskService: IMyTaskService
    {
        private readonly ERPDbContext _context;
        public MyTaskService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<JsonViewModel> GetTodaysTaskInfo(int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"TaskSpGetTodaysTask {employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetTaskByStatus(int employeeId,int statusId, int taskId)
        {
            var result = await _context.jsonViewModels.FromSql($"TaskSpGetTaskByStatus {employeeId},{statusId},{taskId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveTaskStatusLog(string userId, TaskStatusLogViewModel model)
        {
            try
            {
                var result = await _context.saveUpdateValueViewModels.FromSql($"TaskSpSetTaskStatusLog {userId},{model.taskInfoId},{model.remarks},{model.taskStatusId},{model.date},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                throw;
                return 0;
            }

        }

        public async Task<JsonViewModel> GetParentTaskInfo(int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"TaskSpGetParentTaskInfoJson {employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> RptGetTaskByStatus(int employeeId,int taskId,DateTime startDate,DateTime endDate)
        {
            var result = await _context.jsonViewModels.FromSql($"TaskSpRptGetTaskByStatus {employeeId},{taskId},{startDate},{endDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
    }
}
