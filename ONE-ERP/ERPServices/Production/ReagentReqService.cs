using Microsoft.EntityFrameworkCore;
using ONEERP.Areas.Auth.Models;
using ONEERP.Areas.Production.Models;
using ONEERP.Data;
using ONEERP.ERPServices.Production.Interfaces;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Production
{
    public class ReagentReqService: IReagentReqService
    {
        private readonly ERPDbContext _context;
        public ReagentReqService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<JsonViewModel> GetMaxReagentReqNumber(DateTime reagentReqDate)
        {
            var result = await _context.jsonViewModels.FromSql($"PrdSpGetMaxReagentReqNumberJson {reagentReqDate}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<JsonViewModel> GetAllProductForReagentReq(int productId, int employeeId)
        {
            var result = await _context.jsonViewModels.FromSql($"InvSpGetAllProductForReagentReq {productId},{employeeId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<int> SaveReagentReq(string id, ReagentRequisitionViewModel ReqViewModel)
        {
            var result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSetReagentReq {id}, {ReqViewModel.reagentReqId},{ReqViewModel.reagentReqDate},{ReqViewModel.fromsbuId},{ReqViewModel.tosbuId},{ReqViewModel.purpose},{ReqViewModel.isUrgency},{ReqViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();//,{ReqViewModel.purpose},{ReqViewModel.isUrgency},{ReqViewModel.approvalStatus}
            return result.isSuccess;

        }
        public async Task<int> SaveReagentReqDetails(string id, List<ReagentReqDetailsViewModel> purReagentReqDetailsViewModels, int reagentReqId)
        {
            var result = new SaveUpdateValueViewModel();
            foreach (ReagentReqDetailsViewModel purReagentReqDetailsViewModel in purReagentReqDetailsViewModels)
            {
                result = await _context.saveUpdateValueViewModels.FromSql($"PrdSpSetReagentReqDetails {id},{purReagentReqDetailsViewModel.reagentReqDetailsId},{reagentReqId},{purReagentReqDetailsViewModel.productId},{purReagentReqDetailsViewModel.productWiseSpecificationId},{purReagentReqDetailsViewModel.reqQty},{purReagentReqDetailsViewModel.price},{purReagentReqDetailsViewModel.isActive}").AsNoTracking().FirstOrDefaultAsync();
            }
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetReagentReqById(int? userId, int? reagentReqId)
        {
            var result = await _context.jsonViewModels.FromSql($"PrdSpGetProductReqInfoJSON {reagentReqId},{userId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> DeleteReagentReqById(int userId, int reagentReqId)
        {
            var result = await _context.saveUpdateViewModels.FromSql($"PrdSpDeleteReagentReq {userId}, {reagentReqId}").AsNoTracking().FirstOrDefaultAsync();
            return result.isSuccess;
        }
        public async Task<JsonViewModel> GetReagentReqDetailsById(int? reagentReqId)
        {
            var result = await _context.jsonViewModels.FromSql($"PrdSpGetReagentReqDetailsJSON {reagentReqId}").AsNoTracking().FirstOrDefaultAsync();
            return result;
        }
    }
}
