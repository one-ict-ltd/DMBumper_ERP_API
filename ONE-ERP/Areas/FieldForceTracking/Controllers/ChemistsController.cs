using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.FieldForceTracking.Models;
using ONEERP.Data;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.FieldForceTracking.Interfaces;
using ONEERP.Helpers;

namespace ONEERP.Areas.FieldForceTracking.Controllers
{
    //[Area("MasterData")]
    [Route("api/[controller]")]
    public class ChemistsController : Controller
    {
        private readonly IChemistService _chemistService;
        private readonly IUserInfoes _userInfos;
        private ERPDbContext _db;
        public ChemistsController(IChemistService chemistService, IUserInfoes userInfoes, ERPDbContext db)
        {
            this._chemistService = chemistService;
            this._userInfos = userInfoes;
            this._db = db;
        }
        // GET: ChamistsController
        public async Task<ActionResult> Index()
        {
            return View(new ChemistListViewModel { Chemists = await _chemistService.GetAllCmnChemist() });
        }

        // GET: ChamistsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ChamistsController/Create
        public async Task<ActionResult> Create()
        {
            var data = new ChemistListViewModel
            {
                Users = await _userInfos.GetAllUserInfo(),
                Chemists = await _chemistService.GetAllCmnChemist(),
                Zones = await _userInfos.ZoneListViewModels(),
                Depos = null, //await _userInfos.DepoListViewModels(),
                Regions = null, //await _userInfos.RegionListViewModels(),
                Areas = null, //await _userInfos.AreaListViewModels(),
                Teritories = null, //await _userInfos.TeritoryListViewModels(),
                Markets = null, //await _userInfos.MarketListViewModels()
            };
            return View(data);
        }


        [HttpGet("getChemist")]
        public async Task<IActionResult> getChemist(int Id)
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
            var user = await _userInfos.GetUserBasicInfoesbyId(jti);

            if (user.token != uid && user != null)
            {
                bool status = false;
                string actionresult = "Invalid Token.";
                var jwts = await Tokens.changePasswordJwt(status, actionresult, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwts);
            }

            var datajson = await _chemistService.GetChemistList(Id);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        // GET: ChemistsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ChemistsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ChemistsController/Delete/5
        public JsonResult Delete(int id)
        {
            //var data = _chemistService.DeleteChemist(id);
            return Json("true");
        }

        // POST: ChemistsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetChemistById(int id)
        {
            return Json(await _chemistService.GetCmnChemistbyId(id));
        }

        [HttpGet]
        public async Task<ActionResult> GetDepo(int Id) 
        {
            return Json(await _userInfos.DepoListViewModels());
        }
    }
}
