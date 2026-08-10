using ONEERP.Areas.TaskManagement.Models;
using ONEERP.Areas.DigitalGift.Models;
using ONEERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ONEERP.ERPServices.DigitalGift.Interfaces
{
   public interface IDigitalGiftService
    {
        Task<JsonViewModel> ValidateRequestedInfo(int? userId, DigitalGiftModels model);
        Task<int> DigitalGiftDisburseLog(DigitalGiftModels model);
        Task<JsonViewModel> DigitalGiftDisburse(int? userId, DigitalGiftModels model);
        Task<JsonViewModel> DigitalGiftDisburseV2(int? userId, DigitalGiftModels model);
        Task<OAuthResponse> GetBulkOAuthResponse();
        Task<PackListModel> GetPackList(OAuthResponse model);
        Task<ProductOrderResponseModel> DigitalGiftPackDisburse(int? userId, string accessToken, DigitalGiftModels model, Pack_list packList);//PackListModel packListModel)
        Task<JsonViewModel> UpdateDigitalGiftPackDisburseStatus(int? userId, string packName, DigitalGiftModels model, ProductOrderResponseModel disburseResponseModel);
    }
}
