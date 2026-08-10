using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ONEERP.Areas.Inventory.Models;
using ONEERP.Data.Entity;
using ONEERP.ERPService.AuthService.Interfaces;
using ONEERP.ERPServices.Inventory.Interfaces;
using ONEERP.Helpers;

namespace ONEERP.Areas.Inventory.Controllers
{
    [Route("api/[controller]")]
    public class DamageGoods : Controller
    {
        object jwts;
        ApplicationUser user;
        private readonly IUserInfoes userInfoes;
        private readonly IDamageGoodsService service;
        public DamageGoods(IUserInfoes _userInfoes, IDamageGoodsService _service)
        {
            userInfoes = _userInfoes;
            service = _service;
            jwts = new object();
            user = new ApplicationUser();
        }

        #region Damage Goods

        [HttpPost("SaveDamageGoods")]
        public async Task<IActionResult> SaveDamageGoods([FromBody] DamageGoodsViewModel model)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (model == null)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Damage Goods has not created.", false);
                return new OkObjectResult(jwt);
            }

            int damageGoodsId = await service.SaveDamageGoods(user.employeeId.ToString(), model);

            if (damageGoodsId == 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Damage Goods has not created.", false);
                return new OkObjectResult(jwt);
            }


            int result = await service.SaveDamageGoodsDetails(user.employeeId.ToString(), model.lstDetailsViewModel, damageGoodsId);

            if (result != 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Damage Goods Details has created successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Damage Goods Details has not created.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpPost("DeleteDamageGoodsById")]
        public async Task<IActionResult> DeleteDamageGoodsById([FromBody] int damageGoodsId)
        {

            if (Authentication().Result == false) return new OkObjectResult(jwts);

            if (damageGoodsId <= 0)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Damage Goods has not deleted.", false);
                return new OkObjectResult(jwt);
            }
            bool result = await service.DeleteDamageGoodsById(user.employeeId.ToString(), damageGoodsId);

            if (result == true)
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Damage Goods has deleted successfully.", true);
                return new OkObjectResult(jwt);
            }
            else
            {
                var jwt = await Tokens.setJwt(new JsonSerializerSettings { Formatting = Formatting.Indented }, "Damage Goods has not deleted.", false);
                return new OkObjectResult(jwt);
            }
        }

        [HttpGet("GetDamageGoodsById")]
        public async Task<IActionResult> GetDamageGoodsById(int? damageGoodsId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetDamageGoodsById(damageGoodsId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("GetMaxDamageGoodsNumber")]
        public async Task<IActionResult> GetMaxDamageGoodsNumber(DateTime recvDate)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetMaxDamageGoodsNumber(recvDate);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }
        
        #endregion

        #region Damage Goods Details
        
        [HttpGet("GetDamageGoodsDetailsById")]
        public async Task<IActionResult> GetDamageGoodsDetailsById(int? damageGoodsId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetDamageGoodsDetailsById(damageGoodsId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        #endregion

        #region Damage Goods Report
        
        [HttpGet("GetDamageGoodsReportById")]
        public async Task<IActionResult> GetDamageGoodsReportById(int damageGoodsId)
        {
            if (Authentication().Result == false) return new OkObjectResult(jwts);

            var datajson = await service.GetDamageGoodsReportById(damageGoodsId);
            var jwt = await Tokens.getData(datajson.data, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
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