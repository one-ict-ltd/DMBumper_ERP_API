using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Inventory.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Inventory.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Inventory
{
    public class DamageGoodsService : IDamageGoodsService
    {
        private readonly ERPDbContext _context;

        public DamageGoodsService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteDamageGoodsById(string userId, int damageGoodsId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"InvSpDeleteDamageGoodsById {userId}, {damageGoodsId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }

        public async Task<JsonViewModel> GetDamageGoodsById(int? damageGoodsId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetDamageGoodsByIdJSON {damageGoodsId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetDamageGoodsDetailsById(int? stockMasterId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetDamageGoodsDetailsByIdJSON {stockMasterId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetMaxDamageGoodsNumber(DateTime date)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetMaxDamageGoodsNumberJson {date}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }


        public async Task<int> SaveDamageGoods(string userId, DamageGoodsViewModel model)
        {
            try
            {
            var result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetDamageGoods {userId}, {model.damageGoodsId},{model.damageGoodsNo},{model.receiveDate},{model.companyId},{model.sbuId},{model.storeId},{model.stockTypeId},{model.stockCategoryId},{model.isActive}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<int> SaveDamageGoodsDetails(string userId, List<DamageGoodsDetailsViewModel> models, int damageGoodsId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (var item in models)
            {
                try
                {
                    result = await _context.saveUpdateValueViewModels.FromSql($"InvSpSetDamageGoodsDetails {userId},{item.damageGoodsDetailsId},{damageGoodsId},{item.damageQty},{item.stockTypeId},{item.productWiseSpecificationId},{item.remarks},{item.isSelect},{item.isActive},{item.barcodeDetailsId}").AsNoTracking().FirstOrDefaultAsync();
                }
                catch (System.Exception ex)
                {
                    throw;
                }
            }
            return result.isSuccess;
        }

        //public async Task<JsonViewModel> GetDamageGoodsReport(int productId, int productWiseSpecificationId, int companyId, int sbuId, int storeId, bool isStoreWiseGroup)
        //{
        //    var result = await _context.jsonViewModels.FromSql($"InvSpGetDamageGoodsReportByIdJson {productId},{productWiseSpecificationId},{companyId},{sbuId},{storeId},{isStoreWiseGroup}").AsNoTracking().FirstOrDefaultAsync();
        //    return result;
        //}

        public async Task<JsonViewModel> GetDamageGoodsReportById(int damageGoodsId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetDamageGoodsReportByIdJson {damageGoodsId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
    }
}
