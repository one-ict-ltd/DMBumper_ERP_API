using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ONEERP.Areas.FieldForceTracking.Models;
using ONEERP.Areas.Hrm.Models;
using ONEERP.Data;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.FieldForceTracking.Interfaces;
using ONEERP.Helpers;
using ONEERP.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.Areas.FieldForceTracking.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IChemistScheduleService _chemistScheduleService;
        private readonly IDoctorService _doctorService;
        private readonly IChemistService _chemistService;
        private readonly IEmployeeService employeeService;
        private readonly IUserInfoes userInfoes;
        private readonly IReportService reportService;

        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;

        object jwts;
        ApplicationUser user;

        private ERPDbContext _db;
        public ReportController(UserManager<ApplicationUser> userManager, IEmployeeService employeeService, IDoctorService doctorService, IChemistService chemistService, IChemistScheduleService chemistScheduleService, ERPDbContext db, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager, IUserInfoes userInfoes, IHostingEnvironment hostingEnvironment, IConverter converter, IReportService _reportService)
        {
            jwts = new object();
            user = new ApplicationUser();

            this._hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            this.userInfoes = userInfoes;
            this._chemistScheduleService = chemistScheduleService;
            this._chemistService = chemistService;
            this._doctorService = doctorService;
            this.employeeService = employeeService;
            _db = db;
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
            reportService = _reportService;
        }

        #region Calender

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Calender()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Calender([FromForm] MIOVisitReportViewModel model)
        {
            string userName = HttpContext.User.Identity.Name;

            for (int i = 0; i < model.day.Length; i++)
            {
                int hd = 0;
                //1 for holiday
                //2 for weeked
                if (model.selectIds[i] == 1)
                {
                    hd = 1;
                }
                else if (model.WeekendIds[i] == 1)
                {
                    hd = 2;
                }
                await employeeService.setCalender(userName, (int)model.day[i], (DateTime)model.date[i], model.dayName[i], model.month, model.year, hd);
            }

            return RedirectToAction(nameof(Calender));
            ///return Json(1);
        }

        #endregion

        #region MIO | TSO | Empployee
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> MIOVisitReport()
        {

            string userName = HttpContext.User.Identity.Name;
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == userName).ToList();
            var postinglevel = employee.FirstOrDefault()?.POSTING_LOCATION;

            IEnumerable<ZoneListViewModel> zoneListViewModel = new List<ZoneListViewModel>();

            if (postinglevel == "Z")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();



            }
            else if (postinglevel == "D")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();

            }
            else if (postinglevel == "R")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();

            }
            else if (postinglevel == "A")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();

            }
            else if (postinglevel == "T")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();

            }
            else
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();



            }

            MIOVisitReportViewModel model = new MIOVisitReportViewModel
            {
                zoneListViewModels = zoneListViewModel,
                //aspNetUsersViewModels = await userInfoes.GetAllUserInfo(),
                //userInfoViewModels = await userInfoes.GetUserInfoViewModel()

            };
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> EmployeeVisitReport()
        {

            string userName = HttpContext.User.Identity.Name;
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == userName).ToList();
            var postinglevel = employee.FirstOrDefault()?.POSTING_LOCATION;

            IEnumerable<ZoneListViewModel> zoneListViewModel = new List<ZoneListViewModel>();

            if (postinglevel == "Z")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();



            }
            else if (postinglevel == "D")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();

            }
            else if (postinglevel == "R")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();

            }
            else if (postinglevel == "A")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();

            }
            else if (postinglevel == "T")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();

            }
            else
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();



            }

            MIOVisitReportViewModel model = new MIOVisitReportViewModel
            {
                zoneListViewModels = zoneListViewModel,
                //aspNetUsersViewModels = await userInfoes.GetAllUserInfo(),
                //userInfoViewModels = await userInfoes.GetUserInfoViewModel()

            };
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> MIOVisitTracker()
        {
            string userName = HttpContext.User.Identity.Name;
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == userName).ToList();
            ViewBag.employee = employee.FirstOrDefault();

            var postinglevel = employee.FirstOrDefault()?.POSTING_LOCATION;
            ViewBag.postinglevel = postinglevel;

            IEnumerable<ZoneListViewModel> zoneListViewModel = new List<ZoneListViewModel>();

            if (postinglevel == "Z")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();



            }
            else if (postinglevel == "D")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();

            }
            else if (postinglevel == "R")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();

            }
            else if (postinglevel == "A")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();

            }
            else if (postinglevel == "T")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();

            }
            else
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();



            }

            MIOVisitReportViewModel model = new MIOVisitReportViewModel
            {
                zoneListViewModels = zoneListViewModel,
                //aspNetUsersViewModels = await userInfoes.GetAllUserInfo(),
                //userInfoViewModels = await userInfoes.GetUserInfoViewModel()

            };
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> CurrentLocationTracker()
        {
            string userName = HttpContext.User.Identity.Name;
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == userName).ToList();
            ViewBag.employee = employee.FirstOrDefault();
            var postinglevel = employee.FirstOrDefault()?.POSTING_LOCATION;
            ViewBag.postinglevel = postinglevel;
            IEnumerable<ZoneListViewModel> zoneListViewModel = new List<ZoneListViewModel>();

            if (postinglevel == "Z")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();



            }
            else if (postinglevel == "D")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();

            }
            else if (postinglevel == "R")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();

            }
            else if (postinglevel == "A")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();

            }
            else if (postinglevel == "T")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();

            }
            else
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();



            }

            MIOVisitReportViewModel model = new MIOVisitReportViewModel
            {
                zoneListViewModels = zoneListViewModel,
                //aspNetUsersViewModels = await userInfoes.GetAllUserInfo(),
                //userInfoViewModels = await userInfoes.GetUserInfoViewModel()

            };
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> MIOVisitReportPDF(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, DateTime FromDate, DateTime ToDate)
        {
            var Zone = await userInfoes.ZoneListViewModels();
            var depot = await userInfoes.DepoListViewModels();
            var Area = await userInfoes.AreaListViewModels();
            var Region = await userInfoes.RegionListViewModels();
            var Territory = await userInfoes.TeritoryListViewModels();
            var Emp = await employeeService.GetEmployeeLoadViewModels();


            ViewBag.Name = Emp.Where(x => x.employeeNo == EmpCode).Select(x => x.fullName).FirstOrDefault();
            if (ViewBag.Name == "" || ViewBag.Name == null)
            {
                ViewBag.Name = "ALL";
            }
            //ViewBag.Zone = Zone.Where(x => x.Code == ZoneCode).Select(x => x.Name).FirstOrDefault();
            //ViewBag.Depot = depot.Where(x => x.Code == DepotCode).Select(x => x.Name).FirstOrDefault();
            //ViewBag.Region = Region.Where(x => x.Code == RegionCode).Select(x => x.Name).FirstOrDefault();
            //ViewBag.Area = Area.Where(x => x.Code == AreaCode).Select(x => x.Name).FirstOrDefault();
            //ViewBag.Territoy = Territory.Where(x => x.Code == TerritoryCode).Select(x => x.Name).FirstOrDefault();
            //ViewBag.Market = market.Where(x => x.Code == MarketCode).Select(x => x.Name).FirstOrDefault();
            //if (ViewBag.Market == "" || ViewBag.Market == null)
            //{
            //    ViewBag.Market = "ALL";
            //}
            ViewBag.Zone = Zone.Where(x => x.Code == ZoneCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Zone == "" || ViewBag.Zone == null)
            {
                ViewBag.Zone = "ALL";
            }
            ViewBag.Depot = depot.Where(x => x.Code == DepotCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Depot == "" || ViewBag.Depot == null)
            {
                ViewBag.Depot = "ALL";
            }
            ViewBag.Region = Region.Where(x => x.Code == RegionCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Region == "" || ViewBag.Region == null)
            {
                ViewBag.Region = "ALL";
            }
            ViewBag.Area = Area.Where(x => x.Code == AreaCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Area == "" || ViewBag.Area == null)
            {
                ViewBag.Area = "ALL";
            }
            ViewBag.Territoy = Territory.Where(x => x.Code == TerritoryCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Territoy == "" || ViewBag.Territoy == null)
            {
                ViewBag.Territoy = "ALL";
            }

            ViewBag.FromDate = Convert.ToDateTime(FromDate).ToString("dd-MM-yyyy");
            ViewBag.ToDate = Convert.ToDateTime(ToDate).ToString("dd-MM-yyyy");
            MIOVisitReportViewModel model = new MIOVisitReportViewModel
            {
                visitReportDoctorViewModels = await _chemistScheduleService.VisitReportDoctorViewModels(ZoneCode, DepotCode, RegionCode, AreaCode, TerritoryCode, EmpCode, Convert.ToDateTime(FromDate).ToString("yyyyMMdd"), Convert.ToDateTime(ToDate).ToString("yyyyMMdd")),
                visitReportChemistViewModels = await _chemistScheduleService.VisitReportChemistViewModels(ZoneCode, DepotCode, RegionCode, AreaCode, TerritoryCode, EmpCode, Convert.ToDateTime(FromDate).ToString("yyyyMMdd"), Convert.ToDateTime(ToDate).ToString("yyyyMMdd"))
            };


            return View(model);
        }

        [HttpGet("getMIODoctorVisitReport")]
        public async Task<IActionResult> getMIODoctorVisitReport(string ZoneId, string DepoId, string RegionId, string AreaId, string TerritoryID, string EmpCode, DateTime fromDate, DateTime toDate)
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
            //string fd = fromDate.ToString("dd-MMM-yyyy");
            //string td = toDate.ToString("dd-MMM-yyyy");
            var datajson = await _chemistScheduleService.GetMIODoctorVisitReport(ZoneId, DepoId, RegionId, AreaId, TerritoryID
                , EmpCode, Convert.ToDateTime(fromDate).ToString("yyyyMMdd"), Convert.ToDateTime(toDate).ToString("yyyyMMdd"));
            var jwt = await Tokens.getData(JsonDataManipulator.GetImagePathToImageFileRepeat(datajson).data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);

        }


        [HttpGet("getChemistVisitReport")]
        public async Task<IActionResult> getChemistVisitReport(string ZoneId, string DepoId, string RegionId, string AreaId, string TerritoryID, string EmpCode, DateTime fromDate, DateTime toDate)
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
            var datajson = await _chemistScheduleService.GetChemistVisitReport(ZoneId, DepoId, RegionId, AreaId, TerritoryID
                , EmpCode, Convert.ToDateTime(fromDate).ToString("yyyyMMdd"), Convert.ToDateTime(toDate).ToString("yyyyMMdd"));
            var jwt = await Tokens.getData(JsonDataManipulator.GetImagePathToImageFileRepeat(datajson).data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("getChemistWiseVisitReport")]
        public async Task<IActionResult> getChemistWiseVisitReport(string ZoneId, string DepoId, string RegionId, string AreaId, string TerritoryID,string MarketName,int? ChemistId, DateTime fromDate, DateTime toDate)
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
            
            var datajson = await _chemistScheduleService.GetChemistWiseVisitReport(ZoneId, DepoId, RegionId, AreaId, TerritoryID
                , MarketName, ChemistId, Convert.ToDateTime(fromDate).ToString("yyyyMMdd"), Convert.ToDateTime(toDate).ToString("yyyyMMdd"));
            var jwt = await Tokens.getData(JsonDataManipulator.GetImagePathToImageFileRepeat(datajson).data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);

        }

        [HttpGet("getDoctorWiseVisitReport")]
        public async Task<IActionResult> getDoctorWiseVisitReport(string ZoneId, string DepoId, string RegionId, string AreaId, string TerritoryID, string MarketName, string EmpCode, DateTime fromDate, DateTime toDate)
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
            var datajson = await _chemistScheduleService.GetDoctorWiseVisitReport(ZoneId, DepoId, RegionId, AreaId, TerritoryID
                , MarketName, EmpCode, Convert.ToDateTime(fromDate).ToString("yyyyMMdd"), Convert.ToDateTime(toDate).ToString("yyyyMMdd"));
            var jwt = await Tokens.getData(JsonDataManipulator.GetImagePathToImageFileRepeat(datajson).data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);

        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> EmployeeWiseVisitReportPDF(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, DateTime FromDate, DateTime ToDate)
        {
            var Zone = await userInfoes.ZoneListViewModels();
            var depot = await userInfoes.DepoListViewModels();
            var Area = await userInfoes.AreaListViewModels();
            var Region = await userInfoes.RegionListViewModels();
            var Territory = await userInfoes.TeritoryListViewModels();
            var Emp = await employeeService.GetEmployeeLoadViewModels();


            ViewBag.Name = Emp.Where(x => x.employeeNo == EmpCode).Select(x => x.fullName).FirstOrDefault();
            if (ViewBag.Name == "" || ViewBag.Name == null)
            {
                ViewBag.Name = "ALL";
            }
            //ViewBag.Zone = Zone.Where(x => x.Code == ZoneCode).Select(x => x.Name).FirstOrDefault();
            //ViewBag.Depot = depot.Where(x => x.Code == DepotCode).Select(x => x.Name).FirstOrDefault();
            //ViewBag.Region = Region.Where(x => x.Code == RegionCode).Select(x => x.Name).FirstOrDefault();
            //ViewBag.Area = Area.Where(x => x.Code == AreaCode).Select(x => x.Name).FirstOrDefault();
            //ViewBag.Territoy = Territory.Where(x => x.Code == TerritoryCode).Select(x => x.Name).FirstOrDefault();
            //ViewBag.Market = market.Where(x => x.Code == MarketCode).Select(x => x.Name).FirstOrDefault();
            //if (ViewBag.Market == "" || ViewBag.Market == null)
            //{
            //    ViewBag.Market = "ALL";
            //}
            ViewBag.Zone = Zone.Where(x => x.Code == ZoneCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Zone == "" || ViewBag.Zone == null)
            {
                ViewBag.Zone = "ALL";
            }
            ViewBag.Depot = depot.Where(x => x.Code == DepotCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Depot == "" || ViewBag.Depot == null)
            {
                ViewBag.Depot = "ALL";
            }
            ViewBag.Region = Region.Where(x => x.Code == RegionCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Region == "" || ViewBag.Region == null)
            {
                ViewBag.Region = "ALL";
            }
            ViewBag.Area = Area.Where(x => x.Code == AreaCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Area == "" || ViewBag.Area == null)
            {
                ViewBag.Area = "ALL";
            }
            ViewBag.Territoy = Territory.Where(x => x.Code == TerritoryCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Territoy == "" || ViewBag.Territoy == null)
            {
                ViewBag.Territoy = "ALL";
            }

            ViewBag.FromDate = Convert.ToDateTime(FromDate).ToString("dd-MM-yyyy");
            ViewBag.ToDate = Convert.ToDateTime(ToDate).ToString("dd-MM-yyyy");
            ChemistDoctorVisitReportViewModel model = new ChemistDoctorVisitReportViewModel
            {
                visitReportEmployeeViewModels = await _chemistScheduleService.VisitReportEmployeeViewModels(ZoneCode, DepotCode, RegionCode, AreaCode, TerritoryCode, EmpCode, Convert.ToDateTime(FromDate).ToString("yyyyMMdd"), Convert.ToDateTime(ToDate).ToString("yyyyMMdd")),

            };


            return View(model);
        }


        [AllowAnonymous]
        public IActionResult EmployeeWiseVisitReportPDFAction(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, DateTime FromDate, DateTime ToDate)
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Schedule/Report/EmployeeWiseVisitReportPDF?ZoneCode=" + ZoneCode + "&DepotCode=" + DepotCode + "&RegionCode=" + RegionCode + "&AreaCode=" + AreaCode + "&TerritoryCode=" + TerritoryCode + "&EmpCode=" + EmpCode + "&fromDate=" + FromDate + "&toDate=" + ToDate;

            string fileName;
            string status = myPDF.GeneratePDF(out fileName, url);

            // string status = myPDF.GeneratePDF(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }
        [AllowAnonymous]
        public IActionResult MIOVisitReportPDFAction(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, DateTime FromDate, DateTime ToDate)
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Schedule/Report/MIOVisitReportPDF?ZoneCode=" + ZoneCode + "&DepotCode=" + DepotCode + "&RegionCode=" + RegionCode + "&AreaCode=" + AreaCode + "&TerritoryCode=" + TerritoryCode + "&EmpCode=" + EmpCode + "&fromDate=" + FromDate + "&toDate=" + ToDate;

            string fileName;
            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            // string status = myPDF.GeneratePDF(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }

        [HttpGet("GetTSOAttendenceReport")]// MIO | TSO | Empployee MIOAttendenceReportLoad
        public async Task<IActionResult> GetTSOAttendenceReport(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, string fromDate, string toDate)
        {
            #region comments

            //var Zone = await userInfoes.ZoneListViewModels();
            //var depot = await userInfoes.DepoListViewModels();
            //var Area = await userInfoes.AreaListViewModels();
            //var Region = await userInfoes.RegionListViewModels();
            //var Territory = await userInfoes.TeritoryListViewModels();
            //var Emp = await employeeService.GetEmployeeLoadViewModels();


            //ViewBag.Name = Emp.Where(x => x.EMP_ID == EmpCode).Select(x => x.EMPLOYEE_NAME).FirstOrDefault();
            //if (ViewBag.Name == "" || ViewBag.Name == null)
            //{
            //    ViewBag.Name = "ALL";
            //}

            //ViewBag.Zone = Zone.Where(x => x.Code == ZoneCode).Select(x => x.Name).FirstOrDefault();
            //if (ViewBag.Zone == "" || ViewBag.Zone == null)
            //{
            //    ViewBag.Zone = "ALL";
            //}
            //ViewBag.Depot = depot.Where(x => x.Code == DepotCode).Select(x => x.Name).FirstOrDefault();
            //if (ViewBag.Depot == "" || ViewBag.Depot == null)
            //{
            //    ViewBag.Depot = "ALL";
            //}
            //ViewBag.Region = Region.Where(x => x.Code == RegionCode).Select(x => x.Name).FirstOrDefault();
            //if (ViewBag.Region == "" || ViewBag.Region == null)
            //{
            //    ViewBag.Region = "ALL";
            //}
            //ViewBag.Area = Area.Where(x => x.Code == AreaCode).Select(x => x.Name).FirstOrDefault();
            //if (ViewBag.Area == "" || ViewBag.Area == null)
            //{
            //    ViewBag.Area = "ALL";
            //}
            //ViewBag.Territoy = Territory.Where(x => x.Code == TerritoryCode).Select(x => x.Name).FirstOrDefault();
            //if (ViewBag.Territoy == "" || ViewBag.Territoy == null)
            //{
            //    ViewBag.Territoy = "ALL";
            //}

            //ViewBag.FromDate = Convert.ToDateTime(fromDate).ToString("dd-MM-yyyy");
            //ViewBag.ToDate = Convert.ToDateTime(toDate).ToString("dd-MM-yyyy");
            #endregion

            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var res = await _chemistScheduleService.AttendenceReportViewModels(ZoneCode, DepotCode, RegionCode, AreaCode, TerritoryCode, EmpCode, Convert.ToDateTime(fromDate).ToString("yyyyMMdd"), Convert.ToDateTime(toDate).ToString("yyyyMMdd"));

            //return Json(res);
            var jwt = await Tokens.ObjToJson(res);
            return new OkObjectResult(jwt);
        }


        [HttpGet("GetEmployeeDataForMessage")]// MIO | TSO | Empployee MIOAttendenceReportLoad
        public async Task<IActionResult> GetEmployeeDataForMessage(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode)
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
            var jsonToken = handler.ReadToken(stream);

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


            var res = await _chemistScheduleService.getEmployeeDataForMessage(ZoneCode, DepotCode, RegionCode, AreaCode, TerritoryCode, EmpCode);

            var jwt = await Tokens.getData(res.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);

        }



        [HttpGet("GetLocationAll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLocationAll(string Type, string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var locationdata = await userInfoes.MIOCurrentLocationNNViewModels();
            var allemp = await employeeService.GetEmployeeLoadViewModels();

            #region Comments By MOSTAFA

            //if (ZoneCode == "NoData")
            //{
            //    ZoneCode = "";
            //}
            //else
            //{
            //    allemp = allemp.Where(x => x.ZONE_CODE == ZoneCode).ToList();
            //}
            //if (DepotCode == "NoData")
            //{
            //    DepotCode = "";
            //}
            //else
            //{
            //    allemp = allemp.Where(x => x.DEPOT_CODE == DepotCode).ToList();
            //}
            //if (RegionCode == "NoData")
            //{
            //    RegionCode = "";
            //}
            //else
            //{
            //    allemp = allemp.Where(x => x.REGION_CODE == RegionCode).ToList();
            //}
            //if (AreaCode == "NoData")
            //{
            //    AreaCode = "";
            //}
            //else
            //{
            //    allemp = allemp.Where(x => x.AREA_CODE == AreaCode).ToList();
            //}
            //if (TerritoryCode == "NoData")
            //{
            //    TerritoryCode = "";
            //}
            //else
            //{
            //    allemp = allemp.Where(x => x.TERRITORY_CODE == TerritoryCode).ToList();
            //}
            //if (EmpCode == "NoData")
            //{
            //    EmpCode = "";

            //}
            //if (Type == "Z")
            //{
            //    if (ZoneCode == "")
            //    {
            //        allemp = allemp.Where(x => x.POSTING_LOCATION == "Z").ToList();
            //    }
            //    else
            //    {
            //        allemp = allemp.Where(x => x.ZONE_CODE == ZoneCode && x.POSTING_LOCATION == "Z").ToList();
            //    }
            //}
            //else if (Type == "D")
            //{
            //    if (DepotCode == "")
            //    {
            //        allemp = allemp.Where(x => x.POSTING_LOCATION == "D").ToList();
            //    }
            //    else
            //    {
            //        allemp = allemp.Where(x => x.DEPOT_CODE == DepotCode && x.POSTING_LOCATION == "D").ToList();
            //    }
            //}
            //else if (Type == "R")
            //{
            //    if (RegionCode == "")
            //    {
            //        allemp = allemp.Where(x => x.POSTING_LOCATION == "R").ToList();
            //    }
            //    else
            //    {
            //        allemp = allemp.Where(x => x.REGION_CODE == RegionCode && x.POSTING_LOCATION == "R").ToList();
            //    }
            //}
            //else if (Type == "A")
            //{
            //    if (AreaCode == "")
            //    {
            //        allemp = allemp.Where(x => x.POSTING_LOCATION == "A").ToList();
            //    }
            //    else
            //    {
            //        allemp = allemp.Where(x => x.AREA_CODE == AreaCode && x.POSTING_LOCATION == "A").ToList();
            //    }
            //}
            //else if (Type == "T")
            //{
            //    if (TerritoryCode == "")
            //    {
            //        allemp = allemp.Where(x => x.POSTING_LOCATION == "T").ToList();
            //    }
            //    else
            //    {
            //        allemp = allemp.Where(x => x.TERRITORY_CODE == TerritoryCode && x.POSTING_LOCATION == "T").ToList();
            //    }
            //}
            //else
            //{
            //    allemp = allemp.ToList();
            //}
            //if (EmpCode != "")
            //{
            //    allemp = allemp.Where(x => x.employeeNo == EmpCode).ToList();
            //}

            #endregion

            if (!string.IsNullOrWhiteSpace(ZoneCode))
                allemp = allemp.Where(x => x.ZONE_CODE == ZoneCode).ToList();

            if (!string.IsNullOrWhiteSpace(DepotCode))
                allemp = allemp.Where(x => x.DEPOT_CODE == DepotCode).ToList();

            if (!string.IsNullOrWhiteSpace(RegionCode))
                allemp = allemp.Where(x => x.REGION_CODE == RegionCode).ToList();

            if (!string.IsNullOrWhiteSpace(AreaCode))
                allemp = allemp.Where(x => x.TERRITORY_CODE == TerritoryCode).ToList();

            if (!string.IsNullOrWhiteSpace(EmpCode))
                allemp = allemp.Where(x => x.employeeNo.ToString() == EmpCode).ToList();


            List<string> emplist = allemp.Select(x => x.employeeNo).ToList();
            locationdata = locationdata.Where(x => emplist.Contains(x.MIOCode)).ToList();
            //ViewBag.count = locationdata.Count();
            //return Json(locationdata);

            var jwt = await Tokens.ObjToJson(locationdata);
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetLocationMIO")]
        public async Task<IActionResult> GetLocationMIO(string Type, string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, string Date)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var allemp = await employeeService.GetEmployeeLoadViewModels();
            if (Type == "Z")
            {
                if (string.IsNullOrWhiteSpace(ZoneCode))// == "")
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
                if (string.IsNullOrWhiteSpace(DepotCode))// == "")
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
                if (string.IsNullOrWhiteSpace(RegionCode))// == "")
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
                if (string.IsNullOrWhiteSpace(AreaCode))// == "")
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
                if (string.IsNullOrWhiteSpace(TerritoryCode))// == "")
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
            if (!string.IsNullOrWhiteSpace(EmpCode))// != "")
            {
                allemp = allemp.Where(x => x.employeeNo == EmpCode).ToList();
            }

            var locationdata = await userInfoes.MIOCurrentLocationViewModelsByMIO2(ZoneCode, DepotCode, RegionCode, AreaCode, TerritoryCode, EmpCode, Convert.ToDateTime(Date).ToString("yyyyMMdd"));
            
            List<string> emplist = allemp.Select(x => x.employeeNo).ToList();
            
            locationdata = locationdata.Where(x => emplist.Contains(x.MIOCode)).ToList();

            var jwt = await Tokens.ObjToJson(locationdata);
            return new OkObjectResult(jwt);
        }
        [HttpGet("GetLocationMIO_Map")]
        public async Task<IActionResult> GetLocationMIO_Map(string Type, string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, string Date)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);
            var allemp = await employeeService.GetEmployeeLoadViewModels();
            
            if (Type == "Z")
            {
                if (string.IsNullOrWhiteSpace(ZoneCode))// == "")
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
                if (string.IsNullOrWhiteSpace(DepotCode))// == "")
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
                if (string.IsNullOrWhiteSpace(RegionCode))// == "")
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
                if (string.IsNullOrWhiteSpace(AreaCode))// == "")
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
                if (string.IsNullOrWhiteSpace(TerritoryCode))// == "")
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
            if (!string.IsNullOrWhiteSpace(EmpCode))// != "")
            {
                allemp = allemp.Where(x => x.employeeNo == EmpCode).ToList();
            }
            var locationdata = await userInfoes.MIOCurrentLocationViewNModelsByMIO(ZoneCode, DepotCode, RegionCode, AreaCode, TerritoryCode, EmpCode, Convert.ToDateTime(Date).ToString("yyyyMMdd"));
            List<string> emplist = allemp.Select(x => x.employeeNo).ToList();
            locationdata = locationdata.Where(x => emplist.Contains(x.MIOCode)).ToList();

            var jwt = await Tokens.ObjToJson(locationdata);
            return new OkObjectResult(jwt);
        }

        #endregion

        #region FFT Report

        [HttpGet("GetFFTSalesReport")]
        public async Task<IActionResult> GetFFTSalesReport(string ZONE_CODE, string DEPOT_CODE, string REGION_COE, string AREA_CODE, string TERRITORY_CODE, string EmpId, DateTime FDate, DateTime TDate, int StoreId, int SalesInvoiceId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var locationdata = await reportService.GetSalesReport(ZONE_CODE, DEPOT_CODE, REGION_COE, AREA_CODE, TERRITORY_CODE, EmpId, FDate, TDate, StoreId, SalesInvoiceId);

            var jwt = await Tokens.GetJwt(locationdata.data);
            return new OkObjectResult(jwt);
        }
        
        [HttpGet("GetEmp_DoctorPromotionalItemReportData")]
        public async Task<IActionResult> GetEmp_DoctorPromotionalItemReportData(string ZONE_CODE, string DEPOT_CODE, string REGION_COE, string AREA_CODE, string TERRITORY_CODE, string EmpId, string DoctorId, DateTime FDate, DateTime TDate)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var locationdata = await reportService.GetEmp_DoctorPromotionalItemReportData(ZONE_CODE, DEPOT_CODE, REGION_COE, AREA_CODE, TERRITORY_CODE, EmpId, DoctorId, FDate, TDate);

            var jwt = await Tokens.GetJwt(locationdata.data);
            return new OkObjectResult(jwt);
        }

        #endregion

        #region Doctor Wise
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> DoctorWiseVisitReport()
        {
            string userName = HttpContext.User.Identity.Name;
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == userName).ToList();
            var postinglevel = employee.FirstOrDefault()?.POSTING_LOCATION;

            IEnumerable<ZoneListViewModel> zoneListViewModel = new List<ZoneListViewModel>();

            if (postinglevel == "Z")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();



            }
            else if (postinglevel == "D")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();

            }
            else if (postinglevel == "R")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();

            }
            else if (postinglevel == "A")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();

            }
            else if (postinglevel == "T")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();

            }
            else
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();



            }

            ChemistDoctorVisitReportViewModel model = new ChemistDoctorVisitReportViewModel
            {
                zoneListViewModels = zoneListViewModel,
                //aspNetUsersViewModels = await userInfoes.GetAllUserInfo(),
                //userInfoViewModels = await userInfoes.GetUserInfoViewModel()

            };
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> DoctorWiseVisitReportPDF(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string MarketCode, int Id, DateTime FromDate, DateTime ToDate)
        {

            var Zone = await userInfoes.ZoneListViewModels();
            var depot = await userInfoes.DepoListViewModels();
            var Area = await userInfoes.AreaListViewModels();
            var Region = await userInfoes.RegionListViewModels();
            var Territory = await userInfoes.TeritoryListViewModels();
            var market = await userInfoes.MarketListViewModels();


            //ViewBag.Market = market.Where(x => x.Code == MarketCode).Select(x => x.Name).FirstOrDefault();
            //ViewBag.Zone = Zone.Where(x => x.Code == ZoneCode).Select(x => x.Name).FirstOrDefault();
            //ViewBag.Depot = depot.Where(x => x.Code == DepotCode).Select(x => x.Name).FirstOrDefault();
            //ViewBag.Region = Region.Where(x => x.Code == RegionCode).Select(x => x.Name).FirstOrDefault();
            //ViewBag.Area = Area.Where(x => x.Code == AreaCode).Select(x => x.Name).FirstOrDefault();
            //ViewBag.Territoy = Territory.Where(x => x.Code == TerritoryCode).Select(x => x.Name).FirstOrDefault();
            ViewBag.Market = market.Where(x => x.Code == MarketCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Market == "" || ViewBag.Market == null)
            {
                ViewBag.Market = "ALL";
            }
            ViewBag.Zone = Zone.Where(x => x.Code == ZoneCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Zone == "" || ViewBag.Zone == null)
            {
                ViewBag.Zone = "ALL";
            }
            ViewBag.Depot = depot.Where(x => x.Code == DepotCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Depot == "" || ViewBag.Depot == null)
            {
                ViewBag.Depot = "ALL";
            }
            ViewBag.Region = Region.Where(x => x.Code == RegionCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Region == "" || ViewBag.Region == null)
            {
                ViewBag.Region = "ALL";
            }
            ViewBag.Area = Area.Where(x => x.Code == AreaCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Area == "" || ViewBag.Area == null)
            {
                ViewBag.Area = "ALL";
            }
            ViewBag.Territoy = Territory.Where(x => x.Code == TerritoryCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Territoy == "" || ViewBag.Territoy == null)
            {
                ViewBag.Territoy = "ALL";
            }
            var userdata = await _doctorService.GetCmnDoctorById(Id);
            if (userdata == null)
            {
                ViewBag.Name = "ALL";
            }
            else
            {
                ViewBag.Name = userdata?.DoctorName + "-(" + userdata?.DoctorNo + ")";
            }

            ViewBag.FromDate = Convert.ToDateTime(FromDate).ToString("dd-MM-yyyy");
            ViewBag.ToDate = Convert.ToDateTime(ToDate).ToString("dd-MM-yyyy");
            ChemistDoctorVisitReportViewModel model = new ChemistDoctorVisitReportViewModel
            {
                doctorWiseVisitReportViewModels = await _chemistScheduleService.DoctorWiseVisitReportViewModels(ZoneCode, DepotCode, RegionCode, AreaCode, TerritoryCode, MarketCode, Id, Convert.ToDateTime(FromDate).ToString("yyyyMMdd"), Convert.ToDateTime(ToDate).ToString("yyyyMMdd")),

            };


            return View(model);
        }
        [AllowAnonymous]
        public IActionResult DoctorWiseVisitReportPDFAction(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string MarketCode, int Id, DateTime FromDate, DateTime ToDate)
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Schedule/Report/DoctorWiseVisitReportPDF?ZoneCode=" + ZoneCode + "&DepotCode=" + DepotCode + "&RegionCode=" + RegionCode + "&AreaCode=" + AreaCode + "&TerritoryCode=" + TerritoryCode + "&MarketCode=" + MarketCode + "&Id=" + Id + "&fromDate=" + FromDate + "&toDate=" + ToDate;

            string fileName;
            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            // string status = myPDF.GeneratePDF(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }
        #endregion

        #region Chemist Wise
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> ChemistWiseVisitReport()
        {
            string userName = HttpContext.User.Identity.Name;
            var employee = await employeeService.GetEmployeeLoadViewModels();
            employee = employee.Where(x => x.employeeNo == userName).ToList();
            var postinglevel = employee.FirstOrDefault()?.POSTING_LOCATION;

            IEnumerable<ZoneListViewModel> zoneListViewModel = new List<ZoneListViewModel>();

            if (postinglevel == "Z")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();



            }
            else if (postinglevel == "D")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();

            }
            else if (postinglevel == "R")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();

            }
            else if (postinglevel == "A")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();

            }
            else if (postinglevel == "T")
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();
                zoneListViewModel = zoneListViewModel.Where(x => x.Code == employee.FirstOrDefault().ZONE_CODE).ToList();

            }
            else
            {
                zoneListViewModel = await userInfoes.ZoneListViewModels();



            }

            ChemistDoctorVisitReportViewModel model = new ChemistDoctorVisitReportViewModel
            {
                zoneListViewModels = zoneListViewModel,
                //aspNetUsersViewModels = await userInfoes.GetAllUserInfo(),
                //userInfoViewModels = await userInfoes.GetUserInfoViewModel()

            };
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ChemistWiseVisitReportPDF(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string MarketCode, int Id, DateTime FromDate, DateTime ToDate)
        {
            var Zone = await userInfoes.ZoneListViewModels();
            var depot = await userInfoes.DepoListViewModels();
            var Area = await userInfoes.AreaListViewModels();
            var Region = await userInfoes.RegionListViewModels();
            var Territory = await userInfoes.TeritoryListViewModels();
            var market = await userInfoes.MarketListViewModels();


            ViewBag.Market = market.Where(x => x.Code == MarketCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Market == "" || ViewBag.Market == null)
            {
                ViewBag.Market = "ALL";
            }
            ViewBag.Zone = Zone.Where(x => x.Code == ZoneCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Zone == "" || ViewBag.Zone == null)
            {
                ViewBag.Zone = "ALL";
            }
            ViewBag.Depot = depot.Where(x => x.Code == DepotCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Depot == "" || ViewBag.Depot == null)
            {
                ViewBag.Depot = "ALL";
            }
            ViewBag.Region = Region.Where(x => x.Code == RegionCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Region == "" || ViewBag.Region == null)
            {
                ViewBag.Region = "ALL";
            }
            ViewBag.Area = Area.Where(x => x.Code == AreaCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Area == "" || ViewBag.Area == null)
            {
                ViewBag.Area = "ALL";
            }
            ViewBag.Territoy = Territory.Where(x => x.Code == TerritoryCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Territoy == "" || ViewBag.Territoy == null)
            {
                ViewBag.Territoy = "ALL";
            }
            var userdata = await _chemistService.GetCmnChemistbyId(Id);
            if (userdata == null)
            {
                ViewBag.Name = "ALL";
            }
            else
            {
                ViewBag.Name = userdata?.ChemistName + "-(" + userdata?.ChemistNo + ")";
            }

            ViewBag.FromDate = Convert.ToDateTime(FromDate).ToString("dd-MM-yyyy");
            ViewBag.ToDate = Convert.ToDateTime(ToDate).ToString("dd-MM-yyyy");
            ChemistDoctorVisitReportViewModel model = new ChemistDoctorVisitReportViewModel
            {
                chemistWiseVisitReportViewModels = await _chemistScheduleService.ChemistWiseVisitReportViewModels(ZoneCode, DepotCode, RegionCode, AreaCode, TerritoryCode, MarketCode, Id, Convert.ToDateTime(FromDate).ToString("yyyyMMdd"), Convert.ToDateTime(ToDate).ToString("yyyyMMdd")),

            };


            return View(model);
        }
        [AllowAnonymous]
        public IActionResult ChemistWiseVisitReportPDFAction(string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string MarketCode, int Id, DateTime FromDate, DateTime ToDate)
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Schedule/Report/ChemistWiseVisitReportPDF?ZoneCode=" + ZoneCode + "&DepotCode=" + DepotCode + "&RegionCode=" + RegionCode + "&AreaCode=" + AreaCode + "&TerritoryCode=" + TerritoryCode + "&MarketCode=" + MarketCode + "&Id=" + Id + "&fromDate=" + FromDate + "&toDate=" + ToDate;

            string fileName;
            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            // string status = myPDF.GeneratePDF(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }
        #endregion

        #region tracking pdf

        [AllowAnonymous]
        public IActionResult CurrentLocationReportPDFAction(string Type, string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode)
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Schedule/Report/CurrentLocationReportPDF?Type=" + Type + "&ZoneCode=" + ZoneCode + "&DepotCode=" + DepotCode + "&RegionCode=" + RegionCode + "&AreaCode=" + AreaCode + "&TerritoryCode=" + TerritoryCode + "&EmpCode=" + EmpCode;

            string fileName;
            string status = myPDF.GeneratePDF(out fileName, url);

            // string status = myPDF.GeneratePDF(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> CurrentLocationReportPDF(string Type, string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode)
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
            var sdata = allemp.Where(x=>x.employeeNo==user.UserName).FirstOrDefault();
            allemp = allemp.Where(x => x.companyId == sdata.companyId);
            var locationdata = await userInfoes.MIOCurrentLocationDViewModels(user.UserName);
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
            if (EmpCode == "NoData")
            {
                EmpCode = "";

            }
            var Zone = await userInfoes.ZoneListViewModels();
            var depot = await userInfoes.DepoListViewModels();
            var Area = await userInfoes.AreaListViewModels();
            var Region = await userInfoes.RegionListViewModels();
            var Territory = await userInfoes.TeritoryListViewModels();
            var market = await userInfoes.MarketListViewModels();



            ViewBag.Zone = Zone.Where(x => x.Code == ZoneCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Zone == "" || ViewBag.Zone == null)
            {
                ViewBag.Zone = "ALL";
            }
            ViewBag.Depot = depot.Where(x => x.Code == DepotCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Depot == "" || ViewBag.Depot == null)
            {
                ViewBag.Depot = "ALL";
            }
            ViewBag.Region = Region.Where(x => x.Code == RegionCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Region == "" || ViewBag.Region == null)
            {
                ViewBag.Region = "ALL";
            }
            ViewBag.Area = Area.Where(x => x.Code == AreaCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Area == "" || ViewBag.Area == null)
            {
                ViewBag.Area = "ALL";
            }
            ViewBag.Territoy = Territory.Where(x => x.Code == TerritoryCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Territoy == "" || ViewBag.Territoy == null)
            {
                ViewBag.Territoy = "ALL";
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
                allemp = allemp.Where(x => x.employeeNo == EmpCode).ToList();
            }

            if (EmpCode == "")
            {
                ViewBag.Name = "ALL";
            }
            else
            {
                ViewBag.Name = allemp.FirstOrDefault().fullName;
            }
            List<string> emplist = allemp.Select(x => x.employeeNo).ToList();
            locationdata = locationdata.Where(x => emplist.Contains(x.MIOCode)).ToList();

            ChemistDoctorVisitReportViewModel model = new ChemistDoctorVisitReportViewModel
            {
                mIOCurrentLocationViewModels = locationdata,

            };


            return View(model);
        }


        [AllowAnonymous]
        public IActionResult RoadMapReportPDFAction(string Type, string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, DateTime Date)
        {
            string userName = HttpContext.User.Identity.Name;
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Schedule/Report/RoadMapReportPDF?Type=" + Type + "&ZoneCode=" + ZoneCode + "&DepotCode=" + DepotCode + "&RegionCode=" + RegionCode + "&AreaCode=" + AreaCode + "&TerritoryCode=" + TerritoryCode + "&EmpCode=" + EmpCode + "&Date=" + Date;

            string fileName;
            string status = myPDF.GeneratePDF(out fileName, url);

            // string status = myPDF.GeneratePDF(out fileName, url);

            FileName = fileName;
            if (status != "done")
            {
                return Content("<h1>" + status + "</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");

        }


        [AllowAnonymous]
        [HttpGet("RoadMapReportPDF")]
        public async Task<IActionResult> RoadMapReportPDF(string Type, string ZoneCode, string DepotCode, string RegionCode, string AreaCode, string TerritoryCode, string EmpCode, DateTime Date)
        {

            //var locationdata = await userInfoes.MIOCurrentLocationDViewModels();

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
            if (EmpCode == "NoData")
            {
                EmpCode = "";

            }

            var Zone = await userInfoes.ZoneListViewModels();
            var depot = await userInfoes.DepoListViewModels();
            var Area = await userInfoes.AreaListViewModels();
            var Region = await userInfoes.RegionListViewModels();
            var Territory = await userInfoes.TeritoryListViewModels();
            var market = await userInfoes.MarketListViewModels();



            ViewBag.Zone = Zone.Where(x => x.Code == ZoneCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Zone == "" || ViewBag.Zone == null)
            {
                ViewBag.Zone = "ALL";
            }
            ViewBag.Depot = depot.Where(x => x.Code == DepotCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Depot == "" || ViewBag.Depot == null)
            {
                ViewBag.Depot = "ALL";
            }
            ViewBag.Region = Region.Where(x => x.Code == RegionCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Region == "" || ViewBag.Region == null)
            {
                ViewBag.Region = "ALL";
            }
            ViewBag.Area = Area.Where(x => x.Code == AreaCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Area == "" || ViewBag.Area == null)
            {
                ViewBag.Area = "ALL";
            }
            ViewBag.Territoy = Territory.Where(x => x.Code == TerritoryCode).Select(x => x.Name).FirstOrDefault();
            if (ViewBag.Territoy == "" || ViewBag.Territoy == null)
            {
                ViewBag.Territoy = "ALL";
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
                allemp = allemp.Where(x => x.employeeNo == EmpCode).ToList();
            }

            if (EmpCode == "")
            {
                ViewBag.Name = "ALL";
            }
            else
            {
                ViewBag.Name = allemp.FirstOrDefault().fullName;
            }
            var locationdata = await userInfoes.MIOCurrentLocationViewModelsByMIO(ZoneCode, DepotCode, RegionCode, AreaCode, TerritoryCode, EmpCode, Convert.ToDateTime(Date).ToString("yyyyMMdd"));
            List<string> emplist = allemp.Select(x => x.employeeNo).ToList();
            locationdata = locationdata.Where(x => emplist.Contains(x.MIOCode)).ToList();

            ChemistDoctorVisitReportViewModel model = new ChemistDoctorVisitReportViewModel
            {
                mIOCurrentLocationViewModels = locationdata,

            };


            return View(model);
        }

        #endregion

        #region FFT Dashboard

        [HttpGet("GetFFTDashboardData")]
        public async Task<IActionResult> GetFFTDashboardData()
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
            List<string> emplist = employeeLoadViewModels.Select(x => x.employeeNo).ToList();
            //var locationdata = await userInfoes.MIOCurrentLocationDViewModels();
            //locationdata = locationdata.Where(x => emplist.Contains(x.MIOCode)).ToList();
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

            var logOutInfo = await userInfoes.GetNotLoginInfoDataViews();
            logOutInfo = logOutInfo.Where(x => emplist.Contains(x?.EMP_ID)).ToList();

            var logInInfo = await userInfoes.GetLoginInfoDataViews();
            logInInfo = logInInfo.Where(x => emplist.Contains(x?.EMP_ID)).ToList();

            var notLocation = await userInfoes.GetNotLocationInfoDataViews();
            notLocation = notLocation.Where(x => emplist.Contains(x?.EMP_ID)).ToList();

            var chemistWiseVisitReportViewModels = chemistwise;
            var doctorWiseVisitReportViewModels = doctorwise;
            var logOutData = logOutInfo;
            var logInData = logInInfo;

            var jwt = await Tokens.getFFTDashboardData(chemists.Count(), doctors.Count(),chemistWiseVisitReportViewModels.ToList(), doctorWiseVisitReportViewModels.ToList(), logOutData.ToList(), logInData.ToList(), notLocation.ToList(), new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);                      
        }

        [HttpGet("GetDailyAttendanceReport")]
        public async Task<IActionResult> GetDailyAttendanceReport()
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

            
            
            var chemists = await reportService.GetAM_MIOAttendenceReport();
          

            var jwt = await Tokens.getDailyattendenceData(chemists.data, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);
        }


        #endregion

        #region FFT  Dashboard Bar chart

        [HttpGet("GetFFTDashboardDataBarChart")]
        public async Task<IActionResult> GetFFTDashboardDataBarChart()
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
            
            var chemists = await _chemistService.GetAllCmnChemist();
            chemists = chemists.Where(x => teritoryListViewModels.Select(s => s.Code).ToList().Contains(x.TerritoryID)).ToList();             

            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var chemistwiseCurrentMonth = await _chemistScheduleService.ChemistWiseVisitReportDViewModels(0, Convert.ToDateTime(firstDayOfMonth).ToString("yyyyMMdd"), Convert.ToDateTime(lastDayOfMonth).ToString("yyyyMMdd"));
            chemistwiseCurrentMonth = chemistwiseCurrentMonth.Where(x => chemists.Select(s => s.ChemistID).ToList().Contains(x.ChemistID)).ToList();

            var data = (from pr in chemistwiseCurrentMonth
                        group pr by new { pr.date }
                        into grp
                        select new ChemistWiseVisitReportViewModel
                        {
                            date = grp.Key.date,
                            invoiceAmount = grp.Sum(x => x?.invoiceAmount),
                            collectionAmount = grp.Sum(x => x?.collectionAmount),
                        }).ToList();
            
            var jwt = await Tokens.getFFTDashboardDataBarChart(data.ToList(), new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);           
        }

        #endregion

        #region GetOutData

        [HttpGet("GetOutData")]
        public async Task<IActionResult> GetOutData()       
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

            var postinglevel = employee.FirstOrDefault()?.POSTING_LOCATION;
            IEnumerable<ZoneListViewModel> zoneListViewModel = new List<ZoneListViewModel>();
            IEnumerable<DepoListViewModel> depoListViewModels = new List<DepoListViewModel>();
            IEnumerable<RegionListViewModel> regionListViewModels = new List<RegionListViewModel>();
            IEnumerable<AreaListViewModel> areaListViewModels = new List<AreaListViewModel>();
            IEnumerable<TeritoryListViewModel> teritoryListViewModels = new List<TeritoryListViewModel>();
            IEnumerable<EmployeeViewModel> employeeLoadViewModels = new List<EmployeeViewModel>();
            if (postinglevel == "Z")
            {
               
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
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
            }
            List<string> emplist = employeeLoadViewModels.Select(x => x.employeeNo).ToList();
            var logOutInfo = await userInfoes.GetNotLoginInfoDataViews();
            logOutInfo = logOutInfo.Where(x => emplist.Contains(x?.EMP_ID)).ToList();
            var jwt = await Tokens.getLogInOutInfoData(logOutInfo.ToList(), new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        #endregion

        #region GetLoginData

        [HttpGet("GetLoginData")]
        public async Task<IActionResult> GetLoginData()
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

            var postinglevel = employee.FirstOrDefault()?.POSTING_LOCATION;
            IEnumerable<ZoneListViewModel> zoneListViewModel = new List<ZoneListViewModel>();
            IEnumerable<DepoListViewModel> depoListViewModels = new List<DepoListViewModel>();
            IEnumerable<RegionListViewModel> regionListViewModels = new List<RegionListViewModel>();
            IEnumerable<AreaListViewModel> areaListViewModels = new List<AreaListViewModel>();
            IEnumerable<TeritoryListViewModel> teritoryListViewModels = new List<TeritoryListViewModel>();
            IEnumerable<EmployeeViewModel> employeeLoadViewModels = new List<EmployeeViewModel>();
            if (postinglevel == "Z")
            {

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
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
            }
            List<string> emplist = employeeLoadViewModels.Select(x => x.employeeNo).ToList();
            var loginInfo = await userInfoes.GetLoginInfoDataViews();
            loginInfo = loginInfo.Where(x => emplist.Contains(x?.EMP_ID)).ToList();
            var jwt = await Tokens.getLogInOutInfoData(loginInfo.ToList(), new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        #endregion

        #region GetNotLocationData

        [HttpGet("GetNotLocationData")]
        public async Task<IActionResult> GetNotLocationData()
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

            var postinglevel = employee.FirstOrDefault()?.POSTING_LOCATION;
            IEnumerable<ZoneListViewModel> zoneListViewModel = new List<ZoneListViewModel>();
            IEnumerable<DepoListViewModel> depoListViewModels = new List<DepoListViewModel>();
            IEnumerable<RegionListViewModel> regionListViewModels = new List<RegionListViewModel>();
            IEnumerable<AreaListViewModel> areaListViewModels = new List<AreaListViewModel>();
            IEnumerable<TeritoryListViewModel> teritoryListViewModels = new List<TeritoryListViewModel>();
            IEnumerable<EmployeeViewModel> employeeLoadViewModels = new List<EmployeeViewModel>();
            if (postinglevel == "Z")
            {

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
                employeeLoadViewModels = await employeeService.GetEmployeeLoadViewModels();
            }
            List<string> emplist = employeeLoadViewModels.Select(x => x.employeeNo).ToList();
            var notLocation = await userInfoes.GetNotLocationInfoDataViews();
            notLocation = notLocation.Where(x => emplist.Contains(x?.EMP_ID)).ToList();
            var jwt = await Tokens.getLogInOutInfoData(notLocation.ToList(), new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #endregion


        #region Attendance Reports

        [HttpGet("GetMonthlyEmployeeAttendanceReportForFFM")]
        public async Task<IActionResult> GetMonthlyEmployeeAttendanceReportForFFM(DateTime fDate, DateTime tDate, string zoneCode, string depotCode, string regionCode, string areaCode, string territoryCode, string empCode)
        {
            if (!Authentication().Result) return new OkObjectResult(jwts);
            var datajson = await reportService.GetMonthlyEmployeeAttendanceReportForFFM(user.employeeId, fDate, tDate, zoneCode, depotCode, regionCode, areaCode, territoryCode, empCode);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }


        #endregion

        #region DCR Report
        [HttpGet("getDcrSummaryReport")]
        public async Task<IActionResult> getDcrSummaryReport(string zoneCode, string regionCode, string areaCode, string territoryCode, DateTime fromDate, DateTime toDate, string reportId)
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
            var datajson = await _chemistScheduleService.getDcrSummaryReport(user.employeeId, zoneCode, regionCode, areaCode, territoryCode,fromDate, toDate, reportId);
            var jwt = await Tokens.getData(datajson.data, datajson.data2, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
            //var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });

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
                string msg = "Invalid Token.";
                jwts = Tokens.GetFailedJwt(status, msg);//Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                jwts = Tokens.GetFailedJwt(status, msg);//Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return false;
            }
            return true;
            #endregion
        }
    }
}