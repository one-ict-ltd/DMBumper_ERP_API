using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ONEERP.Areas.Auth.Models;
using ONEERP.Controllers;
using ONEERP.Data;
using ONEERP.Data.Entity;
using ONEERP.Data.Entity.Auth;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.AuthService.Interfaces;
using ONEERP.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ONEERP.Areas.Auth.Controllers
{
    [Authorize]
    [Area("Auth")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IUserInfoes _userInfos;

        private ERPDbContext _db;
        public AccountController(UserManager<ApplicationUser> userManager, ERPDbContext db, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IUserInfoes userInfoes)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userInfos = userInfoes;
            _db = db;
        }

        #region Index
        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region Login

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LogInViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var userInfos = await _userInfos.GetUserInfoByUser(model.Name);
                if (userInfos != null)
                {
                    if (userInfos.isActive == 1)
                    {
                        var result = await _signInManager.PasswordSignInAsync(userInfos.UserName, model.Password, model.RememberMe, lockoutOnFailure: true);
                        if (result.Succeeded)
                        {
                            return RedirectToLocal(returnUrl);

                            //var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                            //var userAgent = Request.Headers["User-Agent"].ToString();
                            //var mechineName = Environment.MachineName;
                            //var rip = Dns.GetHostEntry(HttpContext.Connection.RemoteIpAddress.ToString()).ToString();
                            //var userinofs =await userInfoes.GetUserInfoByUser(model.Name);
                            //var employeeinfo =await personalInfoService.GetEmployeeInfoByApplicationId(userinofs?.aspnetId);
                            //UserLogHistory userLog = new UserLogHistory
                            //{
                            //    userId = model.Name,
                            //    logTime = DateTime.Now,
                            //    status = 1,
                            //    ipAddress = ip,
                            //    pcName = mechineName,
                            //    browserName = userAgent
                            //};

                            //await dbChangeService.SaveUserLogHistory(userLog);

                            //EmpAttendance empAttendance = new EmpAttendance
                            //{
                            //    punchCardNo = employeeinfo.employeeCode,
                            //    startTime = DateTime.Now,
                            //    summaryId=1
                            //};
                            //await attendanceProcessService.SaveEmpAttendence(empAttendance);

                            //FormsAuth.SetAuthCookie(username, rememberMe);

                            // return RedirectToLocal(returnUrl);
                            //var claims = new List<Claim>();
                            //var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                            //var principal = new ClaimsPrincipal(identity);

                            //var props = new AuthenticationProperties();
                            //props.IsPersistent = model.RememberMe;

                            //HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal, props).Wait();
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }


            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        #endregion

        #region  Module Register

        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> RegisterModuleAdmin(string returnUrl = null)
        //{
        //    ViewData["ReturnUrl"] = returnUrl;

        //    var roles = await _roleManager.Roles.ToListAsync();
        //    List<ApplicationRoleViewModel> lstRole = new List<ApplicationRoleViewModel>();
        //    foreach (var data in roles)
        //    {
        //        ApplicationRoleViewModel modelr = new ApplicationRoleViewModel
        //        {
        //            RoleId = data.Id,
        //            RoleName = data.Name
        //        };
        //        lstRole.Add(modelr);
        //    }
        //    var model = new RegisterViewModel
        //    {
        //        userRoles = lstRole,
        //    };
        //    return View(model);
        //}


        //[HttpGet]
        //public async Task<IActionResult> UpdateStatus(string Id, int status)
        //{
        //   // await userInfoes.UpdateAspNetUserByUserIdAndStatus(Id, status);
        //    return RedirectToAction(nameof(UserList));
        //}

        //[HttpGet]
        //public async Task<IActionResult> Delete(string Id)
        //{
        //    string userName = HttpContext.User.Identity.Name;
        //   // await userInfoes.DeleteAspNetUserByUserId(Id, userName);
        //    return RedirectToAction(nameof(UserList));
        //}


        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> RegisterModuleAdmin(RegisterViewModel model, string returnUrl = null)
        //{
        //    ViewData["ReturnUrl"] = returnUrl;
        //    if (ModelState.IsValid)
        //    {
        //        int maxUserId = await _userInfos.GetMaxUserId() + 1;
        //        var user = new ApplicationUser { UserName = model.Name, Email = model.Email };
        //        var result = await _userManager.CreateAsync(user, model.Password);

        //        if (result.Succeeded)
        //        {
        //            await _userManager.AddToRoleAsync(user, model.RoleId);
        //            IdentityUser temp = await _userManager.FindByNameAsync(model.Name);
        //            return RedirectToLocal(returnUrl);
        //        }
        //        AddErrors(result);
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        #endregion

        #region Role

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> UserRoleCreate()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            List<ApplicationRoleViewModel> lstRole = new List<ApplicationRoleViewModel>();
            foreach (var data in roles)
            {
                ApplicationRoleViewModel model = new ApplicationRoleViewModel
                {
                    RoleId = data.Id,
                    RoleName = data.Name
                };
                lstRole.Add(model);
            }
            ApplicationRoleViewModel viewModel = new ApplicationRoleViewModel
            {
                // eRPModules = await userInfoes.GetAllERPModule(),
                roleViewModels = lstRole
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UserIdentityRoleCreate([FromForm] ApplicationRoleViewModel model)
        {
            var user = new ApplicationRole(model.RoleName);
            IdentityResult result = await _roleManager.CreateAsync(user);

            return RedirectToAction(nameof(UserRoleCreate));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> UserList()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            List<ApplicationRoleViewModel> lstRole = new List<ApplicationRoleViewModel>();
            foreach (var data in roles)
            {
                ApplicationRoleViewModel modelr = new ApplicationRoleViewModel
                {
                    RoleId = data.Id,
                    RoleName = data.Name
                };
                if (data.Name == "admin" || data.Name == "ERPAdmin")
                {

                }
                else
                {
                    lstRole.Add(modelr);
                }
            }

            UserListViewModel model = new UserListViewModel
            {
                aspNetUsersViewModels = await _userInfos.GetUserInfoList(),
                userRoles = lstRole,
            };
            return View(model);
        }

        public async Task<IActionResult> DeleteRoles(string id)
        {
            try
            {
                await _userInfos.DeleteRoleById(id);
                return Json("Success");
            }
            catch
            {
                return Json("Roles is Already Assigned Someone!!");
            }
        }
        

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteUserList()
        {
            UserListViewModel model = new UserListViewModel
            {
                // userBackUpViewModels = await userInfoes.GetUserBackupListWithEmp()
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> EditRole([FromForm] ApplicationRoleViewModel model)
        {

            ApplicationUser user = await _userManager.FindByNameAsync(model.EuserName);
            //var oldRoleId = _userManager.GetUsersInRoleAsync(model.userName);
            //var oldRoleName = _roleManager.Roles.SingleOrDefault(r => r.Id == oldRoleId).Name;
            if (model.PreRoleId != null)
            {
                await _userManager.RemoveFromRoleAsync(user, model.PreRoleId);
            }
            await _userManager.AddToRoleAsync(user, model.RoleId);
            return RedirectToAction(nameof(UserList));
        }

        #endregion

        #region Logout

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            //var userinofs = await userInfoes.GetUserInfoByUser(HttpContext.User.Identity.Name);
            //var employeeinfo = await personalInfoService.GetEmployeeInfoByApplicationId(userinofs?.aspnetId);
            //UserLogHistory userLog = new UserLogHistory
            //{
            //    userId = HttpContext.User.Identity.Name,
            //    logTime = DateTime.Now,
            //    status = 0,
            //    ipAddress = ip
            //};
            //await dbChangeService.SaveUserLogHistory(userLog);
            //EmpAttendance empAttendance = new EmpAttendance
            //{
            //    punchCardNo = employeeinfo.employeeCode,
            //    startTime = DateTime.Now,
            //    summaryId = 1
            //};
            //await attendanceProcessService.SaveEmpAttendence(empAttendance);
            //_logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        #endregion

        #region Password

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePsswordViewModel model)
        {
            string message = "Fail To Update Password";
            if (ModelState.IsValid)
            {
                var data = await _userManager.ChangePasswordAsync(await _userManager.FindByNameAsync(HttpContext.User.Identity.Name), model.OldPassword, model.Password);
                message = data.ToString();
            }
            return RedirectToAction(nameof(HomeController.Index), "Home", new { Message = message });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword()
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel
            {
                //fLang = _lang.PerseLang("Auth/AuthEN.json", "Auth/AuthBN.json", Request.Cookies["lang"]),

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var userName = await personalInfoService.GetUserInfoByEmpCode(model.Name);
                ////var userName = await userInfoes.GetemployeebyempCode(model.Name);
                //ApplicationUser user = await _userManager.FindByNameAsync(userName.UserName);
                //var result = await _userManager.RemovePasswordAsync(user);
                //var results = await _userManager.AddPasswordAsync(user, model.Password);
                //string filter = model.Name;
                //if (results.Succeeded)
                //{
                //    TempData["Success"] = "Password Changed Successfully!";
                //}
                //else
                //{
                //    AddErrors(results);
                //}

            }
            //return View(model);
            return RedirectToAction(nameof(AccountController.UserList));
        }

        #endregion

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                //return Redirect(returnUrl);
                var userId = HttpContext.User.Identity.Name;
                if (userId == "sabbir@emergingrating.com")
                {
                    return RedirectToAction(nameof(HomeController.CrmDashboard), "Home");
                }
                else
                {
                    //return RedirectToAction(nameof(HomeController.Index), "Home");
                    return RedirectToAction(nameof(HomeController.CrmDashboard), "Home");
                }
            }
            else
            {
                var userId = HttpContext.User.Identity.Name;
                if (userId == "sabbir@emergingrating.com")
                {
                    return RedirectToAction(nameof(HomeController.CrmDashboard), "Home");
                }
                else
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
        }
        #endregion

    }
}