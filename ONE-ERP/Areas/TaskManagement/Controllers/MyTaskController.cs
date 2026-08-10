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
using ONEERP.ERPServices.TaskManagement.Interfaces;
using ONEERP.Helpers;

namespace ONEERP.Areas.TaskManagement.Controllers
{
    [Route("api/[controller]")]
    public class MyTaskController : Controller
    {
        object jwts;
        ApplicationUser user;
        private readonly IUserInfoes userInfoes;
        private readonly ITaskInfoService service;
        private readonly IMyTaskService myTaskService;
        public MyTaskController(IUserInfoes _userInfoes, ITaskInfoService _service, IMyTaskService myTaskService)
        {
            userInfoes = _userInfoes;
            service = _service;
            this.myTaskService = myTaskService;
            jwts = new object();
            user = new ApplicationUser();
        }

        [HttpGet("GetTodaysTaskInfoByempId")]
        public async Task<IActionResult> GetTodaysTaskInfoByempId()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await myTaskService.GetTodaysTaskInfo((int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetTaskInfoByempIdStatus")]
        public async Task<IActionResult> GetTaskInfoByempIdStatus(int statusId,int taskId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await myTaskService.GetTaskByStatus((int)user.employeeId, statusId, taskId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("SaveTaskStatusLog")]
        public async Task<IActionResult> SaveTaskStatusLog([FromBody] TaskStatusLogViewModel model)
        {
            try
            {
                if (Authentication().Result == false) return new OkObjectResult(jwts);

                if (model == null)
                {
                    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Task status log has not created.", false);
                    return new OkObjectResult(jwt);
                }

                model.date = Convert.ToDateTime((model.date?.ToString("yyyy-MMM-dd")+ " " + model.time));

                int result = await myTaskService.SaveTaskStatusLog(user.employeeId.ToString(), model);

                if (result != 0)
                {
                    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Task status log created successfully.", true);
                    return new OkObjectResult(jwt);
                }
                else
                {
                    var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Task status log has not created.", false);
                    return new OkObjectResult(jwt);
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetParentTaskInfo")]
        public async Task<IActionResult> GetParentTaskInfo()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await myTaskService.GetParentTaskInfo((int)user.employeeId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("RptGetTaskByStatus")]
        public async Task<IActionResult> RptGetTaskByStatus(int employeeId, int taskId,DateTime fromDate, DateTime toDate)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await myTaskService.RptGetTaskByStatus(employeeId, taskId, fromDate, toDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

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
