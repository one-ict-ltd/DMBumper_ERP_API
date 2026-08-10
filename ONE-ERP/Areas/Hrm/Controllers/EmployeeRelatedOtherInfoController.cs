using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Hrm.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Hrm.EmployeesInfo.Interfaces;
using ONEERP.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.Hrm.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeRelatedOtherInfoController : Controller
    {
        object jwts;
        ApplicationUser user;
        private IUserInfoes userInfoes;
        private IEmployeeRelatedOtherInfoService employeeRelatedOtherInfoService;

        public EmployeeRelatedOtherInfoController(IUserInfoes userInfoes, IEmployeeRelatedOtherInfoService employeeRelatedOtherInfoService)
        {
            this.userInfoes = userInfoes;
            this.employeeRelatedOtherInfoService = employeeRelatedOtherInfoService;
        }

        #region Employee Address

        [HttpPost("SaveEmployeeAddress")]
        public async Task<IActionResult> SaveEmployeeAddress([FromBody] EmployeeAddressViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model.employeeId == null || model.employeeId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee address has not created successfully.", false);
                return new OkObjectResult(jwt);
            }

            bool result = await employeeRelatedOtherInfoService.SaveEmployeeAddress(user.employeeId.ToString(), model);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee address has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee address has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetEmployeeAddressById")]
        public async Task<IActionResult> GetEmployeeAddressById(int employeeAddressId, int employeeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeRelatedOtherInfoService.GetEmployeeAddressById(employeeAddressId, employeeId);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetDuplicateEmployeeAddress")]
        public async Task<IActionResult> GetDuplicateEmployeeAddress(int employeeAddressId, int employeeId, int addressTypeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeRelatedOtherInfoService.GetDuplicateEmployeeAddress(employeeAddressId, employeeId, addressTypeId);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("DeleteEmployeeAddressById")]
        public async Task<IActionResult> DeleteEmployeeAddressById([FromBody] int employeeAddressId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (employeeAddressId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee address has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await employeeRelatedOtherInfoService.DeleteEmployeeAddressById(user.employeeId.ToString(), employeeAddressId);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee address has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee address has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        #endregion

        #region Employee Job Description

        [HttpPost("SaveEmployeeJobDescription")]
        public async Task<IActionResult> SaveEmployeeJobDescription([FromBody] EmployeeJobDescriptionViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model.employeeId == null || model.employeeId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Job Description has not created successfully.", false);
                return new OkObjectResult(jwt);
            }

            bool result = await employeeRelatedOtherInfoService.SaveEmployeeJobDescription(user.employeeId.ToString(), model.lstDetails);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Job Description has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Job Description has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetEmployeeJobDescriptionById")]
        public async Task<IActionResult> GetEmployeeJobDescriptionById(int employeeJobDescriptionId, int employeeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeRelatedOtherInfoService.GetEmployeeJobDescriptionById(employeeJobDescriptionId, employeeId);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        [HttpPost("DeleteEmployeeJobDescriptionById")]
        public async Task<IActionResult> DeleteEmployeeJobDescriptionById([FromBody] int employeeJobDescriptionId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (employeeJobDescriptionId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Job Description has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await employeeRelatedOtherInfoService.DeleteEmployeeJobDescriptionById(user.employeeId.ToString(), employeeJobDescriptionId);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Job Description has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee Job Description has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        #endregion

        #region Employee Related Others Info
        [HttpGet("GetAllLevelOfEducation")]
        public async Task<IActionResult> GetAllLevelOfEducation()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeRelatedOtherInfoService.GetAllLevelOfEducation();
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetDegreeByLevelOfEducationId")]
        public async Task<IActionResult> GetDegreeByLevelOfEducationId([FromQuery] int levelOfEducationId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeRelatedOtherInfoService.GetDegreeByLevelOfEducationId(levelOfEducationId);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetMejorById")]
        public async Task<IActionResult> GetMejorById([FromQuery] int degreeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeRelatedOtherInfoService.GetMejorById(degreeId);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        //GetResultTypes
        [HttpGet("GetResultTypes")]
        public async Task<IActionResult> GetResultTypes()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeRelatedOtherInfoService.GetResultTypes();
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("SaveEmployeeEducation")]
        public async Task<IActionResult> SaveEmployeeEducation([FromBody] EmployeeEducationViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model.employeeId == null || model.employeeId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee education has not created successfully.", false);
                return new OkObjectResult(jwt);
            }

            bool result = await employeeRelatedOtherInfoService.SaveEmployeeEducation(user.employeeId.ToString(), model);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee education has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee education has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }
        [HttpGet("GetEmployeeEducationById")]
        public async Task<IActionResult> GetEmployeeEducationById(int educationalQualificationId, int employeeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeRelatedOtherInfoService.GetEmployeeEducationById(educationalQualificationId, employeeId);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpPost("DeleteEmployeeEducationById")]
        public async Task<IActionResult> DeleteEmployeeEducationById([FromBody] int educationalQualificationId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (educationalQualificationId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee education has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await employeeRelatedOtherInfoService.DeleteEmployeeEducationById(user.employeeId.ToString(), educationalQualificationId);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee education has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee education has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }
        [HttpGet("GetEmployeeAllRelations")]
        public async Task<IActionResult> GetEmployeeAllRelations()
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeRelatedOtherInfoService.GetEmployeeAllRelations();
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetEmployeeFamilyInfoById")]
        public async Task<IActionResult> GetEmployeeFamilyInfoById(int familyInfoId, int employeeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeRelatedOtherInfoService.GetEmployeeFamilyInfoById(familyInfoId, employeeId);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpPost("SaveEmployeeFamillyInfo")]
        public async Task<IActionResult> SaveEmployeeFamillyInfo([FromBody] EmployeeFamilyInfoViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model.employeeId == null || model.employeeId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee familly information has not created successfully.", false);
                return new OkObjectResult(jwt);
            }

            bool result = await employeeRelatedOtherInfoService.SaveEmployeeFamillyInfo(user.employeeId.ToString(), model);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee familly information has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee familly information has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteEmployeeFamilyInfoById")]
        public async Task<IActionResult> DeleteEmployeeFamilyInfoById([FromBody] int familyInfoId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (familyInfoId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee familly information has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await employeeRelatedOtherInfoService.DeleteEmployeeFamilyInfoById(user.employeeId.ToString(), familyInfoId);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee familly information has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee familly information has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }
        [HttpGet("GetEmployeeEmergencyContactById")]
        public async Task<IActionResult> GetEmployeeEmergencyContactById(int familyInfoId, int employeeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeRelatedOtherInfoService.GetEmployeeEmergencyContactById(familyInfoId, employeeId);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetEmployeeReferenceById")]
        public async Task<IActionResult> GetEmployeeReferenceById(int familyInfoId, int employeeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeRelatedOtherInfoService.GetEmployeeReferenceById(familyInfoId, employeeId);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpPost("UploadHrmEmployeeAttachment")]
        public async Task<IActionResult> UploadHrmEmployeeAttachment([FromBody] EmployeeAttachmentUploadViewModel model)
        {

            var uid = Request.Headers["auth_token"];
            if (uid.Count() == 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var stream = uid;
            var handler = new JwtSecurityTokenHandler();

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);

            bool result = await employeeRelatedOtherInfoService.SetHrmEmployeeAttachment(user.employeeId.ToString(), model);


            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee education has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee education has not created successfully.", false);
                return new OkObjectResult(jwt);
            }



        }
        [HttpGet("GetEmployeeExperienceById")]
        public async Task<IActionResult> GetEmployeeExperienceById(int employeeExperienceId, int employeeId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await employeeRelatedOtherInfoService.GetEmployeeExperienceById(employeeExperienceId, employeeId);
            var jwt = await Tokens.getDataWithStatus(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        [HttpPost("SaveEmployeeExperience")]
        public async Task<IActionResult> SaveEmployeeExperience([FromBody] EmployeeExperienceViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model.employeeId == null || model.employeeId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee experience information has not created successfully.", false);
                return new OkObjectResult(jwt);
            }

            bool result = await employeeRelatedOtherInfoService.SaveEmployeeExperience(user.employeeId.ToString(), model);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee familly information has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee familly information has not created successfully.", false);
                return new OkObjectResult(jwt);
            }
        }
        [HttpPost("DeleteEmployeeExperienceById")]
        public async Task<IActionResult> DeleteEmployeeExperienceById([FromBody] int employeeExperienceId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (employeeExperienceId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee experience  has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await employeeRelatedOtherInfoService.DeleteEmployeeExperienceById(user.employeeId.ToString(), employeeExperienceId);
            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee familly information has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Employee familly information has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }
        #endregion







        #region Token Check
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
    }
}
