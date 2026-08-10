using ONEERP.Areas.Purchase.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.Purchase.Interfaces
{
    public interface IGRNService
    {
        Task<JsonViewModel> getGRNForQA(int? userId);
        Task<JsonViewModel> getGRNForRetest(int? userId);
        Task<JsonViewModel> getGRNImportForQA(int? userId);
        Task<JsonViewModel> getGrnDetailsForQA(int? grnMasterId,string InitialOrRetest);
        Task<JsonViewModel> getGrnDetailsForRetest(int? grnMasterId, string grnType);
        Task<JsonViewModel> getGrnImportDetailsForQA(int? ImpgrnMasterId, string InitialOrRetest);
        Task<int> UpdateGRNQaMasterForApproval(int? userId, int? approvalStatus, List<grnlist> models);
        Task<int> UpdateGRNQaForApproval(int? userId, int? approvalStatus, List<grnlist> models, DateTime? RetestDate,string InitialOrRetest);
        Task<int> UpdateGRNImportQaMasterForApproval(int? userId, int? approvalStatus, List<grnImportqalist> models);
        Task<int> UpdateGRNImportQaForApproval(int? userId, int? approvalStatus, List<grnImportqalist> models, DateTime? RetestDate, string InitialOrRetest);
        Task<int> SaveGrnLogtbl(int? userId, PurGrnLogViewModel model);
    }
}
