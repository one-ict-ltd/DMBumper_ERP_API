using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.TaskManagement.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.EmailService.Interfaces;
using ONEERP.ERPServices.TaskManagement.Interfaces;
using ONEERP.Helpers;

namespace ONEERP.Areas.TaskManagement.Controllers
{
    [Route("api/[controller]")]
    public class TaskInfoController : Controller
    {
        object jwts;
        ApplicationUser user;
        private readonly IUserInfoes userInfoes;
        private readonly ITaskInfoService service;
        private readonly IEmailSenderService emailSenderService;
        public TaskInfoController(IUserInfoes _userInfoes, ITaskInfoService _service, IEmailSenderService _emailSenderService)
        {
            service = _service;
            userInfoes = _userInfoes;
            emailSenderService = _emailSenderService;
            jwts = new object();
            user = new ApplicationUser();
        }

        #region TaskInfo

        [HttpPost("SaveTaskInfo")]
        public async Task<IActionResult> SaveTaskInfo([FromBody] List<TaskInfoViewModel> models)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (models.Count == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Task Info has not created.", false);
                return new OkObjectResult(jwt);
            }

            int result = 0;
            foreach (var model in models)
            {
                result = await service.SaveTaskInfo(user.employeeId.ToString(), model);

                if (result != 0)
                {
                    try
                    {
                        string html = "<div><strong>Task Management.</strong></div>"
                            + "Dear Sir,"
                            + " <br/>"
                            + " This is to inform you that a new task was assigned to you at " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt")
                            + "<br/>"
                            + " Task Name is : <strong>" + model.taskName + " </strong>"
                            + "<br/>"
                            + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Software (ERP) Department.</p></div>"
                            + "<strong>One Information And Communications Technology Limited (ONEICT LTD) </strong>"
                            + "<br/><strong>Visit Us: www.one-ict.com  </strong>";

                        var Employee = await userInfoes.GetEmployeeById((int)model.assignToId);
                        if (Employee.emailId != null)
                        {
                            await emailSenderService.SendEmailWithFrom(Employee.emailId, Employee.fullName, "Task Management Notification", html);
                        }
                    }
                    catch (Exception es)
                    {

                    }
                }
            }
            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Task Info has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Task Info has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteTaskInfoById")]
        public async Task<IActionResult> DeleteTaskInfoById([FromBody] int taskInfoId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (taskInfoId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Task Info has not deleted.", false);
                return new OkObjectResult(jwt);
            }

            bool result = await service.DeleteTaskInfoById(user.employeeId.ToString(), taskInfoId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Task Info has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Task Info has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetTaskInfoById")]
        public async Task<IActionResult> GetTaskInfoById(int? taskInfoId, DateTime? fdate, DateTime? tdate)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetTaskInfoById(user.employeeId, taskInfoId, fdate, tdate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetMaxTaskCode")]
        public async Task<IActionResult> GetMaxTaskCode(int? taskInfoId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetMaxTaskCode(taskInfoId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #endregion


        #region Task Team


        [HttpPost("SaveTaskTeam")]
        public async Task<IActionResult> SaveTaskTeam([FromBody] TaskTeamViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Task Team has not created.", false);
                return new OkObjectResult(jwt);
            }

            int result = await service.SaveTaskTeam(user.employeeId.ToString(), model);

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Task Team has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Task Team has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteTaskTeamById")]
        public async Task<IActionResult> DeleteTaskTeamById([FromBody] int taskTeamMasterIdId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (taskTeamMasterIdId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Task Team has not deleted.", false);
                return new OkObjectResult(jwt);
            }

            bool result = await service.DeleteTaskTeamById(user.employeeId.ToString(), taskTeamMasterIdId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Task Team has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Task Team has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetTaskTeamById")]
        public async Task<IActionResult> GetTaskTeamById(int? taskTeamMasterId, int? teamLeaderId, DateTime? fdate, DateTime? tdate)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetTaskTeamById(taskTeamMasterId, teamLeaderId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        #endregion


        #region Common Function

        [HttpGet("GetTaskTypeList")]
        public async Task<IActionResult> GetTaskTypeList(int? taskTypeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetTaskTypeList(taskTypeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetTaskPriorityList")]
        public async Task<IActionResult> GetTaskPriorityList(int? taskPriorityId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetTaskPriorityList(taskPriorityId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("TaskStatusList")]
        public async Task<IActionResult> TaskStatusList(int? taskStatusId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.TaskStatusList(taskStatusId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetTaskTeamMember")]
        public async Task<IActionResult> GetTaskTeamMember(int? teamLeaderId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetTaskTeamMember(user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetParentTaskList")]
        public async Task<IActionResult> GetParentTaskList(int? taskInfoId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetParentTaskList(user.employeeId, taskInfoId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        #endregion

        #region Employee Monthly Task Assign

        [HttpPost("SaveEmployeeMonthlyTaskAssign")]
        public async Task<IActionResult> SaveEmployeeMonthlyTaskAssign([FromBody] EmployeeMonthlyTaskAssignViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model.teamLeadEmployeeId == null || model.teamLeadEmployeeId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Monthly Task Assign has not created successfully.", false);
                return new OkObjectResult(jwt);
            }

            bool result = await service.SaveEmployeeMonthlyTaskAssign(user.employeeId.ToString(), model.lstDetails);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Monthly Task Assign has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Monthly Task Assign has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetEmployeeMonthlyTaskAssignById")]
        public async Task<IActionResult> GetEmployeeMonthlyTaskAssignById(int employeeMonthlyTaskAssignId, int employeeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetEmployeeMonthlyTaskAssignById(employeeMonthlyTaskAssignId, employeeId);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetEmployeeMonthlyTaskAssignByYearMonth")]
        public async Task<IActionResult> GetEmployeeMonthlyTaskAssignByYearMonth(int employeeMonthlyTaskAssignId, int employeeId, int year, string month)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetEmployeeMonthlyTaskAssignByYearMonth(employeeMonthlyTaskAssignId, employeeId, year, month);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetEmployeeMonthlyTaskAssignByYearMonthTeamMemberEmployeeId")]
        public async Task<IActionResult> GetEmployeeMonthlyTaskAssignByYearMonthTeamMemberEmployeeId(int employeeMonthlyTaskAssignId, int employeeId, int teamMemberEmployeeId, int year, string month)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetEmployeeMonthlyTaskAssignByYearMonthTeamMemberEmployeeId(employeeMonthlyTaskAssignId, employeeId, teamMemberEmployeeId, year, month);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpPost("DeleteEmployeeMonthlyTaskAssignById")]
        public async Task<IActionResult> DeleteEmployeeMonthlyTaskAssignById([FromBody] int employeeMonthlyTaskAssignId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (employeeMonthlyTaskAssignId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Monthly Task Assign has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await service.DeleteEmployeeMonthlyTaskAssignById(user.employeeId.ToString(), employeeMonthlyTaskAssignId);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Monthly Task Assign has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Monthly Task Assign has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }
        [HttpGet("GetEmployeeTeamByTeamLeadEmployeeId")]
        public async Task<IActionResult> GetEmployeeTeamByTeamLeadEmployeeId(int employeeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetEmployeeTeamByTeamLeadEmployeeId(employeeId);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetCoreFunctionByDepartmentId")]
        public async Task<IActionResult> GetCoreFunctionByDepartmentId(int departmentId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetCoreFunctionByDepartmentId(departmentId);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #endregion

        #region Employee Weekly Task Assign

        [HttpPost("SaveEmployeeWeeklyTaskAssign")]
        public async Task<IActionResult> SaveEmployeeWeeklyTaskAssign([FromBody] EmployeeWeeklyTaskAssignViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model.teamLeadEmployeeId == null || model.teamLeadEmployeeId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Weekly Task Assign has not created successfully.", false);
                return new OkObjectResult(jwt);
            }

            bool result = await service.SaveEmployeeWeeklyTaskAssign(user.employeeId.ToString(), model.lstDetails);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Weekly Task Assign has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Weekly Task Assign has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetEmployeeWeeklyTaskAssignById")]
        public async Task<IActionResult> GetEmployeeWeeklyTaskAssignById(int employeeWeeklyTaskAssignId, int employeeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetEmployeeWeeklyTaskAssignById(employeeWeeklyTaskAssignId, employeeId);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetEmployeeWeeklyTaskAssignByYearMonthWeek")]
        public async Task<IActionResult> GetEmployeeWeeklyTaskAssignByYearMonthWeek(int employeeWeeklyTaskAssignId, int employeeId, int year, string month, string week)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetEmployeeWeeklyTaskAssignByYearMonthWeek(employeeWeeklyTaskAssignId, employeeId, year, month, week);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpPost("DeleteEmployeeWeeklyTaskAssignById")]
        public async Task<IActionResult> DeleteEmployeeWeeklyTaskAssignById([FromBody] int employeeWeeklyTaskAssignId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (employeeWeeklyTaskAssignId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Weekly Task Assign has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await service.DeleteEmployeeWeeklyTaskAssignById(user.employeeId.ToString(), employeeWeeklyTaskAssignId);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Weekly Task Assign has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Weekly Task Assign has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        #endregion

        #region Employee Weekly My Task Assign

        [HttpPost("SaveEmployeeWeeklyMyTaskAssign")]
        public async Task<IActionResult> SaveEmployeeWeeklyMyTaskAssign([FromBody] EmployeeWeeklyTaskAssignViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            bool result = await service.SaveEmployeeWeeklyMyTaskAssign(user.employeeId.ToString(), model.lstDetails);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Weekly My Task Assign has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Weekly My Task Assign has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }
        #endregion


        async Task<bool> Authentication()
        {
            #region common
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return false;
            }

            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            user = await userInfoes.GetUserBasicInfoesbyId(jti);

            if (user.token != uid && user != null)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                jwts = Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return false;
            }
            return true;
            #endregion
        }
    }
}