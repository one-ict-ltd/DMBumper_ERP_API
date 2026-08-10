using ONEERP.Areas.Inventory.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Inventory.Interfaces
{
    public interface IDamageGoodsService
    {
        #region Damage Goods

        Task<JsonViewModel> GetDamageGoodsById(int? damageGoodsId);
        Task<JsonViewModel> GetDamageGoodsDetailsById(int? damageGoodsId);
        Task<JsonViewModel> GetMaxDamageGoodsNumber(DateTime date);
        Task<int> SaveDamageGoods(string userId, DamageGoodsViewModel model);
        Task<int> SaveDamageGoodsDetails(string userId, List<DamageGoodsDetailsViewModel> model, int damageGoodsId);
        Task<bool> DeleteDamageGoodsById(string userId, int damageGoodsId);

        //Task<JsonViewModel> GetDamageGoodsReport(int productId, int productWiseSpecificationId, int companyId, int sbuId, int storeId, bool isStoreWiseGroup);
        Task<JsonViewModel> GetDamageGoodsReportById(int damageGoodsId);

        #endregion
    }
}
