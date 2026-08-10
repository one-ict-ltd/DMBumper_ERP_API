using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Attendance.Models;
using ONEERP.Areas.Hrm.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.FieldForceTracking.Interfaces;
using ONEERP.ERPServices.Hrm.EmployeesInfo.Interfaces;
using ONEERP.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Attendance.Controllers
{
    [Route("api/[controller]")]
    public class AttendanceController : Controller
    {
        object jwts;
        ApplicationUser user;
        private IUserInfoes userInfoes;
        private IAttendanceService attendanceService;

        public AttendanceController(IUserInfoes userInfoes, IAttendanceService attendanceService)
        {
            this.userInfoes = userInfoes;
            this.attendanceService = attendanceService;
        }

        #region Calender

        [HttpGet("GetFullMonthCalender")]
        public async Task<IActionResult> GetFullMonthCalender(int year, int month)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var calenderViewModels = CalenderDay.GetDates(year, month);
            IEnumerable<CalenderViewModel> data = calenderViewModels;
            var jwt = await Tokens.getCalenderData(data.ToList(), new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("SaveCalender")]
        public async Task<IActionResult> SaveCalender([FromBody] CalenderViewModel model)
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            bool result = await attendanceService.SaveCalender(user.employeeId.ToString(), model.lstModel, model);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Calender created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Calender has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetCalender")]
        public async Task<IActionResult> GetCalender()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await attendanceService.GetCalender();
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetCalenderByMonth")]
        public async Task<IActionResult> GetCalenderByMonth(int year, int month)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await attendanceService.GetCalenderByMonth(year, month);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteCalenderByMonth")]
        public async Task<IActionResult> DeleteCalenderByMonth([FromBody] CalenderViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model.Year <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Calender has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await attendanceService.DeleteCalenderByMonth(user.employeeId.ToString(), model);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Calender has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Calender has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        #endregion

        #region ShiftGroup Master

        [HttpPost("SaveShiftGroupMaster")]
        public async Task<IActionResult> SaveShiftGroupMaster([FromBody] ShiftGroupMasterViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model.shiftName == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Shift group has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
            int result = 0;
            int shiftMasterId = await attendanceService.SaveShiftGroupMaster(user.employeeId.ToString(), model);
            if (shiftMasterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Shift group has not created.", false);
                return new OkObjectResult(jwt);
            }
            if (model.isDetailsUpdated == 1)
            {
                result = await attendanceService.SaveShiftGroupDetail(user.employeeId.ToString(), model.lstDetails, shiftMasterId);
            }
            if (result != 0 || shiftMasterId != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Shift group has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Shift group has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetShiftGroupMasterById")]
        public async Task<IActionResult> GetShiftGroupMasterById(int shiftMasterId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await attendanceService.GetShiftGroupMasterById(shiftMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetDuplicateShiftGroupMaster")]
        public async Task<IActionResult> GetDuplicateShiftGroupMaster(int shiftMasterId, string shiftName)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await attendanceService.GetDuplicateShiftGroupMaster(shiftMasterId, shiftName);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteShiftGroupMasterById")]
        public async Task<IActionResult> DeleteShiftGroupMasterById([FromBody] int shiftMasterId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (shiftMasterId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Shift group has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await attendanceService.DeleteShiftGroupMasterById(user.employeeId.ToString(), shiftMasterId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Shift group has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Shift group has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        #endregion

        #region ShiftGroup Detail

        [HttpGet("GetShiftGroupDetailByMasterId")]
        public async Task<IActionResult> GetShiftGroupDetailByMasterId(int shiftMasterId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await attendanceService.GetShiftGroupDetailByMasterId(shiftMasterId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        #endregion

        #region Assign Shift & Update PunchCard 

        [HttpPost("AssignShiftGroup")]
        public async Task<IActionResult> AssignShiftGroup([FromBody] PunchCardViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model.shiftMasterId == null || model.shiftMasterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Shift group has not assigned successfully.", false);
                return new OkObjectResult(jwt);
            }

            int shiftMasterId = await attendanceService.AssignShiftGroup(user.employeeId.ToString(), model);
            if (shiftMasterId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Shift group has not assigned.", false);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Shift group has not assigned successfully.", true);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetShiftAssignById")]
        public async Task<IActionResult> GetShiftAssignById(int punchCardId, int companyId, int sbuId, int employeeId, string department)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await attendanceService.GetShiftAssignById(punchCardId, companyId, sbuId, employeeId, department);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetPunchCardById")]
        public async Task<IActionResult> GetPunchCardById(int punchCardId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await attendanceService.GetPunchCardById(punchCardId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeletePunchCardById")]
        public async Task<IActionResult> DeletePunchCardById([FromBody] int punchCardId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (punchCardId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Shift assign has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await attendanceService.DeletePunchCardById(user.employeeId.ToString(), punchCardId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Shift assign has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Shift assign has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("UpdatePunchCardNo")]
        public async Task<IActionResult> UpdatePunchCardNo([FromBody] PunchCardViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model.punchCardId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Shift assign has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await attendanceService.UpdatePunchCardNo(user.employeeId.ToString(), model);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Shift assign has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Shift assign has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        #endregion

        #region Attendance Process

        [HttpPost("ProcessAttendance")]
        public async Task<IActionResult> ProcessAttendance([FromBody] AttendanceProcessViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model.companyId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Attendance has not processed.", false);
                return new OkObjectResult(jwt);
            }

            bool result = await attendanceService.ProcessAttendance(user.employeeId.ToString(), model.startDate, model.endDate, model.companyId);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Attendance has processed successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Attendance has not processed.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetAttendanceByDate")]
        public async Task<IActionResult> GetAttendanceByDate(DateTime startDate, DateTime endDate, int companyId, int employeeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await attendanceService.GetAttendanceByDate(startDate, endDate, companyId, employeeId);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetEmployeeAttnClarificationById")]
        public async Task<IActionResult> GetEmployeeAttnClarificationById(int employeecClarificationId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await attendanceService.GetEmployeeAttnClarificationById(employeecClarificationId);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("SaveEmployeeAttnClarification")]
        public async Task<IActionResult> SaveEmployeeAttnClarification([FromBody] HrmEmployeeClarificationViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            
            bool result = await attendanceService.SaveEmployeeAttnClarification(user.employeeId.ToString(), model.empId, model.employeecClarificationId, model.AttendanceDate, model.clarification);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Attendance has processed successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Attendance has not processed.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetDuplicateAttendanceDateForClarification")]
        public async Task<IActionResult> GetDuplicateAttendanceDateForClarification(int employeecClarificationId, DateTime attendanceDate, int empId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await attendanceService.GetDuplicateAttendanceDateForClarification(employeecClarificationId, attendanceDate, empId);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetEmployeeClarificationForApprovalJson")]
        public async Task<IActionResult> GetEmployeeClarificationForApprovalJson()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await attendanceService.HrmSpGetEmployeeClarificationForApprovalJson((int)user.employeeId);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpPost("SetEmployeeClarificationForApproval")]
        public async Task<IActionResult> SetApproveLeave([FromBody] EmployeeClarificationApprovalViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model.lstMasterViewModel.Count() == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Leave Approve Not Found.", false);
                return new OkObjectResult(jwt);
            }
            int result = await attendanceService.SetEmployeeClarificationForApproval(user.employeeId.ToString(), (int)model.Status, model.lstMasterViewModel);

            if (result > 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Leave has Approved successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Leave has not Approved.", false);
                return new OkObjectResult(jwt);
            }
        }
        #endregion


        #region Attendance Report

        [HttpGet("DailyAttendanceReport")]
        public async Task<IActionResult> DailyAttendanceReport(DateTime startDate, int companyId, int sbuId = 0, int departmentId = 0)//, string empId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await attendanceService.DailyAttendanceReport(startDate, companyId, sbuId, departmentId);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetEmpWiseAttendanceReport")]
        public async Task<IActionResult> GetEmpWiseAttendanceReport(int companyId, string empId, DateTime fromDate, DateTime toDate)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await attendanceService.GetEmpWiseAttendanceReport(companyId, empId, fromDate, toDate);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetEmpWiseAttendanceReportForESS")]
        public async Task<IActionResult> GetEmpWiseAttendanceReportForESS()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            DateTime now = DateTime.Now;

            var fromDate = new DateTime(now.Year, now.Month, 1);
            var toDate = fromDate.AddMonths(1).AddDays(-1);

            var datajson = await attendanceService.GetEmpWiseAttendanceReport(0, user.employeeId.ToString(), fromDate, now);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetAttendanceSummaryByDateRange")]
        public async Task<IActionResult> GetAttendanceSummaryByDateRange(int comId, int sbuId, int deptId, int empId, DateTime fDate, DateTime tDate)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await attendanceService.GetAttendanceSummaryByDateRange(comId, sbuId, deptId, empId, fDate, tDate);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }

        #endregion


        #region Attendance Log Collection App

        [HttpGet("GetMaxVerifyDate")]
        public async Task<IActionResult> GetMaxVerifyDate(int machineNo)
        {
            //if (Authentication().Result == false) return new OkObjectResult(jwts);
            var jvm = await attendanceService.GetMaxVerifyDate(machineNo);
            //var jwt = await Tokens.GetJwt(jvm.data);
            return new OkObjectResult(jvm.data);
        }
        [HttpGet("GetAttendanceDeviceList")]
        public async Task<IActionResult> GetAttendanceDeviceList()
        {
            //if (Authentication().Result == false) return new OkObjectResult(jwts);
            var jvm = await attendanceService.GetAttendanceDeviceList();
            //var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jvm.data);
        }
        [HttpPost("SetAttendanceLog")]
        public async Task<IActionResult> SetAttendanceLog([FromBody] List<AttLog> model)
        {
            //if (Authentication().Result == false) return new OkObjectResult(jwts);
            var res = await attendanceService.SetAttendanceLog(model);
            if (res)
            {
                var jwt = ("[{\"status\": \"Succeed !\",\"message\": \"Successfully saved!\"}]");
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = ("[{\"status\": \"Succeed !\",\"message\": \"Data Not Saved!\"}]");
                return new OkObjectResult(jwt);
            }
        }

        #endregion


        #region Authentication Check
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

        #endregion


        #region Manual Attendance 

        [HttpPost("SaveManualAttendance")]
        public async Task<IActionResult> SaveManualAttendance([FromBody] ManualAttendanceViewModel model)
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            bool result = await attendanceService.SaveManualAttendance(user.employeeId.ToString(), model);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Calender created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Calender has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetManualAttendance")]
        public async Task<IActionResult> GetManualAttendance(int manualAttendanceId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await attendanceService.GetManualAttendance(manualAttendanceId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("SaveAttandanceClarification")]
        public async Task<IActionResult> SaveAttandanceClarification([FromBody] AttandaceClarivicationViewModel model)
        {
            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var stream = uid;
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            if (user.token != uid && user != null)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            bool result = await attendanceService.SaveAttandanceClarification(user.employeeId.ToString(), model);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Calender created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Calender has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }

        #endregion

        #region Join Heldup Report

        [HttpGet("HrmJoiningReportJson")]
        public async Task<IActionResult> HrmJoiningReportJson(DateTime joinDate, int locationId, int departmentId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await attendanceService.HrmJoiningReportJson((int)user.employeeId, joinDate, locationId, departmentId);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }

        [HttpGet("HrmHeldupReportJson")]
        public async Task<IActionResult> HrmHeldupReportJson(DateTime joinDate, int locationId, int departmentId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await attendanceService.HrmHeldupReportJson((int)user.employeeId, joinDate, locationId, departmentId);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }

        #endregion


    }
}
