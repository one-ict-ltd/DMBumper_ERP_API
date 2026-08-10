using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Sales.Models;
using ONEERP.Data;
using ONEERP.Data.Entity.Sales;
using ONEERP.ERPServices.Sales.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Sales
{
    public class SalesRemittanceService : ISalesRemittanceService
    {
        private readonly ERPDbContext _context;

        public SalesRemittanceService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveSalesRemittance(string id, SalesRemittanceMasterViewModel model)
        {
            try
            {
                var remMaster = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetRemittanceMaster  {id}, {model.remittanceId},{model.remittanceDate},{model.selectedAmount}").AsNoTracking().FirstOrDefaultAsync();
                if (remMaster.isSuccess > 0)
                {
                    int isSuccess = 0;
                    foreach (var item in model.salesRemittanceDetails)
                    {
                        var result2 = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetSalRemittance  {id}, {item.remittanceId}, {item.remittanceDate}, {item.remittanceNo}, {item.remittanceTypeId}, {item.oplTranNo}, {item.depositDate}, {item.bankBranchId}, {item.depositRefNo} , {item.depositAmount}, {item.remarks}, {item.depotCode},{remMaster.isSuccess}").AsNoTracking().FirstOrDefaultAsync();

                        isSuccess = result2.isSuccess;
                    }
                    if(isSuccess <= 0)
                    {
                        remMaster.isSuccess = 0;
                    }
                }
                return remMaster.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<int> DeleteRemittance(string id, int remittanceId)
        {
            try
            {

                var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpDeleteSalRemittance {id}, {remittanceId}").AsNoTracking().FirstOrDefaultAsync();
                return result.isSuccess;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<ICollection<SalRemittanceViewModel>> CheckRemittanceTransactionNumber(SalesRemittanceMasterViewModel model)
        {
            var existingEntities = new List<SalRemittanceViewModel>();

            foreach (var item in model.salesRemittanceDetails)
            {
                var existEntity = await _context.SalRemittance
                    .Where(x => x.oplTranNo.Trim() == item.oplTranNo.Trim() && (x.isActive == null || x.isActive == true) && (x.isDelete == null || x.isDelete == false))
                    .FirstOrDefaultAsync();
                if (existEntity != null)
                {
                    existingEntities.Add(new SalRemittanceViewModel
                    {
                        remittanceId = existEntity.remittanceId,
                        remittanceDate = existEntity.remittanceDate,
                        oplTranNo = existEntity.oplTranNo,
                        remittanceNo = existEntity.remittanceNo,
                        remittanceTypeId = existEntity.remittanceTypeId
                    });
                }

            }

            return existingEntities;
        }


        public async Task<int> SaveSalesRemittanceSlips(string id, List<SalesRemittanceSlipViewModel> salesRemittanceSlips, int? remittanceId)
        {
            var resultCount = 0;
            foreach (var item in salesRemittanceSlips)
            {
                if (!(item.remittanceSlipId > 0 && item.fileString == null && item.fileName != null))
                {
                    string[] res = item.fileString?.Split(',');
                    if (res?.Length > 1)
                    {
                        Byte[] bytes = Convert.FromBase64String(res[1]);
                        string servePath = ("./wwwroot/RemittanceSlips");
                        if (!Directory.Exists(servePath)) Directory.CreateDirectory(servePath);
                        string fileName = ($"{DateTime.Now.Ticks}.{item.ext}");
                        string filePath = ($"{servePath}/{fileName}");
                        File.WriteAllBytes(filePath, bytes);

                        item.resourceUrl = filePath;
                    }
                    var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpSetRemittanceSlip  {id}, {item.remittanceSlipId}, {remittanceId}, {item.resourceUrl}").AsNoTracking().FirstOrDefaultAsync();
                    if (result.isSuccess > 0)
                        resultCount++;
                }

            }

            return resultCount;
        }

        public async Task<JsonViewModel> GetSalesRemittanceList(int? remittanceId, int? userId, DateTime? fDate, DateTime? tDate)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesRemittanceJSON {remittanceId},{userId},{fDate},{tDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetSalesRemittanceSummary(string depotCode, int? userId, DateTime? fDate, DateTime? tDate, int? bankId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesRemittanceSummyryJSON {depotCode},{userId},{fDate},{tDate},{bankId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetOplTranNoStatus(string opltranNo, int? remittanceId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetOplTranNoStatusJSON {opltranNo},{remittanceId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetSalesRemittanceById(int? remittanceId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetSalesRemittanceByIdJSON {remittanceId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetCashinHandByDepotCode(int? userId, string depotCode, DateTime? qDate)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetCashInHandByDepotCodeJSON {depotCode},{qDate},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetRemittanceSlipsJson(int? remittanceId, int? remittanceSlipId)
        {
            var result = await _context.jsonViewModels.FromSql($"SalSpGetRemittanceSlipsJSON {remittanceId},{remittanceSlipId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }

        public async Task<JsonViewModel> GetDepotWiseCollections(int? userId, string depotCode)
        {
            try
            {
                var result = await _context.jsonViewModels.FromSql($"SalSpDipotWiseCollectionJson {userId},{depotCode}").AsNoTracking().FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpdateHasRemittanceOfCollectionMaster(string id, ICollection<HasRemittanceOfCollectionMasterUpdateViewModel> models)
        {

            try
            {

                foreach (var item in models)
                {
                    var result = await _context.saveUpdateValueViewModels.FromSql($"SalSpUpdateHasRemittanceOfCollection  {id}, {item.collectionMasterId}, {item.collectionNumber},{item.remittanceId}").AsNoTracking().FirstOrDefaultAsync();
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}