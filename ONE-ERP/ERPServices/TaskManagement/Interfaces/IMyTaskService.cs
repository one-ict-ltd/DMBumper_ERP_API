using ONEERP.Areas.TaskManagement.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.TaskManagement.Interfaces
{
   public interface IMyTaskService
    {
        Task<JsonViewModel> GetTodaysTaskInfo(int employeeId);
        Task<JsonViewModel> GetTaskByStatus(int employeeId, int statusId, int taskId);
        Task<int> SaveTaskStatusLog(string userId, TaskStatusLogViewModel model);
        Task<JsonViewModel> GetParentTaskInfo(int employeeId);
        Task<JsonViewModel> RptGetTaskByStatus(int employeeId, int taskId, DateTime startDate, DateTime endDate);
    }
}
