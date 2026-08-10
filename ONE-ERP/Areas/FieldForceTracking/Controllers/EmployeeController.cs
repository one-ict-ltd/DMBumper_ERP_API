using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ONEERP.Areas.FieldForceTracking.Models;
using ONEERP.Areas.Hrm.Models;
using ONEERP.Areas.MasterData.Models;
using ONEERP.Data;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.FieldForceTracking.Interfaces;
using ONEERP.Helpers;
using ONEERP.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Controllers
{

    [Area("MasterData")]
    public class EmployeeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IChemistScheduleService _chemistScheduleService;
        private readonly IDoctorService _doctorService;
        private readonly IChemistService _chemistService;
        //private readonly IDesignationService designationService;
        private readonly IEmployeeService employeeService;

        private readonly IUserInfoes userInfoes;

        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;

        private ERPDbContext _db;
        public EmployeeController(UserManager<ApplicationUser> userManager
            , IEmployeeService employeeService
            //, IDesignationService designationService
            , IDoctorService doctorService
            , IChemistService chemistService
            , IChemistScheduleService chemistScheduleService
            , ERPDbContext db
            , SignInManager<ApplicationUser> signInManager
            , RoleManager<ApplicationRole> roleManager
            , IUserInfoes userInfoes
            , IHostingEnvironment hostingEnvironment
            , IConverter converter)
        {
            this._hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            this.userInfoes = userInfoes;
            this._chemistScheduleService = chemistScheduleService;
            this._chemistService = chemistService;
            this._doctorService = doctorService;
            // this.designationService = designationService;
            this.employeeService = employeeService;
            _db = db;
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        public async Task<IActionResult> Index()
        {
            var employee = await employeeService.GetEmployeeLoadViewModels();
            ViewBag.code = Convert.ToInt32(employee.Max(s => s.employeeId)) + 1;
            EmployeeViewModel model = new EmployeeViewModel
            {
                zoneListViewModels = await userInfoes.ZoneListViewModels(),
                //designationViewModels = await designationService.GetDesignationViewModel(),
                employeeViewModels = employee

            };
            return View(model);
        }

        //[HttpPost]

        //public async Task<IActionResult> Index([FromForm] EmployeeViewModel model)
        //public async Task<JsonResult> Index([FromForm] EmployeeViewModel model)
        //{
        //    string userName = HttpContext.User.Identity.Name;

        //    await employeeService.setEmployee(model.EMP_ID, model.EMPLOYEE_NAME, model.FATHER_NAME, model.PRESENT_ADD, model.PERMANENT_ADD, model.JOINING_DATE, Convert.ToInt32(model.DESIGNATION), model.MOBILE_NO, model.EMAIL, model.REMARKS,
        //  model.EMP_STATUS, model.BLOOD_GROUP, model.NATIONAL_ID, model.LAST_QUALIFICATION, model.POSTING_LOCATION, model.DEPOT_CODE, model.ZONE_CODE, model.REGION_CODE, model.AREA_CODE, model.TERRITORY_CODE, userName, model.EMP_TYPE);
        //    //return RedirectToAction(nameof(Index));
        //    return Json(1);
        //}

        #region masterdataapi
        [Route("global/api/GetZone")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetZone()
        {
            var depot = await userInfoes.ZoneListViewModels();
            return Json(depot.ToList());
        }

        [Route("global/api/GetZoneById")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetZoneById(int ZoneId)
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

            var depot = await userInfoes.ZoneListViewModelsByEmp((int)user.employeeId);
            return Json(depot.ToList());
        }

        [Route("global/api/GetDepot/{code}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDepot(string code)
        {
            string userName = HttpContext.User.Identity.Name;
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == userName).ToList();
            var postinglevel = employee.FirstOrDefault()?.POSTING_LOCATION;
            IEnumerable<ZoneListViewModel> zoneListViewModel = new List<ZoneListViewModel>();
            IEnumerable<DepoListViewModel> depoListViewModels = new List<DepoListViewModel>();

            if (postinglevel == "Z")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.ZoneCode == employee.FirstOrDefault().ZONE_CODE).ToList();



            }
            else if (postinglevel == "D")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();

            }
            else if (postinglevel == "R")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();

            }
            else if (postinglevel == "A")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();

            }
            else if (postinglevel == "T")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.Code == employee.FirstOrDefault().DEPOT_CODE).ToList();

            }
            else
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();

                depoListViewModels = await userInfoes.DepoListViewModels();


            }

            var depot = await userInfoes.DepoListViewModels();
            return Json(depot.Where(x => x.ZoneCode == code && depoListViewModels.Select(s => s.Code).ToList().Contains(x.Code)).ToList());
        }

        [Route("global/api/GetArea/{code}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetArea(string code)
        {
            string userName = HttpContext.User.Identity.Name;
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == userName).ToList();
            var postinglevel = employee.FirstOrDefault()?.POSTING_LOCATION;
            IEnumerable<ZoneListViewModel> zoneListViewModel = new List<ZoneListViewModel>();
            IEnumerable<DepoListViewModel> depoListViewModels = new List<DepoListViewModel>();
            IEnumerable<RegionListViewModel> regionListViewModels = new List<RegionListViewModel>();
            IEnumerable<AreaListViewModel> areaListViewModels = new List<AreaListViewModel>();

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

            }
            else
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();

                depoListViewModels = await userInfoes.DepoListViewModels();


                regionListViewModels = await userInfoes.RegionListViewModels();


                areaListViewModels = await userInfoes.AreaListViewModels();



            }

            var depot = await userInfoes.AreaListViewModels();
            return Json(depot.Where(x => x.RegionCode == code && areaListViewModels.Select(s => s.Code).ToList().Contains(x.Code)).ToList());
        }

        [Route("global/api/GetRegion/{code}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetRegion(string code)
        {
            string userName = HttpContext.User.Identity.Name;
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == userName).ToList();
            var postinglevel = employee.FirstOrDefault()?.POSTING_LOCATION;
            IEnumerable<ZoneListViewModel> zoneListViewModel = new List<ZoneListViewModel>();
            IEnumerable<DepoListViewModel> depoListViewModels = new List<DepoListViewModel>();
            IEnumerable<RegionListViewModel> regionListViewModels = new List<RegionListViewModel>();

            if (postinglevel == "Z")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();
                depoListViewModels = await userInfoes.DepoListViewModels();
                depoListViewModels = depoListViewModels.Where(x => x.ZoneCode == employee.FirstOrDefault().ZONE_CODE).ToList();
                List<string> depostlist = depoListViewModels.Select(x => x.Code).ToList();
                regionListViewModels = await userInfoes.RegionListViewModels();
                regionListViewModels = regionListViewModels.Where(x => depostlist.Contains(x.DepotCode)).ToList();



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

            }
            else
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();

                depoListViewModels = await userInfoes.DepoListViewModels();


                regionListViewModels = await userInfoes.RegionListViewModels();




            }


            var depot = await userInfoes.RegionListViewModels();
            return Json(depot.Where(x => x.DepotCode == code && regionListViewModels.Select(s => s.Code).ToList().Contains(x.Code)).ToList());
        }

        [Route("global/api/GetTerritory/{code}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetTerritory(string code)
        {
            string userName = HttpContext.User.Identity.Name;
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == userName).ToList();
            var postinglevel = employee.FirstOrDefault()?.POSTING_LOCATION;
            IEnumerable<ZoneListViewModel> zoneListViewModel = new List<ZoneListViewModel>();
            IEnumerable<DepoListViewModel> depoListViewModels = new List<DepoListViewModel>();
            IEnumerable<RegionListViewModel> regionListViewModels = new List<RegionListViewModel>();
            IEnumerable<AreaListViewModel> areaListViewModels = new List<AreaListViewModel>();
            IEnumerable<TeritoryListViewModel> teritoryListViewModels = new List<TeritoryListViewModel>();

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

            }
            else
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();

                depoListViewModels = await userInfoes.DepoListViewModels();


                regionListViewModels = await userInfoes.RegionListViewModels();


                areaListViewModels = await userInfoes.AreaListViewModels();


                teritoryListViewModels = await userInfoes.TeritoryListViewModels();




            }

            var depot = await userInfoes.TeritoryListViewModels();
            return Json(depot.Where(x => x.AreaCode == code && teritoryListViewModels.Select(s => s.Code).ToList().Contains(x.Code)).ToList());
        }

        [Route("global/api/GetEmployee")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetEmployee(string code)
        {
            string userName = HttpContext.User.Identity.Name;
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == userName).ToList();
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
                employeeLoadViewModels = employeeLoadViewModels.Where(x => terrilist.Contains(x.TERRITORY_CODE) && x.employeeNo == employee.FirstOrDefault()?.employeeNo).ToList();
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


            var depot = await employeeService.GetEmployeeLoadViewModels();
            return Json(depot.Where(x => x.TERRITORY_CODE == code && employeeLoadViewModels.Select(s => s.employeeNo).ToList().Contains(x.employeeNo)).ToList());
        }

        [Route("global/api/GetMIO")]
        [HttpGet]
        public async Task<IActionResult> getMIO(string code)
        {
            //var uid = Request.Headers["auth_token"];
            //if (uid.Count() == 0)
            //{
            //    bool status = false;
            //    string actionresult = "Invalid Token.";
            //    var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

            //    return new OkObjectResult(jwts);

            //}
            //var stream = uid;
            //var handler = new JwtSecurityTokenHandler();


            //var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            //var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            //var user = await userInfoes.GetUserBasicInfoesbyId(jti);

            //if (user.token != uid && user != null)
            //{
            //    bool status = false;
            //    string actionresult = "Invalid Token.";
            //    var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

            //    return new OkObjectResult(jwts);

            //}

            //if (Authentication().Result == false) return new OkObjectResult(jwts);
            var datajson = await employeeService.GetMIOById(code);
            var jwt = await Tokens.GetJwt(datajson.data);
            return new OkObjectResult(jwt);
        }


        [Route("global/api/GetCustomerbyMarketCode")]
        [HttpGet]
        public async Task<IActionResult> getCustomerbyMarketCode(string MarketCode)
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

            var datajson = await employeeService.GetCustomerById(MarketCode);

            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);

        }

        [Route("global/api/GetEmployeeforAllEmployee/{code}/{Type}/{SType}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetEmployeeforAllEmployee(string code, string Type, string SType)
        {
            string userName = HttpContext.User.Identity.Name;
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == userName).ToList();
            var postinglevel = employee.FirstOrDefault()?.POSTING_LOCATION;
            IEnumerable<ZoneListViewModel> zoneListViewModel = new List<ZoneListViewModel>();
            IEnumerable<DepoListViewModel> depoListViewModels = new List<DepoListViewModel>();
            IEnumerable<RegionListViewModel> regionListViewModels = new List<RegionListViewModel>();
            IEnumerable<AreaListViewModel> areaListViewModels = new List<AreaListViewModel>();
            IEnumerable<TeritoryListViewModel> teritoryListViewModels = new List<TeritoryListViewModel>();
            IEnumerable<EmployeeViewModel> employeeLoadViewModels = new List<EmployeeViewModel>();



            var depot = await employeeService.GetEmployeeLoadViewModels();
            var fdata = depot;
            if (Type == "Z")
            {
                fdata = depot.Where(x => x.ZONE_CODE == code && x.POSTING_LOCATION == SType).ToList();
            }
            else if (Type == "D")
            {
                fdata = depot.Where(x => x.DEPOT_CODE == code && x.POSTING_LOCATION == SType).ToList();
            }
            else if (Type == "R")
            {
                fdata = depot.Where(x => x.REGION_CODE == code && x.POSTING_LOCATION == SType).ToList();
            }
            else if (Type == "A")
            {
                fdata = depot.Where(x => x.AREA_CODE == code && x.POSTING_LOCATION == SType).ToList();
            }
            else if (Type == "T")
            {
                fdata = depot.Where(x => x.TERRITORY_CODE == code && x.POSTING_LOCATION == SType).ToList();
            }
            else
            {
                fdata = depot.ToList();
            }
            return Json(fdata);
        }

        [Route("global/api/GetEmployeeforAllEmployeeShow/{Type}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetEmployeeforAllEmployeeShow(string Type)
        {


            var depot = await employeeService.GetEmployeeLoadViewModels();
            var fdata = depot;
            fdata = depot.Where(x => x.POSTING_LOCATION == Type).ToList();
            return Json(fdata);
        }

        [Route("global/api/GetMarket/{code}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetMarket(string code)
        {
            var depot = await userInfoes.MarketListViewModels();
            return Json(depot.Where(x => x.TerritoryCode == code).ToList());
        }
        [Route("global/api/GetDoctor/{code}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDoctor(string code)
        {
            var depot = await _doctorService.GetAllCmnDoctor();
            return Json(depot.Where(x => x.TerritoryID == code).ToList());
        }
        [Route("global/api/GetChemist/{code}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetChemist(string code)
        {
            var depot = await _chemistService.GetAllCmnChemist();
            return Json(depot.Where(x => x.TerritoryID == code).ToList());
        }

        [Route("global/api/GetALLParameter")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetALLParameter()
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();

            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            var datajson = await employeeService.GetEmployeeLoadJsonViewModels(employee.FirstOrDefault()?.employeeNo);
            var jwt = await Tokens.getParamDataforReport(employee.FirstOrDefault()?.employeeNo, employee.FirstOrDefault()?.fullName, datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);
            //var depot = await userInfoes.TeritoryListViewModels();
            //return Json(depot.Where(x => x.AreaCode == code).ToList());
        }

        [Route("global/api/GetALLParameter_V2")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetALLParameter_V2(string userName)
        {
            var allEmployee = await employeeService.GetEmployeeLoadViewModels();
            var employee = allEmployee.Where(x => x.employeeNo == userName).ToList();
            //var user = await userInfoes.GetUserBasicInfoes(userName);

            var datajson = await employeeService.GetEmployeeLoadJsonViewModels(employee.FirstOrDefault()?.employeeNo);
            var jwt = await Tokens.getParamDataforReport(employee.FirstOrDefault()?.employeeNo, employee.FirstOrDefault()?.fullName, datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);
        }

        [Route("global/api/GetALLParameterDoctor")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetALLParameterDoctor()
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            //var employee = await employeeService.GetEmployeeLoadViewModels();
            var employee = await employeeService.GetEmployeeLoadViewModelsbyCode(user.UserName);
            //employee = employee.Where(x => x.employeeNo == user.UserName).ToList();

            if (employee.Token != uid)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            var datajson = await employeeService.GetDoctorJsonViewModels(employee.employeeNo);
            var jwt = await Tokens.getParamDataforReport(employee.employeeNo, employee.fullName, datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);
            //var depot = await userInfoes.TeritoryListViewModels();
            //return Json(depot.Where(x => x.AreaCode == code).ToList());
        }

        [Route("global/api/GetRXDetails")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetRXDetails(DateTime fDate, DateTime tDate, string zoneCode, string regionCode, string areaCode, string territoryCode, string flag)
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

            var empData = await employeeService.GetRXDetailsForEmployee(user.Id, fDate, tDate, zoneCode, regionCode, areaCode, territoryCode, flag);
            var doctorData = await employeeService.GetRXDetailsForDoctor(user.Id, fDate, tDate, zoneCode, regionCode, areaCode, territoryCode, flag);
            var itemData = await employeeService.GetRXDetailsForItem(user.Id, fDate, tDate, zoneCode, regionCode, areaCode, territoryCode, flag);
            var jwt = await Tokens.GetRxJwt(empData.data, doctorData.data, itemData.data);

            return new OkObjectResult(jwt);
        }

        [Route("global/api/GetRXImage")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetRXImage(DateTime fDate, DateTime tDate, string TerritoryCode, int DoctorId)
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

            var empData = await employeeService.GetRXImage(user.Id, fDate, tDate, TerritoryCode, DoctorId);

            var jwt = await Tokens.GetRxImage(empData.data);

            return new OkObjectResult(jwt);
        }
        [Route("global/api/GetRXImageWithProduct")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetRXImageWithProduct(string EmpId, DateTime fDate, DateTime tDate, string TerritoryCode, int DoctorId, string productName, string skuName)
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
            //skuName = WebUtility.UrlDecode(skuName);
            var empData = await employeeService.GetRXImageWithProduct(user.Id, fDate, tDate, TerritoryCode, DoctorId, productName, skuName);

            var jwt = await Tokens.GetRxImage(empData.data);

            return new OkObjectResult(jwt);
        }

        [Route("global/api/GetRXDetails_V2")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetRXDetails_V2(DateTime fDate, DateTime tDate, string zoneCode, string regionCode, string areaCode, string territoryCode, string flag, string userName)
        {
            var user = await userInfoes.GetUserBasicInfoes(userName);

            var empData = await employeeService.GetRXDetailsForEmployee(user.Id, fDate, tDate, zoneCode, regionCode, areaCode, territoryCode, flag);
            var doctorData = await employeeService.GetRXDetailsForDoctor(user.Id, fDate, tDate, zoneCode, regionCode, areaCode, territoryCode, flag);
            var itemData = await employeeService.GetRXDetailsForItem(user.Id, fDate, tDate, zoneCode, regionCode, areaCode, territoryCode, flag);
            var jwt = await Tokens.GetRxJwt(empData.data, doctorData.data, itemData.data);

            return new OkObjectResult(jwt);
        }



        [Route("global/api/GetALLParameterChemist")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetALLParameterChemist()
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();

            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }
            var datajson = await employeeService.GetChemistJsonViewModels(employee.FirstOrDefault()?.employeeNo);
            var jwt = await Tokens.getParamDataforReport(employee.FirstOrDefault()?.employeeNo, employee.FirstOrDefault()?.fullName, datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        //[Route("global/api/GetMIODoctorVisitReport/{ZoneCode}/{DepotCode}/{RegionCode}/{AreaCode}/{TerritoryCode}/{EmpCode}/{FromDate}/{ToDate}")]
        //[HttpGet]
        //[Route("global/api")]
        //[HttpGet("GetMIODoctorVisitReport")]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetMIODoctorVisitReport(string ZoneCode,string DepotCode,string RegionCode,string AreaCode,string TerritoryCode,string EmpCode,DateTime FromDate,DateTime ToDate)
        //{
        //    var uid = Request.Headers["auth_token"];
        //    if (uid.Count() == 0)
        //    {
        //        bool status = false;
        //        string actionresult = "Invalid Token.";
        //        var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

        //        return new OkObjectResult(jwts);

        //    }
        //    var stream = uid;
        //    var handler = new JwtSecurityTokenHandler();
        //    // var jsonToken = handler.ReadToken(stream);

        //    var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
        //    var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
        //    var user = await userInfoes.GetUserBasicInfoesbyId(jti);
        //    var employee = await employeeService.GetEmployeeLoadViewModels();
        //    employee = employee.Where(x => x.employeeNo == user.UserName).ToList();
        //    if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
        //    {
        //        bool status = false;
        //        string actionresult = "Invalid Token.";
        //        var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

        //        return new OkObjectResult(jwts);

        //    }
        //    IEnumerable<VisitReportDoctorViewModel> lstdata  = await _chemistScheduleService.VisitReportDoctorViewModels(ZoneCode, DepotCode,RegionCode,AreaCode,TerritoryCode,EmpCode, Convert.ToDateTime(FromDate).ToString("yyyyMMdd"), Convert.ToDateTime(ToDate).ToString("yyyyMMdd"));
        //    var jwt = await Tokens.getDoctorVisitReport(lstdata.ToList(), new JsonSerializerSettings { Formatting = Formatting.Indented });

        //    return new OkObjectResult(jwt);
        //    //var depot = await userInfoes.TeritoryListViewModels();
        //    //return Json(depot.Where(x => x.AreaCode == code).ToList());
        //}

        //[Route("global/api/GetMarket/{code}")]
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetMarket(string code)
        //{
        //    var depot = await userInfoes.MarketListViewModels();
        //    return Json(depot.Where(x => x.TerritoryCode == code).ToList());
        //}


        [Route("global/api/GetSearchList")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetSearchList()
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
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();

            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            string postinglocation = employee?.FirstOrDefault()?.POSTING_LOCATION;
            string EmpCode = "";
            string EmpName = "";
            if (employee.Count() == 0)
            {
                EmpCode = user.UserName;
                EmpName = user.UserName;
            }
            else
            {
                EmpCode = employee?.FirstOrDefault()?.employeeNo;
                EmpName = employee?.FirstOrDefault()?.fullName;
            }
            List<SearchForViewModel> searchForViewModels = new List<SearchForViewModel>();
            if (postinglocation == "Z")
            {
                searchForViewModels.Add(new SearchForViewModel
                {

                    code = "D",
                    name = "Depot"
                });
                searchForViewModels.Add(new SearchForViewModel
                {

                    code = "R",
                    name = "Region"
                });
                searchForViewModels.Add(new SearchForViewModel
                {

                    code = "A",
                    name = "Area"
                });
                searchForViewModels.Add(new SearchForViewModel
                {

                    code = "T",
                    name = "Territory"
                });
            }
            else if (postinglocation == "D")
            {

                searchForViewModels.Add(new SearchForViewModel
                {

                    code = "R",
                    name = "Region"
                });
                searchForViewModels.Add(new SearchForViewModel
                {

                    code = "A",
                    name = "Area"
                });
                searchForViewModels.Add(new SearchForViewModel
                {

                    code = "T",
                    name = "Territory"
                });
            }
            else if (postinglocation == "R")
            {


                searchForViewModels.Add(new SearchForViewModel
                {

                    code = "A",
                    name = "Area"
                });
                searchForViewModels.Add(new SearchForViewModel
                {

                    code = "T",
                    name = "Territory"
                });
            }
            else if (postinglocation == "A")
            {



                searchForViewModels.Add(new SearchForViewModel
                {

                    code = "T",
                    name = "Territory"
                });
            }
            else if (postinglocation == "T")
            {



                searchForViewModels.Add(new SearchForViewModel
                {

                    code = "T",
                    name = "Territory"
                });
            }
            else
            {


                searchForViewModels.Add(new SearchForViewModel
                {

                    code = "Z",
                    name = "Zone"
                });
                searchForViewModels.Add(new SearchForViewModel
                {

                    code = "D",
                    name = "Depot"
                });
                searchForViewModels.Add(new SearchForViewModel
                {

                    code = "R",
                    name = "Region"
                });
                searchForViewModels.Add(new SearchForViewModel
                {

                    code = "A",
                    name = "Area"
                });
                searchForViewModels.Add(new SearchForViewModel
                {

                    code = "T",
                    name = "Territory"
                });
            }
            var datajson = await employeeService.GetallParam(employee.FirstOrDefault()?.employeeNo);
            // IEnumerable<VisitReportDoctorViewModel> lstdata = await _chemistScheduleService.VisitReportDoctorViewModels(ZoneCode, DepotCode, RegionCode, AreaCode, TerritoryCode, EmpCode, Convert.ToDateTime(FromDate).ToString("yyyyMMdd"), Convert.ToDateTime(ToDate).ToString("yyyyMMdd"));
            var jwt = await Tokens.getSearchfor(EmpCode, EmpName, datajson.data, searchForViewModels, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);
            //var depot = await userInfoes.TeritoryListViewModels();
            //return Json(depot.Where(x => x.AreaCode == code).ToList());
        }



        [Route("global/api/GetCheckinOut")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCheckinOut()
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();

            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            string EmpCode = "";
            string EmpName = "";
            if (employee.Count() == 0)
            {
                EmpCode = user.UserName;
                EmpName = user.UserName;
            }
            else
            {
                EmpCode = employee?.FirstOrDefault()?.employeeNo;
                EmpName = employee?.FirstOrDefault()?.fullName;
            }
            var datajson = await employeeService.GetCheckinout(jti);
            var datajsonsummary = await employeeService.GetCheckinoutSummary(jti);
            var datajsonHistory = await employeeService.GetCheckinoutHistory(jti);


            var jwt = await Tokens.getCheckInOut(EmpCode, EmpName, datajson.data, datajsonsummary.data, datajsonHistory.data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);

        }

        [Route("global/api/GetCheckinOutSummary")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCheckinOutSummary()
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();

            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            string EmpCode = "";
            string EmpName = "";
            if (employee.Count() == 0)
            {
                EmpCode = user.UserName;
                EmpName = user.UserName;
            }
            else
            {
                EmpCode = employee?.FirstOrDefault()?.employeeNo;
                EmpName = employee?.FirstOrDefault()?.fullName;
            }
            var datajson = await employeeService.GetCheckinout(jti);
            //  var datajsonsummary = await employeeService.GetCheckinoutSummary(jti);
            //  var datajsonHistory = await employeeService.GetCheckinoutHistory(jti);


            var jwt = await Tokens.getCheckInOutSummary(EmpCode, EmpName, datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);

        }



        [Route("global/api/GetCheckinOutHistory")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCheckinOutHistory()
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();

            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            string EmpCode = "";
            string EmpName = "";
            if (employee.Count() == 0)
            {
                EmpCode = user.UserName;
                EmpName = user.UserName;
            }
            else
            {
                EmpCode = employee?.FirstOrDefault()?.employeeNo;
                EmpName = employee?.FirstOrDefault()?.fullName;
            }
            // var datajson = await employeeService.GetCheckinout(jti);
            // var datajsonsummary = await employeeService.GetCheckinoutSummary(jti);
            var datajsonHistory = await employeeService.GetCheckinoutHistory(jti);


            var jwt = await Tokens.getCheckInOutHistory(EmpCode, EmpName, datajsonHistory.data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);

        }





        [Route("global/api/GetCheckinOutdetails")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCheckinOut(int year, int month)
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();

            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            string EmpCode = "";
            string EmpName = "";
            if (employee.Count() == 0)
            {
                EmpCode = user.UserName;
                EmpName = user.UserName;
            }
            else
            {
                EmpCode = employee?.FirstOrDefault()?.employeeNo;
                EmpName = employee?.FirstOrDefault()?.fullName;
            }
            //var datajson = await employeeService.GetCheckinout(jti);
            var datajsonsummary = await employeeService.GetCheckinoutDetailsummary(jti, year, month);
            var datajsonHistory = await employeeService.GetCheckinoutDetail(jti, year, month);

            var jwt = await Tokens.getCheckInOutdetail(EmpCode, EmpName, datajsonHistory.data, datajsonsummary.data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);

        }


        [Route("global/api/VisitSummary")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> VisitSummary()
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
            // var jsonToken = handler.ReadToken(stream);

            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var user = await userInfoes.GetUserBasicInfoesbyId(jti);
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == user.UserName).ToList();

            if (employee?.FirstOrDefault()?.Token != uid && employee.Count() != 0)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });

                return new OkObjectResult(jwts);

            }
            /*
             // Un-necessary code. Commented on 24-Jan-2024
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

                try
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
                catch (Exception ex)
                {

                    throw ex;
                }
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
            List<string> emplist = employeeLoadViewModels.Select(x => x.employeeNo).ToList();

            var chemists = await _chemistService.GetAllCmnChemist();
            chemists = chemists.Where(x => teritoryListViewModels.Select(s => s.Code).ToList().Contains(x.TerritoryID)).ToList();
            var doctors = await _doctorService.GetAllCmnDoctor();
            doctors = doctors.Where(x => teritoryListViewModels.Select(s => s.Code).ToList().Contains(x.TerritoryID)).ToList();
            var chemistwise = await _chemistScheduleService.ChemistWiseVisitReportDViewModels(0, Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd"), Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd"));
            chemistwise = chemistwise.Where(x => chemists.Select(s => s.ChemistID).ToList().Contains(x.ChemistID)).ToList();
            var doctorwise = await _chemistScheduleService.DoctorWiseVisitReportDViewModels(0, Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd"), Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd"));
            doctorwise = doctorwise.Where(x => doctors.Select(s => s.DoctorID).ToList().Contains(x.DoctorID)).ToList();
            var mios = await userInfoes.MIOListViewModels();
            mios = mios.Where(x => emplist.Contains(x.EMP_ID)).ToList();
            var loginfo = await userInfoes.GetNotLoginInfoDataViews();
            loginfo = loginfo.Where(x => emplist.Contains(x?.EMP_ID)).ToList();
            var chemistWiseVisitReportViewModels = chemistwise;
            var doctorWiseVisitReportViewModels = doctorwise;

            */
            string EmpCode = "";
            string EmpName = "";
            if (employee.Count() == 0)
            {
                EmpCode = user.UserName;
                EmpName = user.UserName;
            }
            //else
            //{
            //    EmpCode = employee?.FirstOrDefault()?.employeeNo;
            //    EmpName = employee?.FirstOrDefault()?.fullName;
            //}

            List<DoctorWiseVisitReportViewModel> doctorWiseVisitReportViewModels = new List<DoctorWiseVisitReportViewModel>();
            List<ChemistWiseVisitReportViewModel> chemistWiseVisitReportViewModels = new List<ChemistWiseVisitReportViewModel>();
            var jwt = await Tokens.getVisitSummary(EmpCode, EmpName, chemistWiseVisitReportViewModels.ToList(), doctorWiseVisitReportViewModels.ToList(), new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);
        }

        #endregion




    }
}