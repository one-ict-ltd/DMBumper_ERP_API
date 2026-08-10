using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Hrm.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.FieldForceTracking.Interfaces;
using ONEERP.Helpers;
using ONEERP.Models.Dashboard;

namespace ONEERP.Areas.FieldForceTracking.Controllers
{
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        object jwts;
        ApplicationUser user;
        private readonly IUserInfoes userInfoes;
        private readonly IDashboardService dashboardService;
        private readonly IChemistService chemistService;
        private readonly IDoctorService doctorService;
        private readonly IChemistScheduleService chemistScheduleService;
        private readonly IEmployeeService employeeService;

        public DashboardController(IUserInfoes _userInfoes, IEmployeeService _employeeService, IChemistScheduleService _chemistScheduleService, IChemistService _chemistService, IDoctorService _doctorService, IDashboardService _dashboardService)
        {
            jwts = new object();
            user = new ApplicationUser();
            userInfoes = _userInfoes;
            employeeService = _employeeService;
            chemistService = _chemistService;
            doctorService = _doctorService;
            chemistScheduleService = _chemistScheduleService;
            dashboardService = _dashboardService;
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


        #region Navigation



        //public async Task<IActionResult> Navigation()
        //{
        //    string userName = HttpContext.User.Identity.Name;

        //    NavbarViewModel model = new NavbarViewModel
        //    {
        //        navbars = await navbarService.GetNavigationMenu(userName),

        //    };
        //    ViewBag.UserTypeID = 1;

        //    return PartialView("_NavMenu", model);
        //}

        //public async Task<IActionResult> AssignPage()
        //{
        //    //string userName = HttpContext.User.Identity.Name;
        //    //var userId = _db.Users.Where(x => x.UserName == userName).Select(x => x.Id).FirstOrDefault();
        //    //List<string> roleids = _db.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToList();
        //    //List<int?> lstmodule = _db.UserAccessPages.Where(x => roleids.Contains(x.applicationRoleId)).Select(x => x.navbarId).ToList();

        //    //List<int> lstparentId = _db.Navbars.Where(x => lstmodule.Contains(x.Id)).Select(x => x.parentID).ToList();
        //    //List<int> lstparentIdF = _db.Navbars.Where(x => lstparentId.Contains(x.Id)).Select(x => x.parentID).ToList();

        //    //var navdata = await pageAssignService.GetNavbars(userName);
        //    ////var adminrole = _db.UserRoles.Where(x => x.UserId == userId && x.RoleId == "0583d54e-74a8-46a3-b880-e13698723f69").ToList();
        //    ////if (adminrole.Count() == 0)
        //    ////{
        //    ////    navdata = navdata.Where(x => lstmodule.Contains(x.Id) || lstparentId.Contains(x.Id) || lstparentIdF.Contains(x.Id));
        //    ////}
        //    //List<int?> modid = navdata.Select(x => x.moduleId).ToList();
        //    //var modules = await pageAssignService.GetERPModules();
        //    ////if (adminrole.Count() == 0)
        //    ////{
        //    ////    modules = modules.Where(x => modid.Contains(x.Id));
        //    ////}
        //    string userName = HttpContext.User.Identity.Name;
        //    var userId = _db.Users.Where(x => x.UserName == userName).Select(x => x.Id).FirstOrDefault();
        //    List<string> roleids = _db.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToList();
        //    List<int?> lstmodule = _db.UserAccessPages.Where(x => roleids.Contains(x.applicationRoleId)).Select(x => x.navbarId).ToList();

        //    List<int> lstparentId = _db.Navbars.Where(x => lstmodule.Contains(x.Id)).Select(x => x.parentID).ToList();
        //    List<int> lstparentIdF = _db.Navbars.Where(x => lstparentId.Contains(x.Id)).Select(x => x.parentID).ToList();

        //    var navdata = await pageAssignService.GetNavbars(userName);
        //    var adminrole = _db.UserRoles.Where(x => x.UserId == userId && x.RoleId == "e3c27b44-2fac-4cfc-b145-61483d19b06d").ToList();
        //    if (userName != "Admin")
        //    {
        //        //navdata = navdata.Where(x => lstmodule.Contains(x.Id) || lstparentId.Contains(x.Id) || lstparentIdF.Contains(x.Id));
        //        navdata = navdata.Where(x => lstmodule.Contains(x.Id));
        //    }
        //    List<int?> modid = navdata.Select(x => x.moduleId).ToList();
        //    var modules = await pageAssignService.GetERPModules();
        //    if (adminrole.Count() == 0)
        //    {
        //        modules = modules.Where(x => modid.Contains(x.Id));
        //    }
        //    NavbarViewModel model = new NavbarViewModel
        //    {
        //        navbars = navdata,//await pageAssignService.GetNavbars(userName),
        //        ERPModules = modules//await pageAssignService.GetERPModules()
        //    };

        //    ViewBag.UserTypeID = 1;

        //    return PartialView("_Navbar", model);
        //}

        //public async Task<IActionResult> GridMenuPage(int moduleId, int perentId)
        //{
        //    string userName = HttpContext.User.Identity.Name;
        //    var userId = _db.Users.Where(x => x.UserName == userName).Select(x => x.Id).FirstOrDefault();
        //    // var data = _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
        //    List<string> roleids = _db.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToList();
        //    List<int?> lstmodule = _db.UserAccessPages.Where(x => roleids.Contains(x.applicationRoleId)).Select(x => x.navbarId).ToList();

        //    List<int> lstparentId = _db.Navbars.Where(x => lstmodule.Contains(x.Id)).Select(x => x.parentID).ToList();

        //    List<Navbar> lstMenu = _db.Navbars.Where(x => x.moduleId == moduleId && x.parentID == perentId && x.status == true).OrderBy(x => x.displayOrder).ToList();
        //    List<Navbar> lstChieldMenu = new List<Navbar>();
        //    //foreach(var item in lstMenu)
        //    //{
        //    //    lstChieldMenu.AddRange(_db.Navbars.Where(x => x.parentID == item.Id).OrderBy(x => x.displayOrder).ToList());
        //    //}

        //    var navdata = await pageAssignService.GetNavbars(userName);
        //    var adminrole = _db.UserRoles.Where(x => x.UserId == userId && x.RoleId == "0583d54e-74a8-46a3-b880-e13698723f69").ToList();
        //    if (adminrole.Count() == 0)
        //    {
        //        lstChieldMenu = navdata.Where(x => lstmodule.Contains(x.Id)).ToList();
        //        lstMenu = lstMenu.Where(x => lstparentId.Contains(x.Id)).ToList();
        //    }
        //    else
        //    {
        //        lstChieldMenu = navdata.ToList();
        //    }
        //    List<int?> modid = navdata.Select(x => x.moduleId).ToList();
        //    var modules = await pageAssignService.GetERPModules();
        //    if (adminrole.Count() == 0)
        //    {
        //        modules = modules.Where(x => modid.Contains(x.Id));
        //    }

        //    var model = new NavbarViewModel
        //    {
        //        navbars = lstChieldMenu,
        //        navbarsbyparent = lstMenu,
        //        ERPModules = modules
        //    };
        //    return View(model);
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        #endregion



        #region Dashboard
        [HttpGet("CrmDashboard")]
        public async Task<IActionResult> CrmDashboard()
        {

            //   string userName = HttpContext.User.Identity.Name;
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

            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo.ToString() == user.UserName).ToList();
            ViewBag.employee = employee.FirstOrDefault();
            ViewBag.employeeT = employee.FirstOrDefault();

            var postinglevel = employee.FirstOrDefault()?.POSTING_LOCATION;
            ViewBag.postinglevel = postinglevel;

            IEnumerable<ZoneListViewModel> zoneListViewModel = new List<ZoneListViewModel>();
            IEnumerable<DepoListViewModel> depoListViewModels = new List<DepoListViewModel>();
            IEnumerable<RegionListViewModel> regionListViewModels = new List<RegionListViewModel>();
            IEnumerable<AreaListViewModel> areaListViewModels = new List<AreaListViewModel>();
            IEnumerable<TeritoryListViewModel> teritoryListViewModels = new List<TeritoryListViewModel>();
            IEnumerable<EmployeeViewModel> employeeLoadViewModels = new List<EmployeeViewModel>();

            if (postinglevel == "Z")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.ZoneCode == employee.FirstOrDefault().ZONE_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode)).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode)).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode)).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();


            }
            else if (postinglevel == "D")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode)).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode)).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode)).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();
            }
            else if (postinglevel == "R")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode) && x.Code == employee.FirstOrDefault().REGION_CODE).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode)).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode)).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();
            }
            else if (postinglevel == "A")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode) && x.Code == employee.FirstOrDefault().REGION_CODE).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode) && x.Code == employee.FirstOrDefault().AREA_CODE).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode)).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();
            }
            else if (postinglevel == "T")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode) && x.Code == employee.FirstOrDefault().REGION_CODE).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode) && x.Code == employee.FirstOrDefault().AREA_CODE).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode) && x.Code == employee.FirstOrDefault().TERRITORY_CODE).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();
            }
            else
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();

                depoListViewModels = await userInfoes.DepoListViewModels();


                regionListViewModels = await userInfoes.RegionListViewModels();


                areaListViewModels = await userInfoes.AreaListViewModels();


                teritoryListViewModels = await userInfoes.TeritoryListViewModels();


                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();

            }

            if (zoneListViewModel.Count() == 1)
            {
                //$("div.id_100 select").val("val2");
                ViewBag.zoneId = zoneListViewModel.FirstOrDefault().Code;

            }
            if (depoListViewModels.Count() == 1)
            {
                //$("div.id_100 select").val("val2");
                ViewBag.depotId = depoListViewModels.FirstOrDefault().Code;

            }
            List<string> emplist = employeeLoadViewModels.Where(x=>x.companyId== employee.FirstOrDefault().companyId).Select(x => x.employeeId.ToString()).ToList();
            var locationdata = await userInfoes.MIOCurrentLocationDViewModelsN();
            locationdata = locationdata.Where(x => emplist.Contains(x.MIOCode)).ToList();

            var mios = await userInfoes.MIOListViewModels();
            mios = mios.Where(x => emplist.Contains(x.EMP_ID)).ToList();
            var loginfo = await userInfoes.GetNotLoginInfoDataViews();
            loginfo = loginfo.Where(x => emplist.Contains(x?.EMP_ID)).ToList();
            HomeViewModel model = new HomeViewModel
            {


                mIOCurrentLocationViewModels = locationdata,//await userInfoes.MIOCurrentLocationDViewModels(),
                                                            //   chemistWiseVisitReportViewModels = chemistwise, //await _chemistScheduleService.ChemistWiseVisitReportDViewModels(0, Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd"), Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")),

                mIOListViewModels = mios,//await userInfoes.MIOListViewModels()
                loginInfoDataViewModels = loginfo,
                zoneListViewModels = zoneListViewModel


            };

            //return View(model);

            var jwt = await Tokens.ObjToJson(model);
            return new OkObjectResult(jwt);
        }

        [HttpGet("CrmDashboardMD")]
        public async Task<ActionResult> CrmDashboardMD()
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
            
            var employee = await employeeService.GetEmployeeLoadViewModels();
            var sdata = employee.Where(x => x.employeeNo == user.UserName).FirstOrDefault();
            employee = employee.Where(x => x.employeeId.ToString() == sdata.employeeId.ToString()).ToList();
            var postinglevel = employee.FirstOrDefault()?.POSTING_LOCATION;
            IEnumerable<ZoneListViewModel> zoneListViewModel = new List<ZoneListViewModel>();
            IEnumerable<DepoListViewModel> depoListViewModels = new List<DepoListViewModel>();
            IEnumerable<RegionListViewModel> regionListViewModels = new List<RegionListViewModel>();
            IEnumerable<AreaListViewModel> areaListViewModels = new List<AreaListViewModel>();
            IEnumerable<TeritoryListViewModel> teritoryListViewModels = new List<TeritoryListViewModel>();
            IEnumerable<EmployeeViewModel> employeeLoadViewModels = new List<EmployeeViewModel>();
            zoneListViewModel = await userInfoes.ZoneListViewModels();

            depoListViewModels = await userInfoes.DepoListViewModels();


            regionListViewModels = await userInfoes.RegionListViewModels();


            areaListViewModels = await userInfoes.AreaListViewModels();


            teritoryListViewModels = await userInfoes.TeritoryListViewModels();


            employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
            List<string> emplist = employeeLoadViewModels.Select(x => x.employeeId.ToString().ToString()).ToList();
            var locationdata = await userInfoes.MIOCurrentLocationDViewModels(user.UserName);
            locationdata = locationdata.Where(x => emplist.Contains(x.MIOCode)).ToList();
            var chemists = await chemistService.GetAllCmnChemist();
            chemists = chemists.Where(x => teritoryListViewModels.Select(s => s.Code).ToList().Contains(x.TerritoryID)).ToList();
            var doctors = await doctorService.GetAllCmnDoctor();
            doctors = doctors.Where(x => teritoryListViewModels.Select(s => s.Code).ToList().Contains(x.TerritoryID)).ToList();
            var chemistwise = await chemistScheduleService.ChemistWiseVisitReportDViewModels(0, Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd"), Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd"));
            chemistwise = chemistwise.Where(x => chemists.Select(s => s.ChemistID).ToList().Contains(x.ChemistID)).ToList();
            var doctorwise = await chemistScheduleService.DoctorWiseVisitReportDViewModels(0, Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd"), Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd"));
            doctorwise = doctorwise.Where(x => doctors.Select(s => s.DoctorID).ToList().Contains(x.DoctorID)).ToList();
            var mios = await userInfoes.MIOListViewModels();
            mios = mios.Where(x => emplist.Contains(x.EMP_ID)).ToList();
            var loginfo = await userInfoes.GetNotLoginInfoDataViews();
            loginfo = loginfo.Where(x => emplist.Contains(x?.EMP_ID)).ToList();
            HomeViewModel model = new HomeViewModel
            {
                cmnChemists = chemists,//await chemistService.GetAllCmnChemist(),
                cmnDoctors = doctors,//await doctorService.GetAllCmnDoctor(),
                mIOCurrentLocationViewModels = locationdata,//await userInfoes.MIOCurrentLocationDViewModels(),
                chemistWiseVisitReportViewModels = chemistwise, //await _chemistScheduleService.ChemistWiseVisitReportDViewModels(0, Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd"), Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")),
                doctorWiseVisitReportViewModels = doctorwise, //await _chemistScheduleService.DoctorWiseVisitReportDViewModels(0, Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd"), Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")),
                mIOListViewModels = mios,//await userInfoes.MIOListViewModels()
                loginInfoDataViewModels = loginfo,
                //depoListViewModels=await userInfoes.DepoListViewModels(),
                //zoneListViewModels=await userInfoes.ZoneListViewModels(),
                //teritoryListViewModels=await userInfoes.TeritoryListViewModels(),
                //mIOListViewModels=await userInfoes.MIOListViewModels()

            };
            return View(model);
        }

        //[Route("global/api/GetLocation")]
        [HttpGet("GetLocation")]
        public async Task<IActionResult> GetLocation()
        {
            //return Json(await userInfoes.MIOCurrentLocationViewModels());
            // string userName = HttpContext.User.Identity.Name;
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            var sdata = employee.Where(x => x.employeeNo == user.UserName).FirstOrDefault();
            employee = employee.Where(x => x.employeeId.ToString() == sdata.employeeId.ToString()).ToList();
            var postinglevel = employee.FirstOrDefault()?.POSTING_LOCATION;
            IEnumerable<ZoneListViewModel> zoneListViewModel = new List<ZoneListViewModel>();
            IEnumerable<DepoListViewModel> depoListViewModels = new List<DepoListViewModel>();
            IEnumerable<RegionListViewModel> regionListViewModels = new List<RegionListViewModel>();
            IEnumerable<AreaListViewModel> areaListViewModels = new List<AreaListViewModel>();
            IEnumerable<TeritoryListViewModel> teritoryListViewModels = new List<TeritoryListViewModel>();
            IEnumerable<EmployeeViewModel> employeeLoadViewModels = new List<EmployeeViewModel>();
            if (postinglevel == "Z")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.ZoneCode == employee.FirstOrDefault().ZONE_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode)).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode)).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode)).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();


            }
            else if (postinglevel == "D")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode)).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode)).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode)).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();
            }
            else if (postinglevel == "R")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode) && x.Code == employee.FirstOrDefault().REGION_CODE).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode)).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode)).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();
            }
            else if (postinglevel == "A")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode) && x.Code == employee.FirstOrDefault().REGION_CODE).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode) && x.Code == employee.FirstOrDefault().AREA_CODE).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode)).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();
            }
            else if (postinglevel == "T")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode) && x.Code == employee.FirstOrDefault().REGION_CODE).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode) && x.Code == employee.FirstOrDefault().AREA_CODE).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode) && x.Code == employee.FirstOrDefault().TERRITORY_CODE).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();
            }
            else
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();

                depoListViewModels = await userInfoes.DepoListViewModels();


                regionListViewModels = await userInfoes.RegionListViewModels();


                areaListViewModels = await userInfoes.AreaListViewModels();


                teritoryListViewModels = await userInfoes.TeritoryListViewModels();


                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();

            }
            List<string> emplist = employeeLoadViewModels.Select(x => x.employeeId.ToString()).ToList();
            var locationdata = await userInfoes.MIOCurrentLocationDViewModels(user.UserName);
            locationdata = locationdata.Where(x => emplist.Contains(x.MIOCode)).ToList();
            return Json(locationdata);
        }

        //[Route("global/api/GetLocationAll/{Type}/{ZoneCode}/{DepotCode}/{RegionCode}/{AreaCode}/{TerritoryCode}/{EmpCode}")]
        [HttpGet("GetLocationAll")]
        public async Task<IActionResult> GetLocationAll(string Type, string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode)
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
          
            var allemp = await employeeService.GetEmployeeLoadViewModels();

            #region Comments By MOSTAFA
            var employee = allemp.Where(x => x.employeeNo == user.UserName).FirstOrDefault();
            allemp = allemp.Where(x => x.companyId == employee.companyId);
            var locationdata = await userInfoes.MIOCurrentLocationDViewModels(employee.employeeNo);
            #endregion

            if (!string.IsNullOrWhiteSpace(ZoneCode))
                allemp = allemp.Where(x => x.ZONE_CODE == ZoneCode).ToList();

            if (!string.IsNullOrWhiteSpace(DepotCode))
                allemp = allemp.Where(x => x.DEPOT_CODE == DepotCode).ToList();

            if (!string.IsNullOrWhiteSpace(RegionCode))
                allemp = allemp.Where(x => x.REGION_CODE == RegionCode).ToList();

            if (!string.IsNullOrWhiteSpace(AreaCode))
                allemp = allemp.Where(x => x.AREA_CODE == AreaCode).ToList();

            if (!string.IsNullOrWhiteSpace(TerritoryCode))
                allemp = allemp.Where(x => x.TERRITORY_CODE == TerritoryCode).ToList();

            if (!string.IsNullOrWhiteSpace(EmpCode))
                allemp = allemp.Where(x => x.employeeNo.ToString() == EmpCode).ToList();

            List<string> emplist = allemp.Select(x => x.employeeNo.ToString()).ToList();
            locationdata = locationdata.Where(x => emplist.Contains(x.MIOCode)).ToList();

            var jwt = await Tokens.ObjToJson(locationdata);
            return new OkObjectResult(jwt);
        }


        //[Route("global/api/GetSumData/{Type}/{ZoneCode}/{DepotCode}/{RegionCode}/{AreaCode}/{TerritoryCode}/{EmpCode}/{Date}")]
        [HttpGet("GetSumData")]
        public async Task<IActionResult> GetSumData(string Type, string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, DateTime Date)
        {
            //return Json(await userInfoes.MIOCurrentLocationViewModels());
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
            if (ZoneCode == "NoData")
            {
                ZoneCode = "";
            }

            if (DepotCode == "NoData")
            {
                DepotCode = "";
            }

            if (RegionCode == "NoData")
            {
                RegionCode = "";
            }

            if (AreaCode == "NoData")
            {
                AreaCode = "";
            }

            if (TerritoryCode == "NoData")
            {
                TerritoryCode = "";
            }

            if (EmpCode == "NoData")
            {
                EmpCode = "";

            }

            var res = await userInfoes.GetSummaryData(user.UserName,ZoneCode, DepotCode, RegionCode, AreaCode, TerritoryCode, EmpCode, Convert.ToDateTime(Date).ToString("yyyyMMdd"));


            //return Json(res);
            var jwt = await Tokens.ObjToJson(res);
            return new OkObjectResult(jwt);
        }

        //[Route("global/api/GetLocationMIO/{Type}/{ZoneCode}/{DepotCode}/{RegionCode}/{AreaCode}/{TerritoryCode}/{EmpCode}/{Date}")]
        [HttpGet("GetLocationMIO")]
        public async Task<IActionResult> GetLocationMIO(string Type, string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, string Date)
        {
            var allemp = await employeeService.GetEmployeeLoadViewModels();
            if (ZoneCode == "NoData")
            {
                ZoneCode = "";
            }
            else
            {
                allemp = allemp.Where(x => x.ZONE_CODE == ZoneCode).ToList();
            }
            if (DepotCode == "NoData")
            {
                DepotCode = "";
            }
            else
            {
                allemp = allemp.Where(x => x.DEPOT_CODE == DepotCode).ToList();
            }
            if (RegionCode == "NoData")
            {
                RegionCode = "";
            }
            else
            {
                allemp = allemp.Where(x => x.REGION_CODE == RegionCode).ToList();
            }
            if (AreaCode == "NoData")
            {
                AreaCode = "";
            }
            else
            {
                allemp = allemp.Where(x => x.AREA_CODE == AreaCode).ToList();
            }
            if (TerritoryCode == "NoData")
            {
                TerritoryCode = "";
            }
            else
            {
                allemp = allemp.Where(x => x.TERRITORY_CODE == TerritoryCode).ToList();
            }


            if (ZoneCode == "NoData")
            {
                ZoneCode = "";
            }
            if (DepotCode == "NoData")
            {
                DepotCode = "";
            }
            if (RegionCode == "NoData")
            {
                RegionCode = "";
            }
            if (AreaCode == "NoData")
            {
                AreaCode = "";
            }
            if (TerritoryCode == "NoData")
            {
                TerritoryCode = "";
            }
            if (EmpCode == "NoData")
            {
                EmpCode = "";

            }
            if (Type == "Z")
            {
                if (ZoneCode == "")
                {
                    allemp = allemp.Where(x => x.POSTING_LOCATION == "Z").ToList();
                }
                else
                {
                    allemp = allemp.Where(x => x.ZONE_CODE == ZoneCode && x.POSTING_LOCATION == "Z").ToList();
                }
            }
            else if (Type == "D")
            {
                if (DepotCode == "")
                {
                    allemp = allemp.Where(x => x.POSTING_LOCATION == "D").ToList();
                }
                else
                {
                    allemp = allemp.Where(x => x.DEPOT_CODE == DepotCode && x.POSTING_LOCATION == "D").ToList();
                }
            }
            else if (Type == "R")
            {
                if (RegionCode == "")
                {
                    allemp = allemp.Where(x => x.POSTING_LOCATION == "R").ToList();
                }
                else
                {
                    allemp = allemp.Where(x => x.REGION_CODE == RegionCode && x.POSTING_LOCATION == "R").ToList();
                }
            }
            else if (Type == "A")
            {
                if (AreaCode == "")
                {
                    allemp = allemp.Where(x => x.POSTING_LOCATION == "A").ToList();
                }
                else
                {
                    allemp = allemp.Where(x => x.AREA_CODE == AreaCode && x.POSTING_LOCATION == "A").ToList();
                }
            }
            else if (Type == "T")
            {
                if (TerritoryCode == "")
                {
                    allemp = allemp.Where(x => x.POSTING_LOCATION == "T").ToList();
                }
                else
                {
                    allemp = allemp.Where(x => x.TERRITORY_CODE == TerritoryCode && x.POSTING_LOCATION == "T").ToList();
                }
            }
            else
            {
                allemp = allemp.ToList();
            }
            if (EmpCode != "")
            {
                allemp = allemp.Where(x => x.employeeId.ToString() == EmpCode).ToList();
            }
            var locationdata = await userInfoes.MIOCurrentLocationViewModelsByMIO(ZoneCode, DepotCode, RegionCode, AreaCode, TerritoryCode, EmpCode, Convert.ToDateTime(Date).ToString("yyyyMMdd"));
            List<string> emplist = allemp.Select(x => x.employeeId.ToString()).ToList();
            locationdata = locationdata.Where(x => emplist.Contains(x.MIOCode)).ToList();
            return Json(locationdata);
        }

        [HttpGet("GetInvoiceCollection")]
        public async Task<IActionResult> GetInvoiceCollection()
        {
            // string userName = HttpContext.User.Identity.Name;
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            var sdata = employee.Where(x => x.employeeNo == user.UserName).FirstOrDefault();
            employee = employee.Where(x => x.employeeId.ToString() == sdata.employeeId.ToString()).ToList();
            var postinglevel = employee.FirstOrDefault()?.POSTING_LOCATION;
            IEnumerable<ZoneListViewModel> zoneListViewModel = new List<ZoneListViewModel>();
            IEnumerable<DepoListViewModel> depoListViewModels = new List<DepoListViewModel>();
            IEnumerable<RegionListViewModel> regionListViewModels = new List<RegionListViewModel>();
            IEnumerable<AreaListViewModel> areaListViewModels = new List<AreaListViewModel>();
            IEnumerable<TeritoryListViewModel> teritoryListViewModels = new List<TeritoryListViewModel>();
            IEnumerable<EmployeeViewModel> employeeLoadViewModels = new List<EmployeeViewModel>();
            if (postinglevel == "Z")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.ZoneCode == employee.FirstOrDefault().ZONE_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode)).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode)).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode)).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();


            }
            else if (postinglevel == "D")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode)).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode)).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode)).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();
            }
            else if (postinglevel == "R")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode) && x.Code == employee.FirstOrDefault().REGION_CODE).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode)).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode)).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();
            }
            else if (postinglevel == "A")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode) && x.Code == employee.FirstOrDefault().REGION_CODE).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode) && x.Code == employee.FirstOrDefault().AREA_CODE).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode)).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();
            }
            else if (postinglevel == "T")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode) && x.Code == employee.FirstOrDefault().REGION_CODE).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode) && x.Code == employee.FirstOrDefault().AREA_CODE).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode) && x.Code == employee.FirstOrDefault().TERRITORY_CODE).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();
            }
            else
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();

                depoListViewModels = await userInfoes.DepoListViewModels();


                regionListViewModels = await userInfoes.RegionListViewModels();


                areaListViewModels = await userInfoes.AreaListViewModels();


                teritoryListViewModels = await userInfoes.TeritoryListViewModels();


                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();

            }
            List<string> emplist = employeeLoadViewModels.Select(x => x.employeeId.ToString()).ToList();
            var locationdata = await userInfoes.MIOCurrentLocationDViewModels(user.UserName);
            locationdata = locationdata.Where(x => emplist.Contains(x.MIOCode)).ToList();
            var chemists = await chemistService.GetAllCmnChemist();
            chemists = chemists.Where(x => teritoryListViewModels.Select(s => s.Code).ToList().Contains(x.TerritoryID)).ToList();
            //var doctors = await doctorService.GetAllCmnDoctor();
            //doctors = doctors.Where(x => teritoryListViewModels.Select(s => s.Code).ToList().Contains(x.TerritoryID)).ToList();
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var mios = await userInfoes.MIOListViewModels();
            mios = mios.Where(x => emplist.Contains(x.EMP_ID)).ToList();
            var chemistwiseall = await chemistScheduleService.ChemistWiseVisitReportDViewModels(0, Convert.ToDateTime(firstDayOfMonth).ToString("yyyyMMdd"), Convert.ToDateTime(lastDayOfMonth).ToString("yyyyMMdd"));
            chemistwiseall = chemistwiseall.Where(x => chemists.Select(s => s.ChemistID).ToList().Contains(x.ChemistID)).ToList();
            var data = (from pr in chemistwiseall

                        group pr by new { pr.date }
              into grp
                        select new
                        {
                            grp.Key.date,

                            totalInvoice = grp.Sum(x => x?.invoiceAmount),
                            totalCollection = grp.Sum(x => x?.collectionAmount),

                        }).ToList();


            var mdata = data.OrderBy(x => x.date);
            return Json(mdata);
        }

        [HttpGet("GetStockSales")]
        public async Task<IActionResult> GetStockSales(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, DateTime Date)
        {
            string userName = HttpContext.User.Identity.Name;

            var mdata = await chemistScheduleService.StockSalesChartViewModels(ZoneCode, DepotCode, RegionCode, AreaCode, TerritoryCode, EmpCode, Convert.ToDateTime(Date).ToString("yyyyMMdd"));

            //return Json(mdata.OrderBy(x => x.StockQty));
            var jwt = await Tokens.ObjToJson(mdata);
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetAttendanceData")]
        public async Task<IActionResult> GetAttendanceData(string Type, string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, DateTime Date)
        {
            string userName = HttpContext.User.Identity.Name;
            if (ZoneCode == null)
            {
                ZoneCode = "";
            }
            if (RegionCode == null)
            {
                RegionCode = "";
            }
            if (AreaCode == null)
            {
                AreaCode = "";
            }
            if (DepotCode == null)
            {
                DepotCode = "";
            }
            if (TerritoryCode == null)
            {
                TerritoryCode = "";
            }
            if (EmpCode == null)
            {
                EmpCode = "";
            }

            var res = await chemistScheduleService.AttendanceViewModels(Type, ZoneCode, DepotCode, RegionCode, AreaCode, TerritoryCode, EmpCode, Date);
            //return Json(res);
            var jwt = await Tokens.GetJwt(res.data);
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetStockSalesS")]
        public async Task<IActionResult> GetStockSalesS(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, DateTime Date)
        {
            string userName = HttpContext.User.Identity.Name;
            //var employee = await employeeService.GetEmployeeLoadViewModels();
            //employee = employee.Where(x => x.employeeId.ToString() == userName).ToList();
            //var postinglevel = employee.FirstOrDefault()?.POSTING_LOCATION;
            ////IEnumerable<ZoneListViewModel> zoneListViewModel = new List<ZoneListViewModel>();
            ////IEnumerable<DepoListViewModel> depoListViewModels = new List<DepoListViewModel>();
            ////IEnumerable<RegionListViewModel> regionListViewModels = new List<RegionListViewModel>();
            ////IEnumerable<AreaListViewModel> areaListViewModels = new List<AreaListViewModel>();
            ////IEnumerable<TeritoryListViewModel> teritoryListViewModels = new List<TeritoryListViewModel>();
            ////IEnumerable<EmployeeViewModel> employeeLoadViewModels = new List<EmployeeViewModel>();
            //string ZoneCode = "";
            //string DepotCode = "";
            //string RegionCode = "";
            //string AreaCode = "";
            //string TerritoryCode = "";
            //if (postinglevel == "Z")
            //{
            //    ZoneCode = employee.FirstOrDefault().ZONE_CODE;


            //}
            //else if (postinglevel == "D")
            //{
            //    ZoneCode = employee.FirstOrDefault().ZONE_CODE;
            //    DepotCode = employee.FirstOrDefault().DEPOT_CODE;


            //}
            //else if (postinglevel == "R")
            //{
            //    ZoneCode = employee.FirstOrDefault().ZONE_CODE;
            //    DepotCode = employee.FirstOrDefault().DEPOT_CODE;
            //    RegionCode = employee.FirstOrDefault().REGION_CODE;
            //}
            //else if (postinglevel == "A")
            //{
            //    ZoneCode = employee.FirstOrDefault().ZONE_CODE;
            //    DepotCode = employee.FirstOrDefault().DEPOT_CODE;
            //    RegionCode = employee.FirstOrDefault().REGION_CODE;
            //    AreaCode = employee.FirstOrDefault().AREA_CODE;
            //}
            //else if (postinglevel == "T")
            //{
            //    ZoneCode = employee.FirstOrDefault().ZONE_CODE;
            //    DepotCode = employee.FirstOrDefault().DEPOT_CODE;
            //    RegionCode = employee.FirstOrDefault().REGION_CODE;
            //    AreaCode = employee.FirstOrDefault().AREA_CODE;
            //    TerritoryCode = employee.FirstOrDefault().TERRITORY_CODE;
            //}
            //else
            //{
            //    ZoneCode = "";
            //    DepotCode = "";
            //    RegionCode = "";
            //    AreaCode = "";
            //    TerritoryCode = "";

            //}


            var mdata = await chemistScheduleService.StockSalesChartViewModels(ZoneCode, DepotCode, RegionCode, AreaCode, TerritoryCode, EmpCode, Convert.ToDateTime(Date).ToString("yyyyMMdd"));
            return Json(mdata.OrderBy(x => x.SaleQty));
        }

        [HttpGet("GetStockSalesSS")]
        public async Task<IActionResult> GetStockSalesSS(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, DateTime Date)
        {
            string userName = HttpContext.User.Identity.Name;
            var mdata = await chemistScheduleService.StockSalesChartViewModelsSale(ZoneCode, DepotCode, RegionCode, AreaCode, TerritoryCode, EmpCode, Convert.ToDateTime(Date).ToString("yyyyMMdd"));
            //return Json(mdata.OrderBy(x => x.SaleQty));
            var jwt = await Tokens.ObjToJson(mdata);
            return new OkObjectResult(jwt);
        }

        //[Route("global/api/GetLoginData/")]
        [HttpGet("GetLoginData")]
        public async Task<IActionResult> GetLoginData()
        {
            string userName = HttpContext.User.Identity.Name;
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeId.ToString() == userName).ToList();
            var postinglevel = employee.FirstOrDefault()?.POSTING_LOCATION;
            IEnumerable<ZoneListViewModel> zoneListViewModel = new List<ZoneListViewModel>();
            IEnumerable<DepoListViewModel> depoListViewModels = new List<DepoListViewModel>();
            IEnumerable<RegionListViewModel> regionListViewModels = new List<RegionListViewModel>();
            IEnumerable<AreaListViewModel> areaListViewModels = new List<AreaListViewModel>();
            IEnumerable<TeritoryListViewModel> teritoryListViewModels = new List<TeritoryListViewModel>();
            IEnumerable<EmployeeViewModel> employeeLoadViewModels = new List<EmployeeViewModel>();
            if (postinglevel == "Z")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.ZoneCode == employee.FirstOrDefault().ZONE_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode)).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode)).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode)).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();


            }
            else if (postinglevel == "D")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode)).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode)).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode)).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();
            }
            else if (postinglevel == "R")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode) && x.Code == employee.FirstOrDefault().REGION_CODE).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode)).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode)).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();
            }
            else if (postinglevel == "A")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode) && x.Code == employee.FirstOrDefault().REGION_CODE).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode) && x.Code == employee.FirstOrDefault().AREA_CODE).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode)).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();
            }
            else if (postinglevel == "T")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode) && x.Code == employee.FirstOrDefault().REGION_CODE).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode) && x.Code == employee.FirstOrDefault().AREA_CODE).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode) && x.Code == employee.FirstOrDefault().TERRITORY_CODE).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();
            }
            else
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();

                depoListViewModels = await userInfoes.DepoListViewModels();


                regionListViewModels = await userInfoes.RegionListViewModels();


                areaListViewModels = await userInfoes.AreaListViewModels();


                teritoryListViewModels = await userInfoes.TeritoryListViewModels();


                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();

            }
            List<string> emplist = employeeLoadViewModels.Select(x => x.employeeId.ToString()).ToList();

            var loginfo = await userInfoes.GetLoginInfoDataViews();
            loginfo = loginfo.Where(x => emplist.Contains(x?.EMP_ID)).ToList();
            return Json(loginfo);
        }

        //[Route("global/api/GetOutData/")]
        [HttpGet("GetOutData")]
        public async Task<IActionResult> GetOutData()
        {
            string userName = HttpContext.User.Identity.Name;
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeId.ToString() == userName).ToList();
            var postinglevel = employee.FirstOrDefault()?.POSTING_LOCATION;
            IEnumerable<ZoneListViewModel> zoneListViewModel = new List<ZoneListViewModel>();
            IEnumerable<DepoListViewModel> depoListViewModels = new List<DepoListViewModel>();
            IEnumerable<RegionListViewModel> regionListViewModels = new List<RegionListViewModel>();
            IEnumerable<AreaListViewModel> areaListViewModels = new List<AreaListViewModel>();
            IEnumerable<TeritoryListViewModel> teritoryListViewModels = new List<TeritoryListViewModel>();
            IEnumerable<EmployeeViewModel> employeeLoadViewModels = new List<EmployeeViewModel>();
            if (postinglevel == "Z")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.ZoneCode == employee.FirstOrDefault().ZONE_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode)).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode)).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode)).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();


            }
            else if (postinglevel == "D")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode)).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode)).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode)).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();
            }
            else if (postinglevel == "R")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode) && x.Code == employee.FirstOrDefault().REGION_CODE).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode)).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode)).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();
            }
            else if (postinglevel == "A")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode) && x.Code == employee.FirstOrDefault().REGION_CODE).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode) && x.Code == employee.FirstOrDefault().AREA_CODE).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode)).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();
            }
            else if (postinglevel == "T")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode) && x.Code == employee.FirstOrDefault().REGION_CODE).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode) && x.Code == employee.FirstOrDefault().AREA_CODE).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode) && x.Code == employee.FirstOrDefault().TERRITORY_CODE).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();
            }
            else
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();

                depoListViewModels = await userInfoes.DepoListViewModels();


                regionListViewModels = await userInfoes.RegionListViewModels();


                areaListViewModels = await userInfoes.AreaListViewModels();


                teritoryListViewModels = await userInfoes.TeritoryListViewModels();


                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();

            }
            List<string> emplist = employeeLoadViewModels.Select(x => x.employeeId.ToString()).ToList();

            var loginfo = await userInfoes.GetNotLoginInfoDataViews();
            loginfo = loginfo.Where(x => emplist.Contains(x?.EMP_ID)).ToList();
            return Json(loginfo);
        }

        //[Route("global/api/GetNotLocationData/")]
        [HttpGet("GetNotLocationData")]
        public async Task<IActionResult> GetNotLocationData()
        {
            string userName = HttpContext.User.Identity.Name;
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeId.ToString() == userName).ToList();
            var postinglevel = employee.FirstOrDefault()?.POSTING_LOCATION;
            IEnumerable<ZoneListViewModel> zoneListViewModel = new List<ZoneListViewModel>();
            IEnumerable<DepoListViewModel> depoListViewModels = new List<DepoListViewModel>();
            IEnumerable<RegionListViewModel> regionListViewModels = new List<RegionListViewModel>();
            IEnumerable<AreaListViewModel> areaListViewModels = new List<AreaListViewModel>();
            IEnumerable<TeritoryListViewModel> teritoryListViewModels = new List<TeritoryListViewModel>();
            IEnumerable<EmployeeViewModel> employeeLoadViewModels = new List<EmployeeViewModel>();
            if (postinglevel == "Z")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.ZoneCode == employee.FirstOrDefault().ZONE_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode)).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode)).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode)).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();


            }
            else if (postinglevel == "D")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode)).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode)).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode)).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();
            }
            else if (postinglevel == "R")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode) && x.Code == employee.FirstOrDefault().REGION_CODE).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode)).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode)).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();
            }
            else if (postinglevel == "A")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode) && x.Code == employee.FirstOrDefault().REGION_CODE).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode) && x.Code == employee.FirstOrDefault().AREA_CODE).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode)).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();
            }
            else if (postinglevel == "T")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode) && x.Code == employee.FirstOrDefault().REGION_CODE).ToList();
                List<string> regionlist = regionListViewModels.Select(x => x.Code).ToList();
                areaListViewModels = await userInfoes.AreaListViewModels();
                areaListViewModels = areaListViewModels.Where(x => regionlist.Contains(x.RegionCode) && x.Code == employee.FirstOrDefault().AREA_CODE).ToList();
                List<string> arealist = areaListViewModels.Select(x => x.Code).ToList();
                teritoryListViewModels = await userInfoes.TeritoryListViewModels();
                teritoryListViewModels = teritoryListViewModels.Where(x => arealist.Contains(x.AreaCode) && x.Code == employee.FirstOrDefault().TERRITORY_CODE).ToList();
                List<string> terrilist = teritoryListViewModels.Select(x => x.Code).ToList();
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE)).ToList();
            }
            else
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();

                depoListViewModels = await userInfoes.DepoListViewModels();


                regionListViewModels = await userInfoes.RegionListViewModels();


                areaListViewModels = await userInfoes.AreaListViewModels();


                teritoryListViewModels = await userInfoes.TeritoryListViewModels();


                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();

            }
            List<string> emplist = employeeLoadViewModels.Select(x => x.employeeId.ToString()).ToList();

            var loginfo = await userInfoes.GetNotLocationInfoDataViews();
            loginfo = loginfo.Where(x => emplist.Contains(x?.EMP_ID)).ToList();
            return Json(loginfo);
        }


        [HttpGet("GetSalesVsCollectionChartData")]
        public async Task<IActionResult> GetSalesVsCollectionChartData(int Totaldays, string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, DateTime FDate)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var res = await dashboardService.GetSalesVsCollectionChartData(Totaldays, ZoneCode, DepotCode, RegionCode, AreaCode, TerritoryCode, EmpCode, FDate);
            var jwt = await Tokens.GetJwt(res.data);
            return new OkObjectResult(jwt);
        }

        #endregion

        ////[Route("global/api/AjaxFileReceive/")]
        //[HttpPost]
        //public async Task<IActionResult> AjaxFileReceive(Models model)
        //{
        //    string userName = HttpContext.User.Identity.Name;
        //    var zoneListViewModel = await userInfoes.ZoneListViewModels();
        //    var jsonData = "[{\"data\":\"Image File Received !\"}]";
        //    return Json(jsonData);
        //}

    }

    //public class Models
    //{
    //    public string filesName { get; set; }
    //    public IList<Microsoft.AspNetCore.Http.IFormFile> files { get; set; }
    //}
}